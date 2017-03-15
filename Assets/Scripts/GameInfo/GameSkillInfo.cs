using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameSkillInfo
{
	public int SkillId { get; set; }
	public int Level
	{
		get { return level; }
		set { level = value; OnLevelUpdated.Invoke (this); }
	}
	public int Amount
	{
		get { return amount; }
		set { amount = value; OnAmountUpdated.Invoke (this); }
	}

	public class NotifyEvent : UnityEvent<GameSkillInfo> {}
	public NotifyEvent OnLevelUpdated  = new NotifyEvent();
	public NotifyEvent OnAmountUpdated = new NotifyEvent();

	private int level = 0;
	private int amount = 0;
}
