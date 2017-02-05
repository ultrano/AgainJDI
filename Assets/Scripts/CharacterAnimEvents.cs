using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimEvents : MonoBehaviour
{
	public WeakReference characterWR = new WeakReference(null);

	public void Fire()
	{
		if (!characterWR.IsAlive)
			return;

		var character = characterWR.Target as Character;
		character.Fire ();
	}
}
