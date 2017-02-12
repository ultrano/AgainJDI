using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
	bool connected = false;
	public Text Result;
	// Use this for initialization
	void Start ()
	{
		Client.Instance.OnPacketReceived += OnPacketReceived;
		Client.Instance.Connect ();
	}


	void Update()
	{
		if (connected == false && Client.Instance.Socket.Connected)
		{
			connected = true;
			Result.text = "Waiting Player";
		}
	}

	void OnPacketReceived (Packet packet)
	{
		Client.Instance.OnPacketReceived -= OnPacketReceived;
		SceneManager.LoadScene ("Ingame");
	}
}
