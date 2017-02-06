using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	public Projectile projectilePrefab = null;
	private Vector3 velocity;
	private Vector3 fireDirection;
	private Rigidbody rigidBody = null;
	private Animator animator = null;
	public CharacterHeadUI headUI = null;
	private bool preventMovement = false;

	private int health = 100;
	public int Health { get { return health; } }

	void Awake()
	{
		rigidBody = GetComponent<Rigidbody> ();
		animator = GetComponentInChildren<Animator> ();

		headUI.Character = this;
	}

	void Start ()
	{
	}

	public void MoveToDirection(Vector3 direction)
	{
		if (preventMovement)
			return;
		
		float speed = 40;
		rigidBody.velocity = velocity = direction.normalized * speed;

		animator.SetFloat ("speed", speed);
	}

	public void MoveToRadian(float radian)
	{
		if (preventMovement)
			return;

		float speed = 40;
		velocity.x = Mathf.Cos (radian) * speed;
		velocity.z = Mathf.Sin (radian) * speed;
		velocity.y = 0;

		rigidBody.velocity = velocity;
		animator.SetFloat ("speed", speed);
	}

	public void TurnToDirection(Vector3 direction)
	{
		if (preventMovement)
			return;

		float degree = Mathf.Atan2 (-direction.x, direction.z) * Mathf.Rad2Deg;
		TurnToDegree (degree);
	}

	public void TurnToDegree(float degree)
	{
		if (preventMovement)
			return;

		transform.localRotation = Quaternion.AngleAxis (degree, Vector3.down);
	}

	public void StopMovement()
	{
		rigidBody.velocity = velocity = Vector3.zero;

		animator.SetFloat ("speed", 0);
	}

	public void ReadyFire(Vector3 direction)
	{
		if (preventMovement)
			return;
		
		TurnToDirection ((fireDirection = direction));

		animator.SetTrigger ("attacking");
	}

	public void Fire()
	{
		var projectile = GameObject.Instantiate (projectilePrefab);
		projectile.Owner = this;
		projectile.Fire (transform.position, fireDirection);
	}

	public void PauseMovement()
	{
		animator.SetFloat ("speed", 0);
		rigidBody.velocity = Vector3.zero;
		preventMovement = true;
	}

	public void ResumeMovement()
	{
		animator.SetFloat ("speed", velocity.sqrMagnitude);
		rigidBody.velocity = velocity;
		preventMovement = false;
	}

	public void DecreaseHealth(int amount)
	{
		if (amount < 0)
			return;
		if (health <= 0)
			return;

		health = Mathf.Clamp ((health - amount), 0, 100);
		if (health > 0)
			animator.SetTrigger ("damaging");
		else
			animator.SetTrigger ("dying");
		
		headUI.SetHealth ((float)health / 100.0f);
	}

}
