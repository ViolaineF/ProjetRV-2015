using UnityEngine;
using System.Collections;

public class Bonus : MonoBehaviour {

	public GameObject b_Particles;
	public int BonusType;
	public int BonusPoint = 1;
	// Use this for initialization
	void Start () {
		Renderer rend = transform.GetComponent<Renderer>();

		if(BonusType == 1) //	Strenght bonus
		{
			rend.material.color = new Color32(255, 120, 0, 20); //	fire color
		}
		
		else if(BonusType == 2) //	PV bonus
		{
			rend.material.color = new Color32(255, 120, 0, 20); //	fire color
		}
		
		else if(BonusType == 3) //	Defense bonus
		{
			rend.material.color = new Color32(50, 200, 40, 20); //	wood color
		}

	}

	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.tag == "Player")
		{
			//GameObject thePlayer = GameObject.Find("Hero1");
			Hero_1 playerScript = col.GetComponent<Hero_1>();

			if(BonusType == 1) //	Strenght bonus
			{
				playerScript.m_Strenght = playerScript.m_Strenght + BonusPoint;
			}

			else if(BonusType == 2) //	Strenght bonus
			{
				playerScript.m_PV = playerScript.m_PV + BonusPoint;
			}

			else if(BonusType == 3) //	Strenght bonus
			{
				playerScript.m_Defense = playerScript.m_Defense + BonusPoint;
			}

			else if(BonusType == 4) //	Strenght bonus
			{
				playerScript.m_Xp = playerScript.m_Xp + BonusPoint;
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
