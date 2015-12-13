using UnityEngine;
using System.Collections;

public class Atk_Wood02 : MonoBehaviour {
	
	public int power = 1;
	private Enemy01 ennemy01;
	GameObject player;
	
	void Start () {

		player = GameObject.FindGameObjectWithTag("Player").gameObject;
		power = player.GetComponent<Hero_1> ().CheckAtk3pow();
	}
	
	
	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.tag == "Enemy")
		{
			Rigidbody body = col.gameObject.GetComponent<Rigidbody>();
			
			col.gameObject.GetComponent<Enemy01>().Levitate();
			col.gameObject.GetComponent<Enemy01>().LooseLife(power);

			//			otherObject.GetComponent<ThisHasABoolean>().onOrOff = true;
		}
	}

	public void UpgradeAttack () {
		
		power = power + 12;
	}

}