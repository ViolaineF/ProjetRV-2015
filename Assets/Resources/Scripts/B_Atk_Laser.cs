using UnityEngine;
using System.Collections;

public class B_Atk_Laser : MonoBehaviour {

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
		yield return new WaitForSeconds(3);
		Destroy (this.gameObject);
	}
}
