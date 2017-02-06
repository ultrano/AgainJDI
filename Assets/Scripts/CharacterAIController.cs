using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAIController : Controller {

	public Character character;
	public Character opponent;
	// Use this for initialization
	void Start ()
	{
		InvokeRepeating ("Move", 1, 2);
		InvokeRepeating ("Attack", 1, 4);
	}

	void Move()
	{
		character.MoveToRadian (Random.value * Mathf.PI * 2);
	}

	void Attack()
	{
		var delta = opponent.transform.position - character.transform.position;
		character.ReadyFire (delta.normalized);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
