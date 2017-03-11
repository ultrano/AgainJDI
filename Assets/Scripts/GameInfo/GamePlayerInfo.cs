using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GamePlayerInfo
{
	public int[] skillIdSlots = new int[(int)GameEnum.MaxEquipSlot];

	public int Level
	{
		get { return level; }
		set { level = value; OnLevelUpdated.Invoke (this); }
	}

	public int Exp
	{
		get { return exp; }
		set { exp = value; OnExpUpdated.Invoke (this); }
	}

	public int Gold
	{
		get { return exp; }
		set { exp = value; OnGoldUpdated.Invoke (this); }
	}

	public NotifyEvent OnLevelUpdated = new NotifyEvent();
	public NotifyEvent OnExpUpdated = new NotifyEvent();
	public NotifyEvent OnGoldUpdated = new NotifyEvent();

	private int level;
	private int exp;
	private int gold;

	[System.Serializable]
	public class NotifyEvent : UnityEvent<GamePlayerInfo> {}
}