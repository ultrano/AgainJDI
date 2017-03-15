using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldUI : MonoBehaviour
{
	int gameTime = 180;

	public Text timeText;
	// Use this for initialization
	void Start ()
	{
		InvokeRepeating ("Countdown", 0, 1);
	}

	void Countdown()
	{
		gameTime -= 1;

		int minuts = gameTime / 60;
		int seconds = gameTime % 60;

		timeText.text = string.Format ("{0}:{1}", minuts, seconds);;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
