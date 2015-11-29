using UnityEngine;
using System.Collections;

public class MovingPlateform : MonoBehaviour {

	public Transform marker1;
	public Transform marker2;
	bool switchMarker;
	public float MaxWaitTime;
	// Use this for initialization
	void Start () {
		switchMarker = false;
	}
	
	// Update is called once per frame
	void Update () {
			StartCoroutine(MoveFunction());
	}






	IEnumerator MoveFunction()
	{
		float timeSinceStarted = 0;
		if(switchMarker)
		{
			timeSinceStarted += Time.deltaTime;
			transform.position = Vector3.Lerp(transform.position, new Vector3 (marker1.position.x, marker1.position.y), timeSinceStarted);

			if(transform.position == marker1.position || timeSinceStarted == MaxWaitTime)
			{
				yield return new WaitForSeconds(2f);
				switchMarker = false;
				timeSinceStarted = 0;
				Debug.Log ("position atteinte");
			}

			yield return null;
		}
			
		else if(!switchMarker)
		{

			timeSinceStarted += Time.deltaTime;
			transform.position = Vector3.Lerp(transform.position, new Vector3 (marker2.position.x, marker2.position.y), timeSinceStarted);


			if(transform.position == marker2.position || timeSinceStarted == MaxWaitTime)
			{
				yield return new WaitForSeconds(2f);
				switchMarker = true;
				timeSinceStarted = 0;
				Debug.Log ("position atteinte");

			}
			yield return null;
		}
			// If the object has arrived, stop the coroutine
			// Otherwise, continue next frame
	}
}
