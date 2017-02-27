using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
skill data
-skill level data

skill effect data
-skill effect property data
*/


[Serializable]
public class SkillData
{
	public int id;
	public string name;
	public List<SkillLevelData> levelEffect;

	public int GetMaxCardAmount(int level)
	{
		int index = level - 1;
		if (index < 0 || index >= levelEffect.Count)
			return 0;
		return levelEffect [index].maxCards;
	}
}

[Serializable]
public class SkillLevelData
{
	public int effectID;
	public int maxCards;
}

[Serializable]
public class SkillEffectData
{
	public int id;
	public string name;
	public List<SkillEffectPropertyData> properties;
}

[Serializable]
public class SkillEffectPropertyData
{
	public int type;
	public List<float> values;
}


public class DataTableManager
{
	public Dictionary<int, SkillData> skillDataDic = new Dictionary<int, SkillData>();

	SkillData GetSkillData(int skillDataID)
	{
		SkillData skillData; 
		if (skillDataDic.TryGetValue (skillDataID, out skillData))
			Debug.Log ("finding skill data failed");
		return skillData;
	}
}