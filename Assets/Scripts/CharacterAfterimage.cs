using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAfterimage : MonoBehaviour {

	public GameObject target = null;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("CopyRenderer", 0, 2);
	}

	void CopyRenderer()
	{
		Material material;

		GameObject.Instantiate (target.gameObject);
	}
}
