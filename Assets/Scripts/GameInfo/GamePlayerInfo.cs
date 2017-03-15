using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Amazon.DynamoDBv2.DocumentModel;


public class GamePlayerInfo
{
	public string Id { get; set; }
	public int    Level
	{
		get { return level; }
		set { level = value; OnLevelUpdated.Invoke (this); }
	}
	public int    Exp
	{
		get { return exp; }
		set { exp = value; OnExpUpdated.Invoke (this); }
	}
	public int    Gold
	{
		get { return gold; }
		set { gold = value; OnGoldUpdated.Invoke (this); }
  	}

	public NotifyEvent OnLevelUpdated = new NotifyEvent();
	public NotifyEvent OnExpUpdated = new NotifyEvent();
	public NotifyEvent OnGoldUpdated = new NotifyEvent();

	public class NotifyEvent : UnityEvent<GamePlayerInfo> {}

	private int level = 0;
	private int exp = 0;
	private int gold = 0;
}