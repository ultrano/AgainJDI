using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSkillIconUI : MonoBehaviour {
	
	public Text  levelText;
	public Image iconImage;
	public Image amountGauge;
	public Text  amountText;

	private GameSkillInfo skillInfo;

	public GameSkillInfo SkillInfo
	{
		get { return skillInfo; }
		set
		{
			if ((skillInfo = value) == null)
				return;
			
			GameSkillData data = Game.Data.Skill.GetById (skillInfo.SkillId);

			iconImage.sprite = data.IconSprite;

			skillInfo.OnLevelUpdated.AddListener(OnSkillCardInfoUpdated);
			skillInfo.OnAmountUpdated.AddListener(OnSkillCardInfoUpdated);
			OnSkillCardInfoUpdated (skillInfo);
		}
	}

	private void OnSkillCardInfoUpdated(GameSkillInfo info)
	{
		levelText.text = info.Level.ToString();

		GameSkillData data = Game.Data.Skill.GetById (skillInfo.SkillId);
		if (info.Level < data.MaxLevel)
		{
			int maxAmount = 50; //! test value
			amountGauge.fillAmount = (float)info.Amount/(float)maxAmount;
			amountText.text = string.Format ("{0}/{1}", info.Amount, maxAmount);
		}
		else
		{
			amountGauge.fillAmount = 1.0f;
			amountText.text = info.Amount.ToString ();
		}
	}
}
