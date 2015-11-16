using UnityEngine;
using System.Collections;

public class SmoothFollow: MonoBehaviour {
	public Transform target;
	public float smooth= 5.0f;
	public int distance = 10;
	void  Update (){
		transform.position = Vector3.Lerp (
			transform.position, target.position,
			Time.deltaTime * smooth);
	} 
	
} 