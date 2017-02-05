using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimEvents : MonoBehaviour
{
	WeakReference ownerWR = new WeakReference(null);
	public Character Character { get { return ownerWR.Target as Character; } set{ ownerWR.Target = value; }}

	public void Fire()
	{
		if (Character == null)
			return;
		
		Character.Fire ();
	}
}
