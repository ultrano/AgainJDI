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
	public string icon;
	public List<SkillLevelData> levels;

	public int GetMaxCardAmount(int level)
	{
		int index = level - 1;
		if (index < 0 || index >= levels.Count)
			return 0;
		return levels [index].maxCards;
	}
}
#region Skill elements
[Serializable]
public class SkillLevelData
{
	public SkillEffectData effect;
	public int maxCards;
}

[Serializable]
public class SkillEffectData
{
	public List<SkillEffectPropertyData> properties;
}

[Serializable]
public class SkillEffectPropertyData
{
	public int type;
	public List<float> values;
}
#endregion

public class TDataTable<T> : IEnumerable<T>
{
	public List<T> dataList;
	public IEnumerator<T> GetEnumerator()
	{
		return GetSkillEnumerator ();
	}
	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetSkillEnumerator ();
	}
	IEnumerator<T> GetSkillEnumerator()
	{
		return dataList.GetEnumerator ();
	}
}
