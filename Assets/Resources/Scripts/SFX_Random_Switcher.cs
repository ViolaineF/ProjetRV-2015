using UnityEngine;
using System.Collections;

public class SFX_Random_Switcher : MonoBehaviour {

	public AudioClip[] SFx;
	public int Nsound;
	AudioSource SourceSFx;

	void Start () {
		SourceSFx = this.GetComponent<AudioSource> () ;
	}
	
	void Update () {
		StartCoroutine(ChangeSFx());
	}

	void FixedUpdate () {
	}
//	void OnTriggerEnter(Collider Player) {
//		if (Player.gameObject)
//		{	StartCoroutine(ChangeSFx());
//			print("ChangeSFx");
//		}
//	}
	public IEnumerator ChangeSFx()
	{
		if (SourceSFx.isPlaying) {
		yield return new WaitForSeconds (SourceSFx.clip.length);
		}
		if (SourceSFx.isPlaying == false) {
			SourceSFx.clip = SFx [Random.Range (0, Nsound)];
			SourceSFx.Play ();
//			print("ChangeSFx");
		}

	}
}
