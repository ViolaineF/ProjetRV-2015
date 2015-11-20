using UnityEngine;
using System.Collections;

public class TrapCrystal : MonoBehaviour {

	int power = 5;

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "Player")
		{
			Debug.Log ("coll");
			col.gameObject.GetComponent<Hero_1>().LooseLife(power);
		}
	}
}
