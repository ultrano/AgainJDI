using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
	public void Init()
	{
		Skill = new GameSkillDataTable ();
		Skill.Load("DataTable/skill");
	}

	public GameSkillDataTable Skill {
		get;
		private set;
	}
}