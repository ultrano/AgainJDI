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
		var gi = GameInstance.Instance;
		gi.GameInfo.accountInfo.OnLevelUpdated.AddListener(OnLevelUpdated);
		gi.GameInfo.accountInfo.OnExpUpdated.AddListener(OnExpUpdated);
		gi.GameInfo.accountInfo.OnGoldUpdated.AddListener(OnGoldUpdated);
	}

	void OnLevelUpdated(AccountInfo info)
	{
		levelText.text = info.Level.ToString();
	}

	void OnExpUpdated(AccountInfo info)
	{
		expText.text = info.Exp.ToString();
	}

	void OnGoldUpdated(AccountInfo info)
	{
		goldText.text = info.Gold.ToString();
	}
}
