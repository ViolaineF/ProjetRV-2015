using UnityEngine;
using System.Collections;

public class impactSound : MonoBehaviour {

	public AudioClip[] HitSFx;
	public int nHitSound;
	AudioSource SourceSFx;

	// Use this for initialization
	void Start () {
		StartCoroutine (PlayAndDestroy());
	}


	IEnumerator PlayAndDestroy()
	{					
		SourceSFx.clip = HitSFx [Random.Range (0, nHitSound)];
		SourceSFx.Play ();
		yield return new WaitForSeconds(SourceSFx.clip.length);
		Destroy (this.gameObject);
	}
}
