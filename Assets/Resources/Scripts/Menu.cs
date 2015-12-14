using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	public AudioClip NewGameSFx;
	public AudioClip QuitSFx;
	public AudioClip PauseSFx;
	private Hero_1 m_Character; // A reference to the ThirdPersonCharacter on the object

	public GameObject Player;
	
	AudioSource SourceSFx;


	// Use this for initialization
	public void Start () {
		SourceSFx = this.GetComponent<AudioSource> () ;
		SourceSFx.clip = PauseSFx;
		SourceSFx.Play ();
		m_Character = Player.GetComponent<Hero_1>();
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
		Player.GetComponent<Hero_1> ().UpdatePowerAtk1();
	}

	public void UpgradeSkill_2 () {

		Player.GetComponent<Hero_1> ().UpdatePowerAtk2();
	}

	public void UpgradeSkill_3 () {

		Player.GetComponent<Hero_1> ().UpdatePowerAtk3();
	}

	public void UpgradeDefPos () {
		
		Player.GetComponent<Hero_1> ().UpdateDefPos();
	}

	public void UnlockSkill_2 () {
		m_Character.m_Atk_2_unlocked = true;
	}	

	public void UnlockSkill_3 () {
		m_Character.m_Atk_3_unlocked = true;
	}

	public void UnlockDefPos () {
		m_Character.m_DefPos_unlocked = true;
	}

}
