using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuyItemPanel : MonoBehaviour 
{

	public farmManager farmManager;
	private bool inSeed = false;

	void Start () 
	{
		
	}

	public void gotoSeed()
	{

	}
	
	void Update () 
	{
		if(!inSeed)
		{
			if(farmManager.GetFarm() != null)
			{
				this.transform.Find("Type").GetComponent<TextMeshProUGUI>().text = "Comprar a Fazenda: " + farmManager.GetFarm().farmName.ToUpper();
				
				this.transform.Find("MoneyBack").GetComponent<Transform>().Find("MoneyTxt").GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetFloat("Money").ToString("C");

				this.transform.Find("FarmCost").GetComponent<Transform>().Find("MoneyTxt").GetComponent<TextMeshProUGUI>().text = farmManager.GetFarm().cost.ToString("C");

			}
		}


		if(farmManager.GetFarm() != null)
		{
			if(PlayerPrefs.GetFloat("Money")>= farmManager.GetFarm().cost)
			{
				this.transform.Find("BuyItem").GetComponent<Button>().interactable = true;
			}
			if(PlayerPrefs.GetFloat("Money") < farmManager.GetFarm().cost)
			{
				this.transform.Find("BuyItem").GetComponent<Button>().interactable = false;
			}
		}
	}

	public void buyItem()
	{
		PlayerPrefs.SetFloat("Money", PlayerPrefs.GetFloat("Money") - farmManager.GetFarm().cost);
		farmManager.GetFarm().buyed = true;

		string buyedFarmTx = "";

		if(PlayerPrefs.HasKey("buyedFarms"))
		{
			buyedFarmTx = PlayerPrefs.GetString("buyedFarms");
			buyedFarmTx += (","+farmManager.GetFarm().farmName);
		}
		else
		{
			buyedFarmTx = farmManager.GetFarm().farmName;
		}

		PlayerPrefs.SetString("buyedFarms",buyedFarmTx);

		closeMenu();
	}

	public void closeMenu()
	{
		this.gameObject.SetActive(false);
		GameObject.Find("Farm_Selector").GetComponent<farmManager>().Close();
	}
}
