using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerPrefsManager : MonoBehaviour 
{

	TimeManager time_Manager;

	void Start () 
	{
		time_Manager = GetComponent<TimeManager>();
		InvokeRepeating("testPlayerPrefs",4,2f);
	}
	
	// Update is called once per frame
	void testPlayerPrefs () 
	{
		if(PlayerPrefs.HasKey("MinigameChangeCost"))
		{
			DateTime aux = DateTime.Parse(PlayerPrefs.GetString("MinigameChangeCost"));
			if(time_Manager.currentTime()>aux)
			{
				PlayerPrefs.DeleteKey("MinigameChangeCost");
			}
		}
	}
}
