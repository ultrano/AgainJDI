using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Game
{
	public static GameInventoryInfo Inventory { get; private set;}
	public static GamePlayerInfo    PlayerInfo { get; private set;}
	public static GameDataTable     DataTable { get; private set;}
	public static GameAWS           AWS { get; private set;}

	public static void Init(GameAWS _AWS)
	{
		AWS = _AWS;
		Inventory  = new GameInventoryInfo();
		PlayerInfo = new GamePlayerInfo();
		DataTable = new GameDataTable();
		DataTable.Init ();
	}

}
