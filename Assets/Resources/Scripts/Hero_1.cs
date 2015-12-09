using UnityEngine;
using System.Collections;

public class Hero_1 : MonoBehaviour
{
	[SerializeField] float m_MovingTurnSpeed = 360;
	[SerializeField] float m_StationaryTurnSpeed = 180;
	[SerializeField] float m_JumpPower = 15f;
	[Range(1f, 4f)][SerializeField] float m_GravityMultiplier = 2f;
	[SerializeField] float m_RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
	[SerializeField] float m_MoveSpeedMultiplier = 1f;
	[SerializeField] float m_AnimSpeedMultiplier = 1f;
	[SerializeField] float m_GroundCheckDistance = 0.1f;

	AudioSource PlayerSFx;
	public AudioClip footStep;
	public AudioClip fx_Hit;
	public AudioClip fx_Dead;
	public AudioClip fx_atk1;
	public AudioClip fx_atk2;
	public GameObject m_Attack1_sp_simple;
	public GameObject m_Attack1_sp_target;
	public GameObject m_Attack2_sp;
	public GameObject m_Attack3_sp;
	public GameObject m_HUD;

	public Rigidbody m_Dart;
	public Rigidbody m_SkillWind01;
	public Rigidbody m_SkillWind02;
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

	bool m_Crouching;
	bool m_Attacking_1;
	bool m_Attacking_2;
	bool m_Attacking_3;
	bool m_Def_Posture;
	float LifeSafetyCooldown;

	// Player stats
	public int m_Xp; // Experience amount
	public int m_Defense; // Defense amount
	public int m_PVmax; // life max
	public int m_PV; // life amount
	public int m_Strenght; // Strenght amount
	public int m_Speed;  
	public float m_Atk1_stam; // stamina of attack_1
	public float m_Atk2_stam; // stamina of attack_2
	public float m_Atk3_stam; // stamina of attack_3
	public float m_Post_stam; // stamina of defensive posture and wind shield
	public float m_TimerAtk; // timer to prevent attack spaming 

	public Transform m_Target; // enemy defined on click to aim attacks on it 
	HUD_Health hud_ScriptHealth;
	HUD_Stamina hud_ScriptStamina;


	void Start()
	{
		hud_ScriptHealth = m_HUD.GetComponent<HUD_Health> ();
		hud_ScriptStamina = m_HUD.GetComponent<HUD_Stamina> ();

		hud_ScriptHealth.CurrentHealth = m_PV;
		hud_ScriptStamina.CurrentStamina1 = m_Atk1_stam;
		hud_ScriptStamina.CurrentStamina2 = m_Atk2_stam;
		hud_ScriptStamina.CurrentStamina3 = m_Atk3_stam;


		m_Animator = GetComponent<Animator>();
		m_Rigidbody = GetComponent<Rigidbody>();
		m_Capsule = GetComponent<CapsuleCollider>();
		m_CapsuleHeight = m_Capsule.height;
		m_CapsuleCenter = m_Capsule.center;
		PlayerSFx = GetComponent<AudioSource>();
		m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		m_OrigGroundCheckDistance = m_GroundCheckDistance;

		m_Dart = Instantiate(Resources.Load("Dart", typeof(GameObject))) as Rigidbody;
		m_Dart = Resources.Load("Dart") as Rigidbody;

		m_SkillWind01 = Instantiate(Resources.Load("Dart", typeof(GameObject))) as Rigidbody;
		m_SkillWind01 = Resources.Load("Dart") as Rigidbody;

		m_PVmax = 100;
		m_PV = 100;
		m_Strenght = 5;
		m_Defense = 2;
		m_Atk1_stam = 9;            
		m_Atk2_stam = 9;               
		m_Atk3_stam = 9;               
		m_Post_stam = 4;
	}

	void Update()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		hud_ScriptStamina.CurrentStamina1 = m_Atk1_stam;
		hud_ScriptStamina.CurrentStamina2 = m_Atk2_stam;
		hud_ScriptStamina.CurrentStamina3 = m_Atk3_stam;
	}

	public void Move(Vector3 move, bool crouch, bool jump, bool m_Atk01, bool m_Atk02, bool m_Atk03, bool m_Posture )
	{
		// convert the world relative moveInput vector into a local-relative
		// turn amount and forward amount required to head in the desired
		// direction.

		if(m_PV > 0){
			// define a timer to prevent atk spamming between all of them
			m_TimerAtk += Time.deltaTime;	
			LifeSafetyCooldown += Time.deltaTime;
			// define a timer to prevent atk_1 spamming
			if (m_Atk1_stam < 9)
				m_Atk1_stam += Time.deltaTime;	
			else
				m_Atk1_stam = 9;

			// define a timer to prevent atk_2 spamming
			if (m_Atk2_stam < 9)	
				m_Atk2_stam += Time.deltaTime;	
			else
				m_Atk2_stam = 9;

			// define a timer to prevent atk_2 spamming
			if (m_Atk3_stam < 9)	
				m_Atk3_stam += Time.deltaTime;	
			else
				m_Atk3_stam = 9;

			if (move.magnitude > 1f)
				move.Normalize ();

			m_Attacking_1 = m_Atk01;
			m_Attacking_2 = m_Atk02;
			m_Attacking_3 = m_Atk03;

			move = transform.InverseTransformDirection (move);
			CheckGroundStatus ();
			move = Vector3.ProjectOnPlane (move, m_GroundNormal);
			m_TurnAmount = Mathf.Atan2 (move.x, move.z);
			m_ForwardAmount = move.z;
			m_Attacking_1 = m_Attacking_1;
			m_Attacking_2 = m_Attacking_2;
			m_Attacking_3 = m_Attacking_3;
			m_Def_Posture = m_Posture;
			ApplyExtraTurnRotation ();
		
			if (m_Attacking_1 && m_Atk1_stam >= 1) {
				AttackCommand_1 ();
			}
			if (m_Attacking_2 && m_Atk2_stam >= 5) {
				AttackCommand_2 ();
			}
			if (m_Attacking_3 && m_Atk3_stam >= 9) {
				m_Attacking_3= true;
				AttackCommand_3 ();
			}

		// control and velocity handling is different when grounded and airborne:
			if (m_IsGrounded) {
				HandleGroundedMovement (crouch, jump);
			} else {
				HandleAirborneMovement ();
			}
			
			ScaleCapsuleForCrouching (crouch);
			PreventStandingInLowHeadroom ();
			
			// send input and other state parameters to the animator
			UpdateAnimator (move);
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////			
		}
		else {
			StartCoroutine(timerDestroy());
		}
	}
	
	public void LooseLife(int dammage)
	{
		if (LifeSafetyCooldown > 1) {
			m_Animator.SetTrigger("Hit");
			m_PV = m_PV - (dammage - m_Defense);
			StartCoroutine(PlaySFx(fx_Hit));

			LifeSafetyCooldown = 0;

			if(m_PV <= 0)
			{
				m_Animator.SetTrigger("Dead");
				StartCoroutine(PlaySFx(fx_Dead));
			}

//			float m_PV_float = (float)m_PV;
			hud_ScriptHealth.currentDammage = dammage;
			hud_ScriptHealth.CurrentHealth = m_PV;
		}
	}

	public void GetLife(int cure)
	{
		m_PV = m_PV + cure;
		if (m_PV > m_PVmax)
			m_PV = m_PVmax;
//		float m_PV_float = (float)m_PV;
		hud_ScriptHealth.CurrentHealth = m_PV;
	}


	void ScaleCapsuleForCrouching(bool crouch)
	{
		if (m_IsGrounded && crouch)
		{
			if (m_Crouching) return;
			m_Capsule.height = m_Capsule.height / 2f;
			m_Capsule.center = m_Capsule.center / 2f;
			m_Crouching = true;
		}
		else
		{
			Ray crouchRay = new Ray(m_Rigidbody.position + Vector3.up * m_Capsule.radius * k_Half, Vector3.up);
			float crouchRayLength = m_CapsuleHeight - m_Capsule.radius * k_Half;
			if (Physics.SphereCast(crouchRay, m_Capsule.radius * k_Half, crouchRayLength))
			{
				m_Crouching = true;
				return;
			}
			m_Capsule.height = m_CapsuleHeight;
			m_Capsule.center = m_CapsuleCenter;
			m_Crouching = false;
		}
	}
	
	void PreventStandingInLowHeadroom()
	{
		// prevent standing up in crouch-only zones
		if (!m_Crouching)
		{
			Ray crouchRay = new Ray(m_Rigidbody.position + Vector3.up * m_Capsule.radius * k_Half, Vector3.up);
			float crouchRayLength = m_CapsuleHeight - m_Capsule.radius * k_Half;
			if (Physics.SphereCast(crouchRay, m_Capsule.radius * k_Half, crouchRayLength))
			{
				m_Crouching = true;
			}
		}
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	void AttackCommand_1()
	{			
		if (!m_Crouching && m_TimerAtk >= 0.5f)
		{
			m_Atk1_stam = m_Atk1_stam - 1;
			hud_ScriptStamina.CurrentStamina1 = m_Atk1_stam;
			m_TimerAtk = 0;

			Rigidbody clone;
			if (m_Target != null && m_Target.gameObject.tag == "Enemy" || m_Target != null && m_Target.gameObject.tag == "Boss")
			{
				// instantiate a projectile to the target

				Vector3 targetDir = (m_Target.transform.position - transform.position);
				if(targetDir.x < 0)
				{
					clone = Instantiate(m_Dart, m_Attack1_sp_target.transform.position, m_Attack1_sp_target.transform.rotation) as Rigidbody;
					clone.velocity = m_Attack1_sp_target.transform.TransformDirection(targetDir * 2);
				}
				else if(targetDir.x > 0)
				{
					targetDir.x = -targetDir.x;
					clone = Instantiate(m_Dart, m_Attack1_sp_target.transform.position, m_Attack1_sp_target.transform.rotation) as Rigidbody;
					clone.velocity = m_Attack1_sp_target.transform.TransformDirection(targetDir * 2);
				}
				m_Target = null;
			}
			else if (m_Target == null)
			{
				// instantiate a projectile forward

				clone = Instantiate(m_Dart, m_Attack1_sp_simple.transform.position, m_Attack1_sp_simple.transform.rotation) as Rigidbody;
				clone.velocity = m_Attack1_sp_simple.transform.TransformDirection(Vector3.forward * 10);
			}

			StartCoroutine(PlaySFx(fx_atk1));

		}
	}


	void AttackCommand_2()
	{
		if (!m_Crouching && m_TimerAtk >= 1)
		{
			m_Atk2_stam = m_Atk2_stam - 5;
			hud_ScriptStamina.CurrentStamina2 = m_Atk2_stam;
			m_TimerAtk = 0;

			// instantiate a projectile
			Rigidbody clone;
			clone = Instantiate(m_SkillWind01, m_Attack2_sp.transform.position, m_Attack2_sp.transform.rotation) as Rigidbody;
			clone.velocity = m_Attack2_sp.transform.TransformDirection(Vector3.forward * 10);

			StartCoroutine(PlaySFx(fx_atk2));

		}
	}

	void AttackCommand_3()
	{
		// prevent standing up in crouch-only zones
		if (!m_Crouching)
		{
			Rigidbody clone;
			clone = Instantiate(m_SkillWind02, m_Attack3_sp.transform.position, m_Attack3_sp.transform.rotation) as Rigidbody;
			
			clone.velocity = m_Attack3_sp.transform.TransformDirection(Vector3.forward * 10);
		}
	}

	void UpdateAnimator(Vector3 move)
	{
		// update the animator parameters
		m_Animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
		m_Animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
		m_Animator.SetBool("Crouch", m_Crouching);
		m_Animator.SetBool("OnGround", m_IsGrounded);

		m_Animator.SetBool("Attack01", m_Attacking_1);
		m_Animator.SetBool("Attack02", m_Attacking_2);
		m_Animator.SetBool("Attack03", m_Attacking_3);
		m_Animator.SetBool("Posture", m_Def_Posture);

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

		if (m_IsGrounded)
		{
			m_Animator.SetFloat("JumpLeg", jumpLeg);
		}
		/*
		// the anim speed multiplier allows the overall speed of walking/running to be tweaked in the inspector,
		// which affects the movement speed because of the root motion.
		if (m_IsGrounded && move.magnitude > 0)
		{
			m_Animator.speed = m_AnimSpeedMultiplier;
		}
		else
		{
			// don't use that while airborne
			m_Animator.speed = 1;
		}
		*/
	}
	
	
	void HandleAirborneMovement()
	{
		// apply extra gravity from multiplier:
		Vector3 extraGravityForce = (Physics.gravity * m_GravityMultiplier) - Physics.gravity;
		m_Rigidbody.AddForce(extraGravityForce);
		
		m_GroundCheckDistance = m_Rigidbody.velocity.y < 0 ? m_OrigGroundCheckDistance : 0.01f;
	}
	
	
	void HandleGroundedMovement(bool crouch, bool jump)
	{
		// check whether conditions are right to allow a jump:
		if (jump && !crouch && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
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
		if (m_IsGrounded && Time.deltaTime > 0)
		{
			Vector3 v = (m_Animator.deltaPosition * m_MoveSpeedMultiplier * m_Speed) / Time.deltaTime;

			// we preserve the existing y part of the current velocity.
//			v.y = m_Rigidbody.velocity.y;
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

/*
			PlayerSFx.clip = footStep;
			if(!PlayerSFx.isPlaying)
			{
				PlayerSFx.Play();
			}	
*/
		}
		else
		{
			m_IsGrounded = false;
			m_GroundNormal = Vector3.up;
			m_Animator.applyRootMotion = false;
		}
	}


	// coroutines //

	IEnumerator timerDestroy()
	{
		m_Animator.SetBool("Dead", true);
		yield return new WaitForSeconds(5);
		Destroy(this.gameObject);
	}

	// Play sound during its length //
	IEnumerator PlaySFx(AudioClip soundFx) {

		PlayerSFx.clip = soundFx;
		PlayerSFx.Play();
		yield return new WaitForSeconds(PlayerSFx.clip.length);
	}

	/*
	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "Ground")
		{
			m_IsGrounded = true;
			m_Animator.applyRootMotion = true;
			PlayerSFx.clip = FootStep;
			if(!PlayerSFx.isPlaying)
			{
				PlayerSFx.Play();
			}	
		}
		else
		{
			m_IsGrounded = false;
			m_GroundNormal = Vector3.up;
			m_Animator.applyRootMotion = false;
		}

	}
	*/

}

