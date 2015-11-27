﻿using UnityEngine;
using System.Collections;

public class Enemy01 : MonoBehaviour
{
	[SerializeField] float m_MovingTurnSpeed = 360;
	[SerializeField] float m_StationaryTurnSpeed = 180;
	[SerializeField] float m_JumpPower = 12f;
	[Range(1f, 4f)][SerializeField] float m_GravityMultiplier = 2f;
	[SerializeField] float m_RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
	[SerializeField] float m_MoveSpeedMultiplier = 1f;
	[SerializeField] float m_AnimSpeedMultiplier = 1f;
	[SerializeField] float m_GroundCheckDistance = 0.1f;
	
	public GameObject m_Attack_sp;
	public GameObject m_Death_sp;
	public Rigidbody m_Atk_Fx1;
	public GameObject Death_Particle;
	Rigidbody m_Rigidbody;
	Animator m_Animator;
	bool m_IsGrounded;
	float m_OrigGroundCheckDistance;
	const float k_Half = 0.5f;
	float m_TurnAmount;
	float m_ForwardAmount;
	Vector3 m_GroundNormal;
	float m_CapsuleHeight;
	Vector3 m_CapsuleCenter;
	CapsuleCollider m_Capsule;

	bool m_Dead;
	public bool m_Attacking_1;
	public bool m_Attacking_2;
	bool m_Attacking_3;
	bool m_Posture;
	bool m_levit;
	bool m_Hit;
	public int m_PV;                         // life amount
	public int m_Strenght;                   // Strenght amount
//	m_Speed = m_MoveSpeedMultiplier = 0.8;


	public float m_Atk1_stam;                  // stamina of m_Atk1
	public float m_Atk2_stam;                  // stamina of m_Atk2
	public float m_Post_stam;                // stamina of defensive posture and wind shield
	public float m_TimerAtk;					// global timer for attacks

	void Start()
	{
		m_Atk1_stam = 2;
		m_Atk2_stam = 2;
		m_TimerAtk = 2;
		m_Animator = GetComponent<Animator>();
		m_Rigidbody = GetComponent<Rigidbody>();
		m_Capsule = GetComponent<CapsuleCollider>();
		m_CapsuleHeight = m_Capsule.height;
		m_CapsuleCenter = m_Capsule.center;
		
		m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		m_OrigGroundCheckDistance = m_GroundCheckDistance;
		
		m_Atk_Fx1 = Instantiate(Resources.Load("E_Atk_Fx1", typeof(GameObject))) as Rigidbody;
		m_Atk_Fx1 = Resources.Load("E_Atk_Fx1") as Rigidbody;
	}
	
	
	public void Move(Vector3 move, bool jump,  bool m_Pos )
	{
		// convert the world relative moveInput vector into a local-relative
		// turn amount and forward amount required to head in the desired
		// direction.

		if (m_PV > 0) {

			// define a timer to prevent atk spamming between all of them
			m_TimerAtk += Time.deltaTime;	
			
			// define a timer to prevent atk_1 spamming
			if (m_Atk1_stam < 3)
				m_Atk1_stam += Time.deltaTime;	
			else
				m_Atk1_stam = 3;
			
			// define a timer to prevent atk_2 spamming
			if (m_Atk2_stam < 2)	
				m_Atk2_stam += Time.deltaTime;	
			else
				m_Atk2_stam = 3;

			if (move.magnitude > 1f)
				move.Normalize ();
			move = transform.InverseTransformDirection (move);
			CheckGroundStatus ();
			move = Vector3.ProjectOnPlane (move, m_GroundNormal);
			m_TurnAmount = Mathf.Atan2 (move.x, move.z);
			m_ForwardAmount = move.z;
			m_Posture = m_Pos;
			ApplyExtraTurnRotation ();
		
		// control and velocity handling is different when grounded and airborne:

			if (m_IsGrounded) {
				HandleGroundedMovement (jump);
			} 

			else {
				HandleAirborneMovement ();
			}
		// send input and other state parameters to the animator
			UpdateAnimator (move);
			m_Hit = false;		// reset m_Hit
		} 

		else {
			m_TimerAtk += Time.deltaTime;
		}
	}

	/// <summary>
	/// Looses the life.
	/// </summary>
	/// <param name="dammage">Dammage.</param>
	public void LooseLife(int dammage)
	{
		m_PV = m_PV - dammage;
		m_Hit = true;
		m_TimerAtk = 0;
		m_MoveSpeedMultiplier = 0f;
		if(m_PV <= 0)
		{
			GameObject Death_P_Clone;
			Death_P_Clone = Instantiate(Death_Particle, m_Death_sp.transform.position, m_Death_sp.transform.rotation) as GameObject;
			m_Animator.SetTrigger("T_Dead");

			StartCoroutine(timerDestroy());
		}
	}
	
	/// <summary>
	/// Set an attack from the enemy, animation, cooldown...
	/// </summary>
	public void AttackCommand()
	{
		if (m_TimerAtk >= 2)
		{	
			m_Attacking_1= true;
			int RandomAtk = Random.Range (0, 2);

			if (RandomAtk == 0)
			{
				m_Animator.SetTrigger("T_Attack01");
			}

			else if (RandomAtk == 1)
			{
				m_Animator.SetTrigger("T_Attack02");
			}

			m_Atk1_stam = 0;
			m_TimerAtk = 0;
			Rigidbody clone;
			clone = Instantiate(m_Atk_Fx1, m_Attack_sp.transform.position, m_Attack_sp.transform.rotation) as Rigidbody;
//			clone.velocity = m_Attack_sp.transform.TransformDirection(Vector3.forward * 10);
		}
	}


	void UpdateAnimator(Vector3 move)
	{
		// update the animator parameters
		m_Animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
		m_Animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
		m_Animator.SetBool("OnGround", m_IsGrounded);
//		m_Animator.SetBool("Attack01", m_Attacking_1);
//		m_Animator.SetBool("Attack02", m_Attacking_2);
//		m_Animator.SetBool("Posture", m_Posture);
		m_Animator.SetBool("Hit", m_Hit);

		if (!m_IsGrounded)
		{
			m_Animator.SetFloat("Jump", m_Rigidbody.velocity.y);
		}
		
		// calculate which leg is behind, so as to leave that leg trailing in the jump animation
		// (This code is reliant on the specific run cycle offset in our animations,
		// and assumes one leg passes the other at the normalized clip times of 0.0 and 0.5)
		float runCycle =
			Mathf.Repeat(
				m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime + m_RunCycleLegOffset, 1);
		float jumpLeg = (runCycle < k_Half ? 1 : -1) * m_ForwardAmount;
		if (m_IsGrounded && m_TimerAtk >= 1)
		{
			m_Animator.SetFloat("JumpLeg", jumpLeg);
		}


		// the anim speed multiplier allows the overall speed of walking/running to be tweaked in the inspector,
		// which affects the movement speed because of the root motion.
		if (m_IsGrounded && move.magnitude > 0 && m_TimerAtk >= 1)
		{
			m_Animator.speed = m_AnimSpeedMultiplier;
		}

		else
		{
			// don't use that while airborne
			m_Animator.speed = 1;
		}
	}

	void HandleAirborneMovement()
	{
		// apply extra gravity from multiplier:
		Vector3 extraGravityForce = (Physics.gravity * m_GravityMultiplier) - Physics.gravity;
		m_Rigidbody.AddForce(extraGravityForce);
		
		m_GroundCheckDistance = m_Rigidbody.velocity.y < 0 ? m_OrigGroundCheckDistance : 0.01f;
	}
	
	
	void HandleGroundedMovement(bool jump)
	{
		// check whether conditions are right to allow a jump:
		if (jump && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
		{
			// jump!
			m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, m_JumpPower, m_Rigidbody.velocity.z);
			m_IsGrounded = false;
			m_Animator.applyRootMotion = false;
			m_GroundCheckDistance = 0.1f;
		}
	}
	
	void ApplyExtraTurnRotation()
	{
		// help the character turn faster (this is in addition to root rotation in the animation)
		float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
		transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
	}
	
	
	public void OnAnimatorMove()
	{
		// we implement this function to override the default root motion.
		// this allows us to modify the positional speed before it's applied.
		if (m_IsGrounded && m_TimerAtk > 2)
		{
			m_MoveSpeedMultiplier = 0.8f;
			Vector3 v = (m_Animator.deltaPosition * m_MoveSpeedMultiplier) / Time.deltaTime;
			
			// we preserve the existing y part of the current velocity.
			v.y = m_Rigidbody.velocity.y;
			m_Rigidbody.velocity = v;
		}
	}
	
	
	void CheckGroundStatus()
	{
		RaycastHit hitInfo;
		#if UNITY_EDITOR
		// helper to visualise the ground check ray in the scene view
		Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * m_GroundCheckDistance));
		#endif
		// 0.1f is a small offset to start the ray from inside the character
		// it is also good to note that the transform position in the sample assets is at the base of the character
		if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
		{
			m_GroundNormal = hitInfo.normal;
			m_IsGrounded = true;
			m_Animator.applyRootMotion = true;
		}
		else
		{
			m_IsGrounded = false;
			m_GroundNormal = Vector3.up;
			m_Animator.applyRootMotion = false;
		}
	}


	public void Levitate()
	{
		m_Rigidbody.useGravity = false;
		m_Rigidbody.isKinematic = true;
//			StartCoroutine (timerDestroy());
		m_levit = true;
	}

	void Update()
	{

		if (m_levit) {

			m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, m_JumpPower, m_Rigidbody.velocity.z);
			m_IsGrounded = false;
			m_Animator.applyRootMotion = false;
			m_GroundCheckDistance = 0.1f;
		}
	}

	IEnumerator timerDestroy()
	{
		yield return new WaitForSeconds(1);
		Destroy(this.gameObject);
	}
}

