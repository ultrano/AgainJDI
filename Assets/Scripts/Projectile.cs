using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	private float lifeTime = 3;

	WeakReference ownerWR = new WeakReference(null);
	public Character Owner { get { return ownerWR.Target as Character; } set{ ownerWR.Target = value; }}

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Fire(Vector3 position, Vector3 direction)
	{
		direction.Normalize ();

		var rigid = GetComponent<Rigidbody> ();
		rigid.velocity = direction * 200;
		transform.position = position;
		transform.rotation = Quaternion.Euler (0, Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg, 0);

		Invoke ("Expire", lifeTime);
	}

	public void Expire()
	{
		GameObject.Destroy (gameObject);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.attachedRigidbody == null)
			return;

		var character = other.attachedRigidbody.GetComponent<Character> ();
		if (character == Owner)
			return;

		character.DecreaseHealth (10);
	}
}
