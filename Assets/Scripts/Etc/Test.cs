using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour {

	public Image image;
	public GameSkillIconUI card;
	// Use this for initialization
	void Start () {

		Game.Init ();
		Game.Data.Init ();

		GameSkillLevelData levelData = new GameSkillLevelData ();
		levelData.maxAmount = 11;

		GameSkillData data = new GameSkillData ();
		data.icon = "4";
		data.name = "test";

		GameSkillInfo info = new GameSkillInfo ();
		info.Amount = 5;
		info.Level = 1;

		//card.SetInfo (info, data);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
