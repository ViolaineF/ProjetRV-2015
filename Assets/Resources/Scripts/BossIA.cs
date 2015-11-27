using UnityEngine;
using System.Collections;

public class BossIA : MonoBehaviour {

	Animator m_Animator;
	bool m_Death;
	bool m_Dead;
	bool m_Hit;
	bool m_Attacking_1;	
	int m_PV;                         // life amount
	int m_Strenght;                   // Strenght amount
	//public int m_Speed; 
	public GameObject m_Attack_sp;
	public GameObject Death_Particle;
	public GameObject B_Atk_Laser;

	public GameObject B_eyeM;
	public GameObject B_eyeL;
	public GameObject B_eyeR;

	//	m_Speed = m_MoveSpeedMultiplier = 0.8;
	
	public float m_Atk1_stam;                  // stamina of m_Atk1
	public float m_Atk2_stam;                  // stamina of m_Atk2
	public float m_Post_stam;                // stamina of defensive posture and wind shield
	public float m_TimerAtk;					// global timer for attacks


	//-----------------------ATTACKS STATS
	int atk01_chance = 100; // % of chance that the Boss launch this attack
	int atk02_chance = 0;
	Random rnd = new Random(); // generate a new random list
	int atkType; // creates a number between 1 and 100
	int atkCoolDown;
	bool m_IsAttacking;
	Transform target;
	// Use this for initialization



	void Start () {

		target = GameObject.FindGameObjectWithTag("Player").transform;

		m_Animator = GetComponent<Animator>();
		m_PV = 25;
		m_Death = false;
		m_Dead = false;
		m_Hit = false;
		atkCoolDown = 5;

		m_IsAttacking = true;

		StartCoroutine (AttacksIA());
	}

	
	/// <summary>
	/// Updates the animator.
	/// </summary>
	/// <param name="move">Move.</param>
	void UpdateAnimator()
	{
		m_Attacking_1 = false; // Reset the bool for changing anim state so the anim play once
		m_Hit = false;
	}

	// Update is called once per frame
	void Update () {

		if (m_PV > 0) {

			// eyes look at Player
			B_eyeM.transform.LookAt(target.position, Vector3.forward);
			B_eyeL.transform.LookAt(target.position, Vector3.forward);
			B_eyeR.transform.LookAt(target.position, Vector3.forward);
//			this.transform.LookAt(target.position, Vector3.left * Time.deltaTime);

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
		}

		//Check if the Boss is dead
		else if(m_PV <= 0)
		{
			if(m_Dead == true){

			}
			else {
				m_Animator.SetTrigger("T_Death");
				m_Dead = true;
				
				m_IsAttacking = false;
				StopCoroutine(AttacksIA());
			}
		}
		else {
			m_TimerAtk += Time.deltaTime;
		}

		UpdateAnimator();
	}

	/// <summary>
	/// Decrease Boss' life, animate
	/// </summary>
	/// <param name="dammage">Dammage.</param>

	public void LooseLife(int dammage)
	{
		m_PV = m_PV - dammage;
		m_Hit = true;
		m_TimerAtk = 0;
		if(m_PV <= 0)
		{
			GameObject Death_P_Clone;
			Death_P_Clone = Instantiate(Death_Particle, m_Attack_sp.transform.position, m_Attack_sp.transform.rotation) as GameObject;
			m_Animator.SetTrigger("T_Hit");
			
			StartCoroutine(timerDestroy());
		}
	}

	public void AttackCommand()
	{
		if (m_TimerAtk >= 2)
		{	
			m_Attacking_1= true;
			int RandomAtk = Random.Range (0, 2);
			
			if (RandomAtk == 0)
			{
				StartCoroutine(AttacksIA());
			}
			
			else if (RandomAtk == 1)
			{
				StartCoroutine(AttacksIA());
			}
			
			m_Atk1_stam = 0;
			m_TimerAtk = 0;
//			clone.velocity = m_Attack_sp.transform.TransformDirection(Vector3.forward * 10);
		}
	}




	/// <summary>
	/// Manage the attacks, choose, launch, change Boss anim state
	/// </summary>
	/// <returns>The I.</returns>
	IEnumerator AttacksIA(){

		while (m_IsAttacking){
			
			//atkType = rnd.Range (1, 101);
			atkType = Random.Range (0,101);
			
			if(atkType > 0 && atkType <= atk01_chance ){ // launch a type 1 attack
				
				// Instanciate a projectile
				GameObject clone;
				clone = Instantiate(B_Atk_Laser, m_Attack_sp.transform.position, m_Attack_sp.transform.rotation) as GameObject;
				clone.transform.parent = transform;
				m_Animator.SetTrigger("T_Attack01");

				// Change anim state
				m_Attacking_1 = true;
				Debug.Log("attack ! " + Time.time);
			}
			else if(atkType > atk01_chance && atkType < (atk01_chance+atk02_chance)){  // launch a type 2 attack
				m_Animator.SetTrigger("T_Attack02");

			}
			// Wait for the next attack to be launched
			yield return new WaitForSeconds (atkCoolDown);
		}
	}


	IEnumerator timerDestroy()
	{
		yield return new WaitForSeconds(3);
		Destroy(this.gameObject);
	}
	
}
