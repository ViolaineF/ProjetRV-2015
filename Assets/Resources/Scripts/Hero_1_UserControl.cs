﻿using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Hero_1_UserControl : MonoBehaviour
{
	private RaycastHit rcHit;
	private Hero_1 m_Character; // A reference to the ThirdPersonCharacter on the object
	private Transform m_Cam;                  // A reference to the main camera in the scenes transform
	private Vector3 m_CamForward;             // The current forward direction of the camera
	private Vector3 m_Move;
	private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
	private bool m_Atk01;                    // the attack state
	private bool m_Atk02;                   // the skill_1 state
	private bool m_Atk03;                   // the skill_2 state
	private bool m_Posture;                   // the defensive posture and wind shield state

	public GameObject M_PauseMenu;
	Transform target;
	
	public bool RaycastMouse(out RaycastHit hit)
	{
		bool rayHit = false;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if(Physics.Raycast(ray, out hit))
		{
			rayHit = true;
		}
		return rayHit;
	}

	private void Start()
	{
		// get the transform of the main camera
		if (Camera.main != null)
		{
			m_Cam = Camera.main.transform;
		}
		// get the third person character ( this should never be null due to require component )
		m_Character = GetComponent<Hero_1>();
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////


	private void Update()
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			if(RaycastMouse(out rcHit))
			{
				if(rcHit.transform.gameObject.tag == "Enemy")
				{
					Renderer rend = rcHit.transform.GetComponent<Renderer>();
					if(rend != null)
					{
						rend.material.color = Color.red;
//						StartCoroutine (FadeSelection());
					}

					rend = rcHit.transform.GetComponentInChildren<Renderer>();
					if(rend != null)
					{
						rend.material.color = Color.red;
						//						StartCoroutine (FadeSelection());
					}
					target = rcHit.transform;
				}
			}
		}


		if (!m_Jump)
		{
			m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
		}
	}

	// Fixed update is called in sync with physics
	private void FixedUpdate()
	{
		// read inputs
		float h = CrossPlatformInputManager.GetAxis("Horizontal");
//			float v = CrossPlatformInputManager.GetAxis("Vertical");
		bool crouch = Input.GetKey(KeyCode.LeftControl);
		m_Character.m_Target = target;
		m_Atk01 = CrossPlatformInputManager.GetButtonDown("Fire1");
		m_Atk02 = CrossPlatformInputManager.GetButtonDown("Fire2");
		m_Atk03 = CrossPlatformInputManager.GetButtonDown("Fire3");
		m_Posture = CrossPlatformInputManager.GetButtonDown("Defense");

		if (Input.GetKeyDown(KeyCode.P))
		{
			Time.timeScale = 0;
			M_PauseMenu.SetActive(true);
		}

		// calculate move direction to pass to character
		if (m_Cam != null)
		{
			// calculate camera relative direction to move:
			m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
//				m_Move = v*m_CamForward + h*m_Cam.right;
			m_Move = h*m_Cam.right;
		}
		else
		{
			// we use world-relative directions in the case of no main camera
//				m_Move = v*Vector3.forward + h*Vector3.right;
			m_Move = h*Vector3.right;
		}

		// pass all parameters to the character control script
		m_Character.Move(m_Move, crouch, m_Jump, m_Atk01, m_Atk02, m_Atk03, m_Posture);
		m_Jump = false;
	}

	/*
	IEnumerator FadeSelection()
	{
		yield return new WaitForSeconds(5);
		Destroy(this.gameObject);
	}
	*/

}

