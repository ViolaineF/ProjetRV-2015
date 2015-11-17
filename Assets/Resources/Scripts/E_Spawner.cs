using UnityEngine;
using System.Collections;

public class E_Spawner : MonoBehaviour {

	public int nb_E1 = 3;
	public int nb_E2 = 0;
	public GameObject E1;
	public GameObject E2;
	public GameObject Spawn;
	// Use this for initialization
	void Start () {
	}
	
	
	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.tag == "Player")
		{
			Debug.Log ("coll");

			for (int i = 0; i < nb_E1; i++)
			{
				GameObject clone;
				clone = Instantiate(E1, Spawn.transform.position, Spawn.transform.rotation) as GameObject;
			//	clone.velocity = transform.TransformDirection(Vector3.forward * 10);
			}

			for (int i = 0; i < nb_E2; i++)
			{
				GameObject clone;
				clone = Instantiate(E2, Spawn.transform.position, Spawn.transform.rotation) as GameObject;
			//	clone.velocity = m_Attack_sp.transform.TransformDirection(Vector3.forward * 10);
			}
			StartCoroutine (timerDestroy());
		}
	}

	IEnumerator SpawnDelay(GameObject type)
	{			
		yield return new WaitForSeconds(1);
		Destroy (this.gameObject);
	}

	IEnumerator timerDestroy()
	{			
		yield return new WaitForSeconds(1);
		Destroy (this.gameObject);
	}
}
