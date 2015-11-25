using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD_Health : MonoBehaviour {


//	explanations on https://www.youtube.com/watch?v=NgftVg3idB4

	public RectTransform healthTransform;
	private float cachedY;
	private float minXValue;
	private float maxXValue;
	public Text healthText;
	public int currentHealth;
	public int currentDammage;
	public int CurrentHealth
	{
		get{ return currentHealth;}
		set{ currentHealth = value;
			HandleHealth();
		}
	}
	public int maxHealth;
	public Image visualHealth;
	public GameObject player;

	void Start () {
		cachedY = healthTransform.localPosition.y;
		maxXValue = healthTransform.localPosition.x;
		minXValue = healthTransform.localPosition.x - healthTransform.rect.width;
//		currentHealth = player.GetComponent<Hero_1>().m_PV;
	}

	void Update () {
//		CurrentHealth = player.GetComponent<Hero_1>().m_PV;
	}


	private void HandleHealth()
	{
		healthText.text = "Life : " + currentHealth;
		float currentXValue = mapValues (currentHealth, 0, maxHealth, minXValue, maxXValue);

//		healthTransform.localPosition = new Vector3 (currentXValue, cachedY);
		StartCoroutine(MoveFunction(currentXValue));



		if(currentHealth > maxHealth/2) // more than 50% of health
		{
			visualHealth.color = new Color32((byte)mapValues(currentHealth, maxHealth / 2, maxHealth, 255 , 0), 255, 100, 150);
		}
		else
		{
			visualHealth.color = new Color32(255,(byte)mapValues(currentHealth, 0, maxHealth / 2 ,0 , 255), 100, 150);
		}
	}

	private float mapValues(float x, float inMin, float inMax, float outMin, float outMax)
	{
		return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}


	IEnumerator MoveFunction(float xVal)
	{
		float timeSinceStarted = 0f;
		while (true)
		{
			timeSinceStarted += Time.deltaTime;
			healthTransform.localPosition = Vector3.Lerp(healthTransform.localPosition, new Vector3 (xVal, cachedY), timeSinceStarted);
			
			// If the object has arrived, stop the coroutine
			if (healthTransform.localPosition == new Vector3 (xVal, cachedY))
			{
				yield break;
			}
			
			// Otherwise, continue next frame
			yield return null;
		}
	}


}
