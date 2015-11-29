using UnityEngine;
using System.Collections;

public class Atk_Dart : MonoBehaviour {
	public AudioClip[] LaunchSFx;
	public int nLaunchSound;
	AudioSource SourceSFx;
	int power = 10;
	GameObject impactSound;
	// Use this for initialization
	void Start () {

		SourceSFx = this.GetComponent<AudioSource> () ;
		SourceSFx.clip = LaunchSFx [Random.Range (0, nLaunchSound)];
		SourceSFx.Play ();

		impactSound = Instantiate(Resources.Load("impactSound", typeof(GameObject))) as GameObject;
		impactSound = Resources.Load("Dart") as GameObject;
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

	}

	IEnumerator timerDestroy()
	{			
		yield return new WaitForSeconds(2);
		Destroy (this.gameObject);
	}
	
}
