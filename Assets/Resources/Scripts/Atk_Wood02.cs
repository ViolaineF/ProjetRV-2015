using UnityEngine;
using System.Collections;

public class Atk_Wood02 : MonoBehaviour {
	
	public int power = 1;
	private Enemy01 ennemy01;
	GameObject player;
	Animator anim;

	void Start () {

		player = GameObject.FindGameObjectWithTag("Player").gameObject;
		power = player.GetComponent<Hero_1> ().CheckAtk3pow();
		anim = this.GetComponent<Animator> ();
		StartCoroutine(Grow());
	}
	
	
	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.tag == "Enemy")
		{
			Rigidbody body = col.gameObject.GetComponent<Rigidbody>();
			
			col.gameObject.GetComponent<Enemy01>().Levitate();
			col.gameObject.GetComponent<Enemy01>().LooseLife(power);
		}
	}

	public void UpgradeAttack () {
		
		power = power + 12;
	}

	IEnumerator Grow()
	{
		yield return new WaitForSeconds(5);
		anim.SetTrigger ("StopAnim");
		yield return new WaitForSeconds(2);
		Destroy (this.gameObject);
	}

}