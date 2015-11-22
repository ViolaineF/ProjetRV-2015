﻿using System;
using UnityEngine;

public class EnemyIA : MonoBehaviour
{
	public NavMeshAgent agent { get; private set; } // the navmesh agent required for the path finding
	public Enemy01 character { get; private set; } // the character we are controlling
	Transform target; // target to aim for

	private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
//	private bool m_Atk01;                    // the attack state
//	private bool m_Atk02;                   // the skill_1 state
	private bool m_Posture;                   // the defensive posture and wind shield state
	// Use this for initialization

	private void Start()
	{
		// get the components on the object we need ( should not be null due to require component so no need to check )
		agent = GetComponentInChildren<NavMeshAgent>();
		character = GetComponent<Enemy01>();
		agent.updateRotation = false;
		agent.updatePosition = true;
		target = GameObject.FindGameObjectWithTag("Player").transform;
	}

	/// <summary>
	/// Check if the player is close enough to attack
	/// </summary>
	/// <param name="col">Col.</param>
	void OnTriggerStay (Collider col)
	{
		if(col.gameObject.tag == "Player" && character.m_PV > 0)
		{		
			character.AttackCommand();
			character.m_Attacking_1 = true;
		}
		else
		{
			character.m_Attacking_1 = false;
		}
	}
	// Update is called once per frame
	private void Update()
	{		
		if (target != null)
		{				
			agent.SetDestination(target.position);
			character.Move(agent.desiredVelocity, m_Jump, m_Posture );
		}
		else
		{
			// We still need to call the character's move function, but we send zeroed input as the move param.
			character.Move(Vector3.zero,false, false);
		}

	}

	/// <summary>
	/// Reset the enemy destination when the player has disappeared
	/// </summary>
	/// <param name="target">Target.</param>
	public void SetTarget(Transform target)
	{
		this.target = target;
	}
}

