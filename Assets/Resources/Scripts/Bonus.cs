using UnityEngine;
using System.Collections;

public class Bonus : MonoBehaviour {

	public GameObject b_Particles;
	public int BonusType;
	public int BonusPoint = 1;
	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.tag == "Player")
		{
			//GameObject thePlayer = GameObject.Find("Hero1");
			Hero_1 playerScript = col.GetComponent<Hero_1>();

			if(BonusType == 1)
			{
				playerScript.m_Strenght = playerScript.m_Strenght + BonusPoint;
			}

			else if(BonusType == 2)
			{
				playerScript.m_PV = playerScript.m_PV + BonusPoint;
			}

			else if(BonusType == 3)
			{
				playerScript.m_Speed = playerScript.m_Speed + BonusPoint/10;
			}
			Rigidbody clone;
			clone = Instantiate(b_Particles, transform.position, transform.rotation) as Rigidbody;
			
			//			transform.position = Vector3.MoveTowards(transform.position, m_Attack_sp.transform.position, Time.deltaTime * 50f);
			Destroy (this.gameObject);

		}
		else
		{
		}
		
	}
}
