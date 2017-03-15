using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IngameTrigger : MonoBehaviour
{

	public Image aimPanel;
	public RectTransform aimPoint;
	public RectTransform aimOrigin;

	public TriggerEvent OnBegineTrigger;
	public TriggerEvent OnTriggerring;
	public TriggerEvent OnEndTrigger;

	private float radian = 0;
	private float radianOffset = 0;
	private float scaleFactor = 1;
	private float threshold = 30;
	private float ballRadius = 100;
	private bool  gotInThreshold = false;

	void Start ()
	{
		aimPanel.gameObject.SetActive (false);
		if (IngameManager.Instance != null)
		{
			var mainCamera = IngameManager.Instance.MainCamera;
			radianOffset = (Mathf.PI / 2.0f) - Mathf.Acos (Vector3.Dot (mainCamera.transform.up, Vector3.right));
		}
		var rootCanvas = transform.root.GetComponentInChildren<Canvas> ();
		if (rootCanvas != null)
		{
			scaleFactor = rootCanvas.scaleFactor;
			ballRadius = ballRadius * scaleFactor;
			threshold = threshold * scaleFactor;
		}
	}

	public void OnBeginDrag (BaseEventData bed)
	{
	}

	public void OnDragging (BaseEventData bed)
	{
		var ped = bed as PointerEventData;
		var delta = ped.position - ped.pressPosition;

		float length = delta.magnitude;
		float halfPI = Mathf.PI / 2.0f;

		radian = Mathf.Atan2 (-delta.x, delta.y );
		radian = Mathf.Clamp (radian, -halfPI + radianOffset, halfPI + radianOffset);

		if (gotInThreshold == false)
		{
			if (gotInThreshold = (length >= threshold))
			{
				aimPanel.gameObject.SetActive (true);
				aimPanel.rectTransform.position = ped.pressPosition;
				aimPanel.rectTransform.rotation = Quaternion.Euler (0, 0, radianOffset * Mathf.Rad2Deg);
				OnBegineTrigger.Invoke(radian - radianOffset);
			}
		}
		else
		{
			length = Mathf.Min (length, ballRadius);

			delta.x = Mathf.Cos (radian + halfPI);
			delta.y = Mathf.Sin (radian + halfPI);

			Vector3 distanced = delta * length;
			aimPoint.position = aimPanel.rectTransform.position + distanced;

			OnTriggerring.Invoke (radian - radianOffset);
		}
	}

	public void OnEndDrag (BaseEventData bed)
	{
		if (gotInThreshold)
		{
			gotInThreshold = false;
			aimPanel.gameObject.SetActive (false);
			OnEndTrigger.Invoke (radian - radianOffset);
		}
	}

	[System.Serializable]
	public class TriggerEvent : UnityEvent<float>
	{

	}
}
