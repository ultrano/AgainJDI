using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IngameController : Controller
{
	public Camera mainCamera;
	public Character character;

	public Image triggerImage;
	public Image aimArrowImage;
	public Image ctrolBallImage;
	public float ctrolBallRadius = 100;
	private Vector3 upDir;
	private Vector3 rightDir;
	private float delay = 0;
	// Use this for initialization
	void Start ()
	{
		upDir = mainCamera.transform.up;
		upDir.y = 0;
		upDir.Normalize ();

		rightDir = mainCamera.transform.right;
		rightDir.y = 0;
		rightDir.Normalize ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey(KeyCode.Space))
		{
			OnDashButtonClicked();
		}
		delay -= Time.deltaTime;
	}

	void FixedUpdate()
	{
		if (aimArrowImage.gameObject.activeSelf)
			aimArrowImage.rectTransform.position = mainCamera.WorldToScreenPoint(character.transform.position);
	}

	public void OnBeginPadDrag(BaseEventData bed)
	{
	}

	public void OnPadDragging(BaseEventData bed)
	{
		var ped   = bed as PointerEventData;
		var delta = ped.position - ped.pressPosition;

		ctrolBallImage.rectTransform.anchoredPosition = delta.normalized * Mathf.Min (delta.magnitude, ctrolBallRadius);

		Vector3 direction = (rightDir * delta.x) + (upDir * delta.y);
		character.MoveToDirection (direction.normalized);

		if (delay < 0)
		{
			delay = 0.1f;
			DataMoveToDirection data = new DataMoveToDirection ();
			data.position.x = character.transform.position.x;
			data.position.y = character.transform.position.z;
			data.radian = Mathf.Atan2 (direction.normalized.z, direction.normalized.x);

			Packet packet = new Packet ();
			packet.type = Protocol.MoveToDirection;
			packet.SetData (data);

			Client.Instance.SendAsync (Packet.Serialize(packet));
		}
	}

	public void OnEndPadDrag(BaseEventData bed)
	{
		ctrolBallImage.rectTransform.anchoredPosition3D = Vector3.zero;
		character.StopMovement ();

		{
			var data = new DataStopMovement ();
			data.position.x = character.transform.position.x;
			data.position.y = character.transform.position.z;

			Packet packet = new Packet ();
			packet.type = Protocol.StopMovement;
			packet.SetData (data);

			Client.Instance.SendAsync (Packet.Serialize(packet));
		}
	}

	public void OnBeginPullTrigger(BaseEventData bed)
	{
		var ped   = bed as PointerEventData;

		triggerImage.rectTransform.position = ped.position;
		aimArrowImage.rectTransform.position = mainCamera.WorldToScreenPoint(character.transform.position);

		triggerImage.gameObject.SetActive (true);
		aimArrowImage.gameObject.SetActive (true);
	}

	public void OnPullTrigger(BaseEventData bed)
	{
		var ped   = bed as PointerEventData;
		var delta = ped.position - ped.pressPosition;

		float screenRadian = Mathf.Atan2 (-delta.y, -delta.x);
		float worldDegree = Mathf.Rad2Deg * (screenRadian) - 90;

		character.TurnToDegree (worldDegree);

		aimArrowImage.rectTransform.rotation = Quaternion.Euler (0, 0, (screenRadian * Mathf.Rad2Deg) - 90);
	}

	public void OnEndPullTrigger(BaseEventData bed)
	{
		var ped   = bed as PointerEventData;
		var delta = ped.position - ped.pressPosition;

		triggerImage.gameObject.SetActive (false);
		aimArrowImage.gameObject.SetActive (false);

		Vector3 direction = (rightDir * -delta.x) + (upDir * -delta.y);
		character.ReadyFire (direction.normalized);

		{
			var data = new DataFireToDirection ();
			data.position.x = character.transform.position.x;
			data.position.y = character.transform.position.z;
			data.radian = Mathf.Atan2 (direction.normalized.z, direction.normalized.x);

			Packet packet = new Packet ();
			packet.type = Protocol.FireToDirection;
			packet.SetData (data);

			Client.Instance.SendAsync (Packet.Serialize(packet));
		}
	}

	public void OnDashButtonClicked()
	{
		character.StartDashMode ();
	}
}
