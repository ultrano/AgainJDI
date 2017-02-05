using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHeadUI : MonoBehaviour {

	WeakReference characterWR = new WeakReference(null);
	public Character Character { get { return characterWR.Target as Character; } set{ characterWR.Target = value; }}

	public Camera mainCamera;
	public Image healthImage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		transform.position = mainCamera.WorldToScreenPoint(Character.transform.position);
	}

	public void SetHealth(float ratio)
	{
		healthImage.fillAmount = ratio;
	}
}
