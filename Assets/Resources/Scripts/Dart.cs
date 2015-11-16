using UnityEngine;
using System.Collections;

public class Dart : MonoBehaviour {
	public AudioClip[] SFx;
	public int Nsound;
	AudioSource SourceSFx;

	// Use this for initialization
	void Start () {
		SourceSFx = this.GetComponent<AudioSource> () ;
		StartCoroutine (timerDestroy());
	}


	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "Ennemy")
		{
			Destroy(col.gameObject);
			Destroy (this.gameObject);
			SourceSFx.clip = SFx [5];

		}
	}

	IEnumerator timerDestroy()
	{			
		SourceSFx.clip = SFx [Random.Range (0, Nsound)];
		SourceSFx.Play ();
		yield return new WaitForSeconds(2);
		Destroy (this.gameObject);
	}
}
