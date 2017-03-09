using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkillCardInfo
{
	public SkillData SkillData
	{
		get { return skillData; }
		set { skillData = value; OnUpdated.Invoke (this); }
	}

	public int Level
	{
		get { return level; }
		set { level = value; OnUpdated.Invoke (this); }
	}

	public int Amount
	{
		get { return amount; }
		set { amount = value; OnUpdated.Invoke (this); }
	}

	public int MaxAmount
	{
		get { return skillData.GetMaxCardAmount (Level); }
	}

	private SkillData skillData;
	private int level;
	private int amount;

	public SkillCardEvent OnUpdated = new SkillCardEvent();

	[System.Serializable]
	public class SkillCardEvent : UnityEvent<SkillCardInfo> {}
}