using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	public AudioClip NewGameSFx;
	public AudioClip QuitSFx;
	public AudioClip PauseSFx;
	public GameObject skillWind01;
	public GameObject skillWind02;
	public GameObject skillWind03;
	public GameObject skillWind04;
	public GameObject skillWind05;
	public GameObject Player;
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
			this.gameObject.SetActive (false);
		}
	}
	
	public void StartGame () {
		SourceSFx.clip = PauseSFx;
		SourceSFx.Play ();
		Application.LoadLevel(1);
	}

	public void QuitGame () {
		this.gameObject.SetActive (false);
		Application.Quit();
	}
	
	public void UpgradeSkill_1 () {
		this.gameObject.SetActive (false);
		Application.Quit();
	}

	public void UpgradeSkill_2 () {
		this.gameObject.SetActive (false);
		Application.Quit();
	}

	public void UpgradeSkill_3 () {
		this.gameObject.SetActive (false);
		Application.Quit();
	}

	public void UnlockSkill_3 () {
		this.gameObject.SetActive (false);
		Application.Quit();
	}

	public void UnlockSkill_4 () {
		this.gameObject.SetActive (false);
		Application.Quit();
	}
}
