using UnityEngine;
using System.Collections;

public class SelfDestroy : MonoBehaviour {

	public float delay;

	void Start () {
		StartCoroutine (timerDestroy());
	}
	
	IEnumerator timerDestroy()
	{			
		yield return new WaitForSeconds(delay);
		Destroy (this.gameObject);
	}
}
