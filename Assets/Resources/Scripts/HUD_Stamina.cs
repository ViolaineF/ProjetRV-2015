using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD_Stamina : MonoBehaviour {

	//	explanations on https://www.youtube.com/watch?v=NgftVg3idB4
	
	public RectTransform stam1Transform;
	public RectTransform stam2Transform;
	public RectTransform stam3Transform;
	private float s1cachedY;
	private float s2cachedY;
	private float s3cachedY;
	private float s1minXValue;
	private float s2minXValue;
	private float s3minXValue;
	private float s1maxXValue;
	private float s2maxXValue;
	private float s3maxXValue;
	public float currentStamina1;
	public float currentStamina2;
	public float currentStamina3;

	public float CurrentStamina1
	{
		get{ return currentStamina1;}
		set{ currentStamina1 = value;
			HandleStamina();
		}
	}
	public float CurrentStamina2
	{
		get{ return currentStamina2;}
		set{ currentStamina2 = value;
			HandleStamina();
		}
	}
	public float CurrentStamina3
	{
		get{ return currentStamina3;}
		set{ currentStamina3 = value;
			HandleStamina();
		}
	}

	public int maxStam1;
	public int maxStam2;
	public int maxStam3;
	public Image visualStam1;
	public Image visualStam2;
	public Image visualStam3;
	public GameObject player;
	
	void Start () {

		s1cachedY = stam1Transform.localPosition.y;
		s2cachedY = stam2Transform.localPosition.y;
		s3cachedY = stam3Transform.localPosition.y;
		s1maxXValue = stam1Transform.localPosition.x;
		s2maxXValue = stam2Transform.localPosition.x;
		s3maxXValue = stam3Transform.localPosition.x;
		s1minXValue = stam1Transform.localPosition.x - stam1Transform.rect.width;
		s2minXValue = stam2Transform.localPosition.x - stam2Transform.rect.width;
		s3minXValue = stam3Transform.localPosition.x - stam3Transform.rect.width;
		//		currentHealth = player.GetComponent<Hero_1>().m_PV;
	}
	
	void Update () {
		//		CurrentHealth = player.GetComponent<Hero_1>().m_PV;
	}
	
	
	private void HandleStamina()
	{
		float curentStam1XValue = mapValues (currentStamina1, 0, maxStam1, s1minXValue, s1maxXValue);
		float curentStam2XValue = mapValues (currentStamina2, 0, maxStam2, s2minXValue, s2maxXValue);
		float curentStam3XValue = mapValues (currentStamina3, 0, maxStam3, s3minXValue, s3maxXValue);

		//		healthTransform.localPosition = new Vector3 (currentXValue, cachedY);
		StartCoroutine(MoveFunction(curentStam1XValue, stam1Transform, s1cachedY));
		StartCoroutine(MoveFunction(curentStam2XValue, stam2Transform, s2cachedY));
		StartCoroutine(MoveFunction(curentStam3XValue, stam3Transform, s3cachedY));
	}
	
	private float mapValues(float x, float inMin, float inMax, float outMin, float outMax)
	{
		return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}
	
	
	IEnumerator MoveFunction(float xVal, RectTransform sTransform, float cachedY)
	{
		float timeSinceStarted = 0f;
		while (true)
		{
			timeSinceStarted += Time.deltaTime;
			sTransform.localPosition = Vector3.Lerp(sTransform.localPosition, new Vector3 (xVal, cachedY), timeSinceStarted);

			// If the object has arrived, stop the coroutine
			if (sTransform.localPosition == new Vector3 (xVal, cachedY))
			{
				yield break;
			}
			// Otherwise, continue next frame
			yield return null;
		}
	}
	
	
}
