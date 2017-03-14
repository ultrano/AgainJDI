using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCardInfoUI : MonoBehaviour {

	GameSkillInfo skillInfo;
	SkillData skillData;
	public Text levelText;
	public Image cardImage;
	public Image cardNumGauge;
	public Text cardNumText;

	public GameSkillInfo SkillInfo
	{
		get { return skillInfo; }
		set { SetInfo (skillInfo = value); }
	}

	private void SetInfo(GameSkillInfo info)
	{
	}

	private void OnSkillCardInfoUpdated(GameSkillInfo info)
	{
	}

}
