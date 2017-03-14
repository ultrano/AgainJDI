using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo
{
	public GamePlayerInfo    Player = new GamePlayerInfo();
	public Dictionary<int, GameSkillInfo> Skills = new Dictionary<int, GameSkillInfo>();
}
