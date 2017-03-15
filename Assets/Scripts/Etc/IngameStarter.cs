using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameStarter : MonoBehaviour {

	public FieldUI fieldUI;
	public Controller AIController;
	public Controller MeController;

	public void StartGame()
	{
		fieldUI.enabled = true;
		AIController.enabled = true;
		MeController.enabled = true;
		GameObject.Destroy (gameObject);
	}
}
