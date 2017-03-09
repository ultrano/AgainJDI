using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game
{
	public static GameInventory Inventory { get; private set;}
	public static GameAccountInfo AccountInfo { get; private set;}
	public static GameDataTable DataTable { get; private set;}

	public static void Init()
	{
		Inventory  = new GameInventory();
		AccountInfo = new GameAccountInfo();
		DataTable = new GameDataTable();
		DataTable.Init ();
	}

}
