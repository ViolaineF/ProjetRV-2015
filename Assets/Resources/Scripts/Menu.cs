using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	public AudioClip NewGameSFx;
	public AudioClip QuitSFx;
	public AudioClip PauseSFx;
	AudioSource SourceSFx;


	// Use this for initialization
	public void Start () {
		SourceSFx = this.GetComponent<AudioSource> () ;
		SourceSFx.clip = PauseSFx;
		SourceSFx.Play ();
	}

	public void PauseGame () {
		if (Time.timeScale == 1)
		{
			Time.timeScale = 0;
			SourceSFx.clip = PauseSFx;
			SourceSFx.Play ();
		}
		else
		{
			Time.timeScale = 1;
			SourceSFx.clip = PauseSFx;
			SourceSFx.Play ();
		}
	}
	
	public void StartGame () {
		Application.LoadLevel(1);
		SourceSFx.clip = PauseSFx;
		SourceSFx.Play ();
		StartCoroutine (PlayAndDisable());
	}

	public void QuitGame () {
		StartCoroutine (PlayAndDisable());
		Application.Quit();
	}
	


	IEnumerator PlayAndDisable()
	{					
		yield return new WaitForSeconds(SourceSFx.clip.length);
		this.gameObject.SetActive (false);
	}


}
