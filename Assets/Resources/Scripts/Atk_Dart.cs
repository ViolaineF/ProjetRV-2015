using UnityEngine;
using System.Collections;

public class Atk_Dart : MonoBehaviour {
	public AudioClip[] launchSFx;
	public int nLaunchSound;
	AudioSource SourceSFx;
	int power = 10;
	public GameObject impactSound;
	// Use this for initialization
	void Start () {

		SourceSFx = this.GetComponent<AudioSource> () ;
		SourceSFx.clip = launchSFx [Random.Range (0, nLaunchSound)];
		SourceSFx.Play ();
		
		StartCoroutine (timerDestroy());
	}


	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "Enemy")
		{
			col.gameObject.GetComponent<Enemy01>().LooseLife(power);
			impactSound = Instantiate(impactSound, transform.position, transform.rotation) as GameObject;
			Destroy (this.gameObject);
		}

		else if(col.gameObject.tag == "Boss")
		{
			col.gameObject.GetComponent<BossIA>().LooseLife(power);
			impactSound = Instantiate(impactSound, transform.position, transform.rotation) as GameObject;
			Destroy (this.gameObject);
		}
		else
		{
			impactSound = Instantiate(impactSound, transform.position, transform.rotation) as GameObject;
			Destroy (this.gameObject);
		}

	}

	IEnumerator timerDestroy()
	{			
		yield return new WaitForSeconds(2);
		Destroy (this.gameObject);
	}
	
}
