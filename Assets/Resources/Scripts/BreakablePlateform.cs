using UnityEngine;
using System.Collections;

public class BreakablePlateform : MonoBehaviour {

	public Rigidbody p_RigidB_1;
	public Rigidbody p_RigidB_2;
	public Rigidbody p_RigidB_3;
	// Use this for initialization
	void Start () {
	}

	void BreakIt ()
	{
		p_RigidB_1.AddForce (Vector3.down * 1);
		p_RigidB_1.isKinematic = false;		

		p_RigidB_2.AddForce (Vector3.down * 1);
		p_RigidB_2.isKinematic = false;

		p_RigidB_3.AddForce (Vector3.down * 1);
		p_RigidB_3.isKinematic = false;
	}

}
