using UnityEngine;
using System.Collections;

public class DestroyAtkWood02 : MonoBehaviour {

	public float delay01;
	public float delay02;
	Rigidbody rBody;
	public GameObject[] rAtk;
	bool timeUp;
	ParticleSystem pEmitter;
	private RaycastHit rcHit;

	void Start () {
		StartCoroutine (timerDestroy());
		rBody = this.GetComponent<Rigidbody>();
		pEmitter = this.GetComponent<ParticleSystem>();
		pEmitter.emissionRate = 0;
		timeUp = false;
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag == "Ground" && !timeUp)
		{
			pEmitter.emissionRate = 1;
			StartCoroutine(SpawnWood());

		}
		else
		{
			pEmitter.emissionRate = 0;
			StopCoroutine(SpawnWood());
		}
	}

	IEnumerator timerDestroy()
	{			
		yield return new WaitForSeconds(delay01);
		timeUp = true;

		rBody.velocity = Vector3.zero;
		rBody.angularVelocity = Vector3.zero;
		
		//Finally freeze the body in place so forces like gravity or movement won't affect it
		rBody.constraints = RigidbodyConstraints.FreezeAll;
		yield return new WaitForSeconds(delay02);
		Destroy (this.gameObject);
	}

	IEnumerator SpawnWood()
	{
		yield return new WaitForSeconds(0.2f);
		GameObject clone;
//		clone = rAtk [Random.Range (0, nWoodAtk)];
		if(rBody.velocity.x < 1 && rBody.velocity.x > -1)
		{
			StopCoroutine(SpawnWood());
		}
		else
		{
			clone = rAtk [Random.Range (0, rAtk.Length)];
			Vector3 spawnPoint = transform.position;
			Quaternion spawnDir = transform.rotation;
			spawnPoint.y = spawnPoint.y - 0.3f;
			spawnDir.y = Random.Range (0, 180);

			clone = Instantiate(clone, spawnPoint, spawnDir) as GameObject;
			StartCoroutine(SpawnWood());
		}
	}
}
