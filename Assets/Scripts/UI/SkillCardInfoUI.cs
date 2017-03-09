using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCardInfoUI : MonoBehaviour {

	SkillCardInfo cardInfo;
	public Text levelText;
	public Image cardImage;
	public Image cardNumGauge;
	public Text cardNumText;

	public SkillCardInfo CardInfo
	{
		get { return cardInfo; }
		set { SetInfo (cardInfo = value); }
	}

	private void SetInfo(SkillCardInfo info)
	{
		if (info == null)
			return;

		info.OnUpdated.AddListener (OnSkillCardInfoUpdated);
		OnSkillCardInfoUpdated (info);
	}

	private void OnSkillCardInfoUpdated(SkillCardInfo info)
	{
		levelText.text = info.Level.ToString();
		cardNumText.text = String.Format ("{0}/{1}", info.Amount, info.MaxAmount);
	}

}
