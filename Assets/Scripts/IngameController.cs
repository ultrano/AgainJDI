using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IngameController : Controller
{
	public Camera mainCamera;
	public Character character;

	public Image triggerImage;
	public Image aimArrowImage;
	private Vector3 upDir;
	private Vector3 rightDir;
	// Use this for initialization
	void Start ()
	{
		upDir = mainCamera.transform.up;
		upDir.y = 0;
		upDir.Normalize ();

		rightDir = mainCamera.transform.right;
		rightDir.y = 0;
		rightDir.Normalize ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	void FixedUpdate()
	{
		if (aimArrowImage.gameObject.activeSelf)
			aimArrowImage.rectTransform.position = mainCamera.WorldToScreenPoint(character.transform.position);
	}

	public void OnPadDragging(BaseEventData bed)
	{
		var ped   = bed as PointerEventData;
		var delta = ped.position - ped.pressPosition;

		Vector3 direction = (rightDir * delta.x) + (upDir * delta.y);
		character.MoveToDirection (direction.normalized);
	}

	public void OnEndPadDrag(BaseEventData bed)
	{
		character.StopMovement ();
	}

	public void OnBeginPullTrigger(BaseEventData bed)
	{
		var ped   = bed as PointerEventData;

		triggerImage.rectTransform.position = ped.position;
		aimArrowImage.rectTransform.position = mainCamera.WorldToScreenPoint(character.transform.position);

		triggerImage.gameObject.SetActive (true);
		aimArrowImage.gameObject.SetActive (true);
	}

	public void OnPullTrigger(BaseEventData bed)
	{
		var ped   = bed as PointerEventData;
		var delta = ped.position - ped.pressPosition;

		float screenRadian = Mathf.Atan2 (-delta.y, -delta.x);
		float worldDegree = Mathf.Rad2Deg * (screenRadian) - 90;

		character.TurnToDegree (worldDegree);

		aimArrowImage.rectTransform.rotation = Quaternion.Euler (0, 0, (screenRadian * Mathf.Rad2Deg) - 90);
	}

	public void OnEndPullTrigger(BaseEventData bed)
	{
		var ped   = bed as PointerEventData;
		var delta = ped.position - ped.pressPosition;

		triggerImage.gameObject.SetActive (false);
		aimArrowImage.gameObject.SetActive (false);

		Vector3 direction = (rightDir * -delta.x) + (upDir * -delta.y);
		character.ReadyFire (direction.normalized);
	}

	public void OnFireButtonClicked()
	{
	}
}
