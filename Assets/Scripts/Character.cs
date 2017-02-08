using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
	public Projectile projectilePrefab = null;
	private Vector3 velocity;
	private Vector3 fireDirection;
	private Rigidbody rigidBody = null;
	private Animator animator = null;
	public CharacterHeadUI headUI = null;
	private bool preventMovement = false;

	private float speed = 40;
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
		
		rigidBody.velocity = velocity = direction.normalized * speed;

		animator.SetFloat ("speed", speed);
	}

	public void MoveToRadian(float radian)
	{
		MoveToDirection (new Vector3(Mathf.Cos (radian), 0, Mathf.Sin (radian)));
	}

	public void TurnToDirection(Vector3 direction)
	{
		TurnToDegree (Mathf.Atan2 (-direction.x, direction.z) * Mathf.Rad2Deg);
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

	public void StartDashMode()
	{
		speed = 100;
		MoveToDirection (velocity.normalized);
		CancelInvoke ("StopDashMode");
		Invoke ("StopDashMode", 0.2f);
	}

	private void StopDashMode()
	{
		speed = 40;
		MoveToDirection (velocity.normalized);
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
		Physics.IgnoreCollision (GetComponent<Collider>(), projectile.GetComponent<Collider>());
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
		{
			animator.SetTrigger ("dying");
			Invoke ("LoadStartScene", 2);
		}
		
		headUI.SetHealth ((float)health / 100.0f);
	}

	private void LoadStartScene()
	{
		SceneManager.LoadScene ("Start");
	}
}
