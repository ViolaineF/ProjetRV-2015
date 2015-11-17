using UnityEngine;
using System.Collections;

public class SelfRotate : MonoBehaviour {

	void Update() {
		transform.Rotate(Vector3.right * 3 * Time.deltaTime);
		transform.Rotate(Vector3.up * 6 * Time.deltaTime, Space.World);
	}
}
