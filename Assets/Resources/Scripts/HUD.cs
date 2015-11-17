using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour {


//	explanations on https://www.youtube.com/watch?v=NgftVg3idB4

	public RectTransform healthTransform;
	private float cachedY;
	private float health_minXValue;
	private float health_maxXValue;
	public Text healthText;
	public int currentHealth;
	public int maxHealth;
	public Text visualHealth;

	public GameObject player;
	// Use this for initialization
	void Start () {
		cachedY = healthTransform.position.y;
		health_maxXValue = healthTransform.position.x;
		health_minXValue = healthTransform.position.x - healthTransform.rect.width;
		currentHealth = player.GetComponent<Hero_1>().m_PV;
	}

	private int CurrentHealth
	{
		get{ return currentHealth;}
		set{ currentHealth = value;
			HandleHealth();
		}
	}

	// Update is called once per frame
	void Update () {
		currentHealth = player.GetComponent<Hero_1>().m_PV;
		maxHealth = player.GetComponent<Hero_1>().m_PVmax;
	}

	private void HandleHealth()
	{
		healthText.text = "maxHealth: " + currentHealth;
		float currentXValue = mapValues (currentHealth, 0, maxHealth, health_minXValue, health_maxXValue);
		healthTransform.position = new Vector3 (currentXValue, cachedY);

		if(currentHealth > maxHealth/2) // more than 50% of health
		{
			visualHealth.color = new Color32(255,(byte)mapValues(currentHealth, 0, maxHealth/2 ,0 , 255),0,255);
		}
		else
		{
			visualHealth.color = new Color32(255,(byte)mapValues(currentHealth, 0, maxHealth/2 ,0 , 255),0,255);
		}

		/*
		if(currentHealth > maxHealth/2) // more than 50% of health
		{
			visualHealth.color = new Color32(mapValues(currentHealth,maxHealth/2,255,0),190,255,255);
		}
		else
		{
			visualHealth.color = new Color32(255,mapValues(currentHealth,0,maxHealth/2,0,255),0,255);
		}
		*/

	}


	private float mapValues(float x, float inMin, float inMax, float outMin, float outMax)
	{
		return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}

}
