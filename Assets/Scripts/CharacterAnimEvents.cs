using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimEvents : MonoBehaviour
{
	public void Fire()
	{
		var character = GetComponent<Character> ();
		if (character == null)
			return;
		
		character.Fire ();
	}
}
