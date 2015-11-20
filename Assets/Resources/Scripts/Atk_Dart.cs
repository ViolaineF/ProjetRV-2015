using UnityEngine;
using System.Collections;

public class Atk_Dart : MonoBehaviour {
	public AudioClip[] SFx;
	public int Nsound;
	AudioSource SourceSFx;
	int power = 10;

	// Use this for initialization
	void Start () {
		SourceSFx = this.GetComponent<AudioSource> () ;
		SourceSFx.clip = SFx [Random.Range (0, Nsound)];
		SourceSFx.Play ();
		StartCoroutine (timerDestroy());
	}


	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "Enemy")
		{
			col.gameObject.GetComponent<Enemy01>().LooseLife(power);
			Destroy (this.gameObject);
			SourceSFx.clip = SFx [5];
		}
	}

	IEnumerator timerDestroy()
	{			
		yield return new WaitForSeconds(2);
		Destroy (this.gameObject);
	}
}
