using UnityEngine;
using System.Collections;

public class E_Atk_01 : MonoBehaviour {

	int power = 10;
	
	// Use this for initialization
	void Start () {
		StartCoroutine (timerDestroy());
	}
	
	
	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.tag == "Player")
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
