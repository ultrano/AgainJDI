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
		var playerInfo = Game.Info.Player;
		playerInfo.OnLevelUpdated.AddListener(OnLevelUpdated);
		playerInfo.OnExpUpdated.AddListener(OnExpUpdated);
		playerInfo.OnGoldUpdated.AddListener(OnGoldUpdated);
		OnLevelUpdated (playerInfo);
		OnExpUpdated (playerInfo);
		OnGoldUpdated (playerInfo);
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
