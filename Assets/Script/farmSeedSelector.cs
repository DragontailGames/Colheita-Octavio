using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class farmSeedSelector : MonoBehaviour 
{
	public GameObject farmSelector;
	public GameObject seedSelector;

	private farmManager farmSelManager;
	private seedManager seedSelManager;

	public GameObject blackPanel;

	public string gameScene;

	void Awake()
	{
		farmSelManager = GameObject.Find("Farm_Selector").GetComponent<farmManager>();
		seedSelManager = GameObject.Find("Seed_Selector").GetComponent<seedManager>();

		if(PlayerPrefs.HasKey("FarmSelected") && PlayerPrefs.HasKey("SeedSelected"))
		{
			blackPanel.SetActive(true);

			foreach(farmType aux in farmSelManager.farmTypes)
			{
				if(aux.farmName == PlayerPrefs.GetString("FarmSelected"))
				{
					farmSelManager.typeFarm = aux;
					gotoSelectSeed();
				}
			}

			foreach(seedType aux in seedSelManager.seedTypes)
			{
				if(aux.seedName == PlayerPrefs.GetString("SeedSelected"))
				{
					seedSelManager.typeSeed = aux;
					gotoGame();
				}
			}
		}
	}

	public void gotoSelectSeed()
	{
		farmSelManager.resetSpecial();
		PlayerPrefs.SetString("FarmSelected",farmSelManager.typeFarm.farmName);
		farmSelector.SetActive(false);
		seedSelector.SetActive(true);
	}

	public void gotoGame()
	{
		blackPanel.SetActive(true);
		PlayerPrefs.SetFloat("Money", PlayerPrefs.GetFloat("Money") - seedSelManager.typeSeed.seedCost);
		PlayerPrefs.SetString("SeedSelected",seedSelManager.typeSeed.seedName);
		GameObject.Find("Loader").GetComponent<loadScene>().gotoLoadScene("Farm prime");
	}
}
