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
public class GameSkillData
{
	public int id;
	public string name;
	public string icon;
	public List<int> levelEffects;

	public int    MaxLevel   { get { return levelEffects.Count + 1; } }
	public Sprite IconSprite
	{
		get
		{
			if (iconSprite.IsAlive == false)
				iconSprite.Target = Resources.Load<Sprite> ("Sprites/SkillIcon/"+icon);

			return iconSprite.Target as Sprite;
		}
	}
	private WeakReference iconSprite = new WeakReference(null);
}

#region Skill elements
[Serializable]
public class GameSkillLevelData
{
	public GameSkillEffectData effect;
	public int maxAmount;
}

[Serializable]
public class GameSkillEffectData
{
	public List<GameSkillEffectPropertyData> properties;
}

[Serializable]
public class GameSkillEffectPropertyData
{
	public int type;
	public List<float> values;
}
#endregion

public class TDataTable<ElementType> : IEnumerable<ElementType>
{
	public List<ElementType> dataList;

	public void Load(string path)
	{
		var json = Resources.Load<TextAsset>(path);
		if (json == null)
			throw new Exception ("Failed to load data table at " + path);

		JsonUtility.FromJsonOverwrite(json.text, this);

		Init ();
	}

	public IEnumerator<ElementType> GetEnumerator()
	{
		return GetSkillEnumerator ();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetSkillEnumerator ();
	}
	private IEnumerator<ElementType> GetSkillEnumerator()
	{
		return dataList.GetEnumerator ();
	}

	virtual protected void Init () {}
}


public class GameSkillDataTable : TDataTable<GameSkillData>
{
	Dictionary<int, int> lookUpDic;
	override protected void Init ()
	{
		int index = dataList.Count;
		while(index-- > 0)
			lookUpDic.Add (dataList [index].id, index);
	}

	public GameSkillData GetById(int Id)
	{
		GameSkillData data;
		if (!TryGetById (Id, out data))
			throw new System.Exception ("Tried to get invalid skill data of id-: " + Id);
		return data;
	}

	public bool TryGetById(int Id, out GameSkillData data)
	{
		int index = -1;
		bool ret = lookUpDic.TryGetValue (Id, out index);
		data = (ret)? data = dataList [index] : null;
		return ret;
	}
}

