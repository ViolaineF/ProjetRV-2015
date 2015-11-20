using UnityEngine;
using System.Collections;

public class download : MonoBehaviour {

	public static download instance; // est accessible depuis n'importe quel script

	public Material cubemat;

	// Use this for initialization
	IEnumerator Start () {
	
		instance = this;

		Debug.Log (Time.time);
		//yield return null; // prochaine image
		yield return new WaitForSeconds (2);
		//yield return new WaitForEndOfFrame (); // une fois le reste du script calculé sur l'image, il revient là;
		//StartCoroutine (LogTime ()); // LogTime fonctionne en parallèle
		// yield return StartCoroutine (LogTime ()); // copie colle le script de LogTime
		Debug.Log (Time.time);

	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetMouseButtonDown (0))
			//StartCoroutine (LogTime ());
			StartCoroutine ("DoDownload","http://vegecru.com/res/base/169/image/basilicsacrefeuille.jpg?1327254397294");
			// on ne peut pas écrire juste : LogTime(); car c'est une coroutine qui a besoin de paramètres particuliers
			// Permet d'avoir des instructions parallèles au reste du script, ne s'exécute pas à la suite s'il y a un yield
			// mais ne bloque pas le reste du script avec ce yield
		if (Input.GetMouseButtonDown (1)) {
			StopCoroutine("DoDownload");
		}
	}


	IEnumerator LogTime(){

		Debug.Log ("LogTime : " + Time.time);
		yield return new WaitForSeconds (2);
		Debug.Log ("LogTime : " + Time.time);


	}

	IEnumerator DoDownload(string pLink){
	
		WWW www = new WWW (pLink);

		yield return www; // attendre la fin du téléchargement

		cubemat.mainTexture = www.texture;
	
	}


}
