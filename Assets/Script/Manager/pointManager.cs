using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointManager : MonoBehaviour {

	public static pointManager sharedInstance = null;

	void Awake() 
	{
		if (sharedInstance == null)
		{
			sharedInstance = this;
		} 
		else if (sharedInstance != this)
		{
			Destroy (gameObject);  
		}
		DontDestroyOnLoad(gameObject);
	}

	public void setModifier(float modifier)
	{

	}
	
	public void setPoints (float waterValue, float nutrientsValor, float cleaningValor, int modifier) 
	{
		float medianStatus = (waterValue + nutrientsValor + cleaningValor)/3;

		int pointValue = 0;

		if(medianStatus>80)
		{
			pointValue = 4 * modifier;
		}
		else if(medianStatus>60)
		{
			pointValue = 3 * modifier;
		}
		else if(medianStatus>40)
		{
			pointValue = 2 * modifier;
		}
		else if(medianStatus>20)
		{
			pointValue = 1 * modifier;
		}
		else
		{
			pointValue = 0;
		}

		if(PlayerPrefs.HasKey("Points"))
		{
			PlayerPrefs.SetFloat("Points", PlayerPrefs.GetFloat("Points") + pointValue);
		}
		else
			PlayerPrefs.SetFloat("Points", pointValue);

	}
}
