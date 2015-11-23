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

	//-----------------------ATTACKS STATS
	int atk01_chance = 100; // % of chance that the Boss launch this attack
	int atk02_chance = 0;
	Random rnd = new Random(); // generate a new random list
	int atkType; // creates a number between 1 and 100
	int atkCoolDown;
	bool m_IsAttacking;

	// Use this for initialization
	void Start () {

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
	/// Decrease Boss' life, animate
	/// </summary>
	/// <param name="dammage">Dammage.</param>
	public void LooseLife(int dammage)
	{
		m_PV = m_PV - dammage;
		m_Hit = true;
	}
	

	/// <summary>
	/// Updates the animator.
	/// </summary>
	/// <param name="move">Move.</param>
	void UpdateAnimator()
	{
		// update the animator parameters
		m_Animator.SetBool("Attack01", m_Attacking_1);
		m_Animator.SetBool("Hit", m_Hit);

		m_Attacking_1 = false; // Reset the bool for changing anim state so the anim play once
		m_Hit = false;
	}

	// Update is called once per frame
	void Update () {

		//Check if the Boss is dead
		if(m_PV <= 0)
		{
			if(m_Dead == true){

				m_Animator.SetBool("Death", false);

			}else {

				m_Animator.SetBool("Death", true);
				m_Dead = true;
				
				m_IsAttacking = false;
				StopCoroutine(AttacksIA());
			}

		}
	
		UpdateAnimator();

		if(Input.GetMouseButtonDown(0)){
			LooseLife(10);
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
				
				// Change anim state
				m_Attacking_1 = true;

				Debug.Log("attack ! " + Time.time);
				
			}
			else if(atkType > atk01_chance && atkType < (atk01_chance+atk02_chance)){  // launch a type 2 attack
				
				
			}

			// Wait for the next attack to be launched
			yield return new WaitForSeconds (atkCoolDown);

		}

	}

}
