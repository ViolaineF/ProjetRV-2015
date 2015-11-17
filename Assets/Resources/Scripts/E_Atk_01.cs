using UnityEngine;
using System.Collections;

public class E_Atk_01 : MonoBehaviour {

	int power = 10;
	
	// Use this for initialization
	void Start () {
		StartCoroutine (timerDestroy());
	}
	
	
	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.name == "Player")
		{
			Debug.Log ("coll");
			col.gameObject.GetComponent<Hero_1>().LooseLife(power);
		}
	}
	
	IEnumerator timerDestroy()
	{			
		yield return new WaitForSeconds(1);
		Destroy (this.gameObject);
	}
}
