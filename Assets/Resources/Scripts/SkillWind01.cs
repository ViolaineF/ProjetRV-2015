using UnityEngine;
using System.Collections;

public class SkillWind01 : MonoBehaviour {

	int power = 10;
	private Enemy01 ennemy01;
	// Use this for initialization
	void Start () {
		StartCoroutine (timerDestroy());


		GameObject thePlayer = GameObject.Find("Hero1");
		Hero_1 playerScript = thePlayer.GetComponent<Hero_1>();
		power = playerScript.m_Strenght;

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

	void Update ()
	{
		this.transform.TransformDirection(Vector3.forward * 10);

	}


	IEnumerator timerDestroy()
	{
		yield return new WaitForSeconds(5);
		Destroy (this.gameObject);
	}
}