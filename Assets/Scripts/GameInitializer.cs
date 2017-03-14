using System;
using System.Text;
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

using Amazon.Lambda.Model;

using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

using Facebook;
using Facebook.Unity;
using Facebook.Unity.Mobile;
using Facebook.Unity.Arcade;

public class GameInitializer : MonoBehaviour {

	public Text MessageText = null;
	public Button FacebookButton = null;

	// Use this for initialization
	IEnumerator Start ()
	{
		//! wait for aws initializing
		while (!GameAWS.IsInitialized)
			yield return null;

		//! wait for facebook initializing
		FB.Init ();
		while (!FB.IsInitialized)
			yield return null;

		//! logging in facebook
		bool hasToken = (AccessToken.CurrentAccessToken != null);
		FacebookButton.gameObject.SetActive (!hasToken);
		if (!hasToken)
		{
			PrintMsg("Please log in facebook");
			while (AccessToken.CurrentAccessToken == null)
				yield return null;
		}
		FacebookButton.gameObject.SetActive (false);

		//! add login token
		GameAWS.Credentials.AddLogin ("graph.facebook.com", AccessToken.CurrentAccessToken.TokenString);

		var logInInfo = GameAWS.Cognito.OpenOrCreateDataset ("LogInInfo");
		logInInfo.OnSyncFailure += OnLogInSyncFailed;
		logInInfo.OnSyncSuccess += OnLogInSyncSuccess;
		logInInfo.Put ("LastTime", System.DateTime.Now.ToString ());
		logInInfo.SynchronizeAsync ();

	}

	public void LogInFacebook()
	{
		FB.LogInWithReadPermissions (new List<string> () { "public_profile", "email", "user_friends" }, 
			(result) =>
			{
				if ((!string.IsNullOrEmpty (result.Error) || !FB.IsLoggedIn))
					PrintMsg("You have to log in facebook");
			});
	}

	void OnLogInSyncSuccess(object sender, SyncSuccessEventArgs e)
	{
		PrintMsg ("Loading information");
		Game.Init ();
		Game.Net.InvokeRequestInfo (()=>
			{
				SceneManager.LoadScene("Outgame");
			});
	}

	void OnLogInSyncFailed(object sender, SyncFailureEventArgs e)
	{
		PrintMsg("Failed to log in, please try again");
		Debug.Log (e.Exception.Message);
	}
	
	private void PrintMsg(string msg)
	{
		MessageText.text = msg;
		Debug.Log (msg);
	}
}

