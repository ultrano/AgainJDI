using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game
{
	public static GameInfo  Info { get; set; }
	public static GameData  Data { get; set;}
	public static GameNet   Net { get; set;}

	public static void Init()
	{
		Info = new GameInfo ();
		Data = new GameData ();
		Net  = new GameNet ();
	}
}
