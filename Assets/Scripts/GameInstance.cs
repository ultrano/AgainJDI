using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance
{
	public GameInfoManager GameInfo { get { return gameInfo; } }
	public DataTableManager DataTable { get { return dataTable; } }

	public static GameInstance Instance
	{
		get
		{
			if (instance == null)
				instance = new GameInstance ();
			return instance;
		}
	}

	private GameInfoManager gameInfo = new GameInfoManager();
	private DataTableManager dataTable = new DataTableManager();
	private static GameInstance instance = null;
	private GameInstance()
	{
	}
}
