﻿using UnityEngine;
using System.Collections;

public class SelfRotate : MonoBehaviour {
	
	public float RightSpeed = 2;
	public float UpSpeed = 2;
	public float forwardSeed = 0;

	void Update() {
		transform.Rotate(Vector3.right * 3 * Time.deltaTime * RightSpeed);
		transform.Rotate(Vector3.up * 6 * Time.deltaTime * UpSpeed);
		transform.Rotate(Vector3.forward * 6 * Time.deltaTime * forwardSeed);
	}
}
