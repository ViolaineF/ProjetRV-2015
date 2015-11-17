using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	// Use this for initialization
	public void Start () {
		PauseGame ();
	}

	public void PauseGame () {
		if (Time.timeScale == 1)
		{
			Time.timeScale = 0;
		}
		else
		{
			Time.timeScale = 1;
			this.gameObject.SetActive (false);
		}
	}
	
	public void StartGame () {
		Application.LoadLevel(1);
		this.gameObject.SetActive (false);
	}

	public void QuitGame () {
		Application.Quit();
	}
	

}
