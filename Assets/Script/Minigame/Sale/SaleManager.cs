using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaleManager : MonoBehaviour 
{

	public GameObject coffeSalePrefab;
	public Transform coffeStartPosition;

	float coffePoints;
	public float valueForCoffePoints = 125;

	void Start ()
	{
		createCoffeSack();
		createCoffeSack();
		createCoffeSack();
		createCoffeSack();
		createCoffeSack();
	}
	
	public void createCoffeSack()
	{
		Instantiate(coffeSalePrefab,coffeStartPosition.position,Quaternion.identity);
	}

	public void addCoffePoints()
	{
		coffePoints++;
	}

	void Update()
	{
		if(GetComponent<MinigameManager>().testEndPuzzle())
		{
			GetComponent<MinigameManager>().calculateReward(valueForCoffePoints,coffePoints);
		}
	}

	public void saveCicle()
	{
		if(PlayerPrefs.HasKey("Points"))
		{
			if(PlayerPrefs.HasKey("CiclePoint"))
			{
				PlayerPrefs.SetString("CiclePoint", PlayerPrefs.GetString("CiclePoint") + "," + PlayerPrefs.GetFloat("Points") + " pontos.");
			}
			else
			{
				PlayerPrefs.SetString("CiclePoint", PlayerPrefs.GetFloat("Points").ToString());
			}

			string ciclePointSave = PlayerPrefs.GetString("CiclePoint");
			float moneySave = PlayerPrefs.GetFloat("Money");
			string buyedItens = "";
			string entranceOnlineSave = PlayerPrefs.GetString("entranceOnline");
			if(PlayerPrefs.HasKey("buyedItensId"))
				buyedItens = PlayerPrefs.GetString("buyedItensId");

			PlayerPrefs.DeleteAll();

			PlayerPrefs.SetString("CiclePoint", ciclePointSave);
			PlayerPrefs.SetString("buyedItensId", buyedItens);
			PlayerPrefs.SetString("entranceOnline", entranceOnlineSave);
			PlayerPrefs.SetString("TutorialEnd", "...");
			PlayerPrefs.SetFloat("Money", moneySave);

			SceneManager.LoadScene(0);
			

		}
	}
}
