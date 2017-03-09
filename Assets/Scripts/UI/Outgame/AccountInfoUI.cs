using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccountInfoUI : MonoBehaviour
{
	public Text goldText;
	public Text expText;
	public Text levelText;

	// Use this for initialization
	void Start ()
	{
		Game.AccountInfo.OnLevelUpdated.AddListener(OnLevelUpdated);
		Game.AccountInfo.OnExpUpdated.AddListener(OnExpUpdated);
		Game.AccountInfo.OnGoldUpdated.AddListener(OnGoldUpdated);
	}

	void OnLevelUpdated(GameAccountInfo info)
	{
		levelText.text = info.Level.ToString();
	}

	void OnExpUpdated(GameAccountInfo info)
	{
		expText.text = info.Exp.ToString();
	}

	void OnGoldUpdated(GameAccountInfo info)
	{
		goldText.text = info.Gold.ToString();
	}
}
