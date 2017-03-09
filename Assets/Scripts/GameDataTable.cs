using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataTable
{
	public void Init()
	{
		Skill = LoadDataTable<SkillData> ("DataTable/skill");
	}

	public TDataTable<SkillData> Skill {
		get;
		private set;
	}

	private TDataTable<T> LoadDataTable<T>(string path)
	{
		var json = Resources.Load<TextAsset>(path);
		if (json == null)
			throw new Exception ("Failed to load data table at " + path);

		var dataTable = JsonUtility.FromJson<TDataTable<T>> (json.text);
		if (dataTable == null)
			throw new Exception ("incorrect data table format with " + path);

		return dataTable;
	}
}