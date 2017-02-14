using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameManager : MonoBehaviour {


	public static IngameManager Instance{ get{ return instance; } }
	public Camera MainCamera { get { return mainCamera; } }

	[SerializeField]
	private Camera mainCamera;
	private static IngameManager instance;

	IngameManager()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
