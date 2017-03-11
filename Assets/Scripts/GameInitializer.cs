using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Amazon;
using Amazon.CognitoSync;
using Amazon.Runtime;
using Amazon.CognitoIdentity;
using Amazon.CognitoIdentity.Model;
using Amazon.CognitoSync.SyncManager;

public class GameInitializer : MonoBehaviour {

	public Text MessageText = null;

	// Use this for initialization
	IEnumerator Start ()
	{
		PrintMsg("Game initializer's started");
		//! wait for aws initializing
		while (!GameAWS.IsInitialized)
			yield return null;

		PrintMsg("AWS is initialized");

		//! initialize game instances other else
		Game.Init (GameAWS.Instance);

		PrintMsg("GAME is initialized");

		PrintMsg("Synchronizing player info will be started");
		{
			bool isFinished = false;
			var playerInfo = Game.AWS.SyncManager.OpenOrCreateDataset ("PlayerInfo");

			playerInfo.OnSyncSuccess += (sender, e) => 
			{
				var dataset = sender as Dataset;

				Func<string, int, int> str2int = (str, val) =>
				{
					return string.IsNullOrEmpty (str) ? val : int.Parse (str);
				};

				Game.PlayerInfo.Level = str2int (dataset.Get ("level"), 1);
				Game.PlayerInfo.Exp   = str2int (dataset.Get ("exp"), 0);
				Game.PlayerInfo.Gold  = str2int (dataset.Get ("gold"), 0);

				isFinished = true;
			};

			playerInfo.SynchronizeAsync ();

			PrintMsg("Waiting synchronizing player info ");
			while (!isFinished)
				yield return null;
		}
		PrintMsg("Synchronizing player info is finished");

		PrintMsg("Initializing has been finished all");

		yield return new WaitForSeconds (1);

		PrintMsg("Entering to the lobby now :)");

		yield return new WaitForSeconds (1);

		SceneManager.LoadScene ("Outgame");
	}


	private void PrintMsg(string msg)
	{
		MessageText.text = msg;
		Debug.Log (msg);
	}
}
