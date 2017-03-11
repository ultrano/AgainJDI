using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Amazon;
using Amazon.CognitoSync;
using Amazon.Runtime;
using Amazon.CognitoIdentity;
using Amazon.CognitoIdentity.Model;
using Amazon.CognitoSync.SyncManager;

public class GameAWS : MonoBehaviour {

	public string IdentityPoolId = "";
	public string Region = RegionEndpoint.USEast1.SystemName;

	public CognitoAWSCredentials Credentials { get; private set; }
	public CognitoSyncManager SyncManager { get; private set; }

	public static bool IsInitialized { get { return Instance != null; } }

	private RegionEndpoint _Region { get { return RegionEndpoint.GetBySystemName(Region); } }

	public static GameAWS Instance { get; private set; }
	void Start () {
		if (Instance != null)
			throw new System.Exception ("there is already GameAWS, it might be initialized again");
		
		UnityInitializer.AttachToGameObject(this.gameObject);

		Credentials = new CognitoAWSCredentials(IdentityPoolId, _Region);
		SyncManager = new CognitoSyncManager(Credentials, new AmazonCognitoSyncConfig { RegionEndpoint = _Region });

		Instance = this;
	}

}
