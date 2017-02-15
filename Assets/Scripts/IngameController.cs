using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IngameController : Controller
{
	public Camera mainCamera;
	public Character character;

	public Image aimArrowImage;
	public Image ctrolBallImage;
	public float ctrolBallRadius = 100;
	private Vector3 upDir;
	private Vector3 rightDir;
	private float delay = 0;
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
		if (Input.GetKey(KeyCode.Space))
		{
			OnDashButtonClicked();
		}
		delay -= Time.deltaTime;
	}

	public void OnBeginPadDrag(BaseEventData bed)
	{
	}

	public void OnPadDragging(BaseEventData bed)
	{
		var ped   = bed as PointerEventData;
		var delta = ped.position - ped.pressPosition;

		ctrolBallImage.rectTransform.anchoredPosition = delta.normalized * Mathf.Min (delta.magnitude, ctrolBallRadius);

		Vector3 direction = (rightDir * delta.x) + (upDir * delta.y);
		character.MoveToDirection (direction.normalized);

	}

	public void OnEndPadDrag(BaseEventData bed)
	{
		ctrolBallImage.rectTransform.anchoredPosition3D = Vector3.zero;
		character.StopMovement ();

	}

	public void OnBeginTrigger()
	{
		aimArrowImage.gameObject.SetActive (true);
		
	}

	public void OnTriggering(float radian, float offset)
	{
		float r = radian - offset;
		character.TurnToDegree (r * Mathf.Rad2Deg);

	}

	public void OnEndTrigger(float radian, float offset)
	{
		aimArrowImage.gameObject.SetActive (false);
		float r = radian - offset;
		character.ReadyFireToRadian (r);

	}

	public void OnDashButtonClicked()
	{
		character.StartDashMode ();
	}
}
