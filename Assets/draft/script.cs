using UnityEngine;
using System.Collections;


public class script : MonoBehaviour {

	private Animator anim;

	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator> ();
		if (anim == null)
			Debug.LogError ("Animator not found !");
	
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetMouseButtonDown(0)){
			anim.SetTrigger("death");
		}

		if(Input.GetMouseButtonDown(1)){ //right click
			anim.SetLayerWeight(2,1);
		}
        
	
	}
}
