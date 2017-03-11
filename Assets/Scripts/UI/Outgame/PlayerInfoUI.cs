using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUI : MonoBehaviour
{
	public Text goldText;
	public Text expText;
	public Text levelText;

	// Use this for initialization
	void Start ()
	{
		Game.PlayerInfo.OnLevelUpdated.AddListener(OnLevelUpdated);
		Game.PlayerInfo.OnExpUpdated.AddListener(OnExpUpdated);
		Game.PlayerInfo.OnGoldUpdated.AddListener(OnGoldUpdated);
		OnLevelUpdated (Game.PlayerInfo);
		OnExpUpdated (Game.PlayerInfo);
		OnGoldUpdated (Game.PlayerInfo);
	}

	void OnLevelUpdated(GamePlayerInfo info)
	{
		levelText.text = info.Level.ToString();
	}

	void OnExpUpdated(GamePlayerInfo info)
	{
		expText.text = info.Exp.ToString();
	}

	void OnGoldUpdated(GamePlayerInfo info)
	{
		goldText.text = info.Gold.ToString();
	}
}
