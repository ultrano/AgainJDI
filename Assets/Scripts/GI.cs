using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GI
{
	public static GameInfoManager GameInfo { get { return gameInfo; } }
	public static DataTableManager DataTable { get { return dataTable; } }

	public void Init()
	{
		gameInfo  = new GameInfoManager();
		dataTable = new DataTableManager();
	}

	private static  GameInfoManager gameInfo  = null;
	private static DataTableManager dataTable = null;
}
