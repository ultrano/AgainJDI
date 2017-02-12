using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterNetController : Controller
{
	public Character character;

	// Use this for initialization
	void Start ()
	{
		Client.Instance.OnPacketReceived += OnPacketReceived;
	}

	void OnPacketReceived(Packet packet)
	{
		if (character == null)
			return;
		
		switch(packet.type)
		{
		case Protocol.MoveToDirection:
			{
				var data = packet.GetData<DataMoveToDirection> ();

				Vector3 position;
				position.x = -data.position.x;
				position.z = -data.position.y;
				position.y = 0;

				character.transform.position = position;
				character.MoveToRadian (data.radian + Mathf.PI);
			}
			break;

		case Protocol.StopMovement:
			{
				var data = packet.GetData<DataStopMovement> ();

				Vector3 position;
				position.x = -data.position.x;
				position.z = -data.position.y;
				position.y = 0;

				character.transform.position = position;
				character.StopMovement ();
			}
			break;

		case Protocol.FireToDirection:
			{
				var data = packet.GetData<DataFireToDirection> ();

				Vector3 position;
				position.x = -data.position.x;
				position.z = -data.position.y;
				position.y = 0;

				character.transform.position = position;
				character.ReadyFireToRadian (data.radian + Mathf.PI);
			}
			break;
		case Protocol.StartDash:
			character.StartDashMode ();
			break;
		}


	}
}
