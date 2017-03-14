using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Amazon;
using Amazon.CognitoSync;
using Amazon.Runtime;
using Amazon.Lambda;
using Amazon.Lambda.Model;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.CognitoIdentity;
using Amazon.CognitoIdentity.Model;
using Amazon.CognitoSync.SyncManager;

public class GameAWS : MonoBehaviour {

	public string IdentityPoolId = "";
	public string Region = RegionEndpoint.USEast1.SystemName;
	public RegionEndpoint _Region { get { return RegionEndpoint.GetBySystemName(Region); } }

	public static CognitoAWSCredentials Credentials { get; private set; }
	public static CognitoSyncManager    Cognito { get; private set; }
	public static IAmazonLambda         Lambda { get; private set; }

	public static bool IsInitialized { get { return Instance != null; } }
	public static GameAWS Instance { get; private set; }

	void Start()
	{
		UnityInitializer.AttachToGameObject(this.gameObject);

		Credentials     = new CognitoAWSCredentials(IdentityPoolId, _Region);
		Cognito         = new CognitoSyncManager(Credentials, new AmazonCognitoSyncConfig { RegionEndpoint = _Region });
		Lambda          = new AmazonLambdaClient(Credentials, _Region);

		Instance    = this;
	}

	public static void InvokeLambdaAsync<T>(string funcName, object payLoad, Action<T> callback)
	{
		Lambda.InvokeAsync (new InvokeRequest () {
			FunctionName = funcName,
			Payload = JsonUtility.ToJson (payLoad)
		}, (result) => {
			if (result.Exception != null) {
				Debug.LogError (result.Exception.Message);
				return;
			}

			string jsonStr = Encoding.ASCII.GetString (result.Response.Payload.ToArray ());

			var data = JsonUtility.FromJson<T> (jsonStr);
			if (callback != null)
				callback (data);
		});
	}
}
