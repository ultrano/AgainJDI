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

	public UnityEvent OnBegineTrigger;
	public TriggerEvent OnTriggerring;
	public TriggerEvent OnEndTrigger;

	private float radian = 0;
	private float radianOffset = 0;
	// Use this for initialization

	void Start ()
	{
		var mainCamera = IngameManager.Instance.MainCamera;
		radianOffset = (Mathf.PI / 2.0f) - Mathf.Acos (Vector3.Dot (mainCamera.transform.up, Vector3.right));
	}

	public void OnBeginDrag (BaseEventData bed)
	{
		var ped = bed as PointerEventData;
		aimPanel.rectTransform.position = ped.position;
		aimPanel.rectTransform.rotation = Quaternion.Euler (0, 0, radianOffset * Mathf.Rad2Deg);
		//aimPoint.rectTransform.position = ped.position;
		OnBegineTrigger.Invoke();
	}

	public void OnDragging (BaseEventData bed)
	{
		CalcRadian (bed);

		aimPoint.rotation = Quaternion.Euler (0, 0, radian * Mathf.Rad2Deg);

		OnTriggerring.Invoke (radian, radianOffset);
	}

	public void OnEndDrag (BaseEventData bed)
	{
		CalcRadian (bed);
		OnEndTrigger.Invoke (radian, radianOffset);
	}

	private void CalcRadian (BaseEventData bed)
	{
		var ped = bed as PointerEventData;
		Vector3 pointerPos = ped.position;
		var delta = pointerPos - aimOrigin.position;

		float halfPI = Mathf.PI / 2.0f;
		radian = Mathf.Atan2 (-delta.x, delta.y);
		radian = Mathf.Clamp (radian, radianOffset - halfPI, radianOffset + halfPI);
	}

	[System.Serializable]
	public class TriggerEvent : UnityEvent<float, float>
	{

	}
}
