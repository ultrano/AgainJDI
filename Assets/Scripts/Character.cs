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
	private bool preventMovement = false;
	private Animator animator = null;

	void Awake()
	{
		rigidBody = GetComponent<Rigidbody> ();
		animator = GetComponentInChildren<Animator> ();
	}

	void Start ()
	{
		var characterBehaviour = animator.GetBehaviour<CharacterAttackBehaviour> ();
		var characterAnimEvent = GetComponentInChildren<CharacterAnimEvents> ();

		characterBehaviour.characterWR.Target = this;
		characterAnimEvent.characterWR.Target = this;
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
		Debug.Log (velocity);
		animator.SetFloat ("speed", speed);
	}

	public void TurnToDirection(Vector3 direction)
	{
		if (preventMovement)
			return;

		float degree = Mathf.Atan2 (direction.z, direction.x) * Mathf.Rad2Deg - 90;
		transform.localRotation = Quaternion.AngleAxis (degree, Vector3.down);
	}

	public void TurnToRadian(float radian)
	{
		if (preventMovement)
			return;

		transform.localRotation = Quaternion.AngleAxis (radian, Vector3.down);
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
		
		TurnToDirection (fireDirection = direction);

		animator.SetTrigger ("attacking");
	}

	public void Fire()
	{
		var projectile = GameObject.Instantiate (projectilePrefab);
		projectile.Fire (transform.position, fireDirection);
	}

	public void OnBeginAttack()
	{
		animator.SetFloat ("speed", 0);
		rigidBody.velocity = Vector3.zero;
		preventMovement = true;
	}

	public void OnEndAttack()
	{
		Debug.Log ("fsfsffs222");
		preventMovement = false;
	}

	void FixedUpdate()
	{
	}

}
