using UnityEngine;
using System.Collections;

public class DestroyAtkWood02 : MonoBehaviour {

	public float delay01;
	public float delay02;
	Rigidbody rBody;
	ParticleSystem pEmitter;
	private RaycastHit rcHit;

	void Start () {
		StartCoroutine (timerDestroy());
		rBody = this.GetComponent<Rigidbody>();
		pEmitter = this.GetComponent<ParticleSystem>();
		pEmitter.emissionRate = 0;

	}

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag == "Ground")
		{
			pEmitter.emissionRate = 1;
		}
		else
		{
			pEmitter.emissionRate = 0;
		}
	}


	IEnumerator timerDestroy()
	{			
		yield return new WaitForSeconds(delay01);

		rBody.velocity = Vector3.zero;
		rBody.angularVelocity = Vector3.zero;
		
		//Finally freeze the body in place so forces like gravity or movement won't affect it
		rBody.constraints = RigidbodyConstraints.FreezeAll;
		yield return new WaitForSeconds(delay02);
		Destroy (this.gameObject);
	}
}
