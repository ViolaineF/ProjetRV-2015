using UnityEngine;
using System.Collections;

public class SelfRotate : MonoBehaviour {

	public float speed = 2;

	void Update() {
		transform.Rotate(Vector3.right * 3 * Time.deltaTime * speed);
		transform.Rotate(Vector3.up * 6 * Time.deltaTime * speed, Space.World);
	}
}
