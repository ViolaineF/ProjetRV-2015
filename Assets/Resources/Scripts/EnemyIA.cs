using System;
using UnityEngine;

public class EnemyIA : MonoBehaviour
{
	public NavMeshAgent agent { get; private set; } // the navmesh agent required for the path finding
	public Enemy01 character { get; private set; } // the character we are controlling
	public Transform target; // target to aim for

	private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
	private bool m_Atk01;                    // the attack state
	private bool m_Atk02;                   // the skill_1 state
	private bool m_Posture;                   // the defensive posture and wind shield state
	private float playerDistance;
	// Use this for initialization

	private void Start()
	{
		// get the components on the object we need ( should not be null due to require component so no need to check )
		agent = GetComponentInChildren<NavMeshAgent>();
		character = GetComponent<Enemy01>();
		
		agent.updateRotation = false;
		agent.updatePosition = true;
	}
	
	
	// Update is called once per frame
	private void Update()
	{
		playerDistance = target.position.x;
		if (target != null)
		{
			agent.SetDestination(target.position);

			if (playerDistance < 3)
			{
				agent.SetDestination(target.position);
				
				// use the values to move the character
				character.AttackCommand();
			}

			else {
				// use the values to move the character
				character.Move(agent.desiredVelocity, m_Jump, m_Atk01, m_Atk02, m_Posture );
			}
		}
		else
		{
			// We still need to call the character's move function, but we send zeroed input as the move param.
			character.Move(Vector3.zero,false, false, false, false);
		}


	}
	
	
	public void SetTarget(Transform target)
	{
		this.target = target;
	}
}

