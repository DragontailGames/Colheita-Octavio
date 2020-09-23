using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class farmManager : MonoBehaviour 
{
	public farmType[] farmTypes;
	public farmType typeFarm;

	[Header("Farms UI")]
	public GameObject[] farmGO;

	public static farmManager sharedInstance = null;

	public Button selectFarm;

	public GameObject buyBox;


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

	void Start()
	{
		if(PlayerPrefs.HasKey("buyedFarms"))
		{
			string[] buyedFarmTx = PlayerPrefs.GetString("buyedFarms").Split(',');
			if(buyedFarmTx.Length ==0)
			{
				buyedFarmTx[0] = PlayerPrefs.GetString("buyedFarms");
			}
			
			foreach(var temp in buyedFarmTx)
			{
				foreach(var aux in farmTypes)
				{
					if(temp == aux.farmName)
					{
						aux.buyed = true;
					}
				}
			}
		}

		for(int c = 0; c<4; c++)
			setUiText(c);
	}

	void setUiText(int index)
	{
		TextMeshProUGUI name = farmGO[index].transform.Find("Name").GetComponent<TextMeshProUGUI>();
		name.text = farmTypes[index].farmName;

		TextMeshProUGUI cost = farmGO[index].transform.Find("Cost").GetComponent<TextMeshProUGUI>();
		cost.text = "$" + farmTypes[index].cost +".00";

		if(farmTypes[index].buyed)
		{
			cost.transform.gameObject.SetActive(false);
		}

		Image icon;
		for(int c = 0;c<3;c++)
		{
			if(farmTypes[index].cleaningHours != farmTypes[index].waterHours && 
			farmTypes[index].cleaningHours != farmTypes[index].nutrientsHours)
			{
				icon = farmGO[index].transform.Find("IconPanel").GetComponent<Transform>().Find("icon_Cleaning").GetComponent<Image>();
				icon.color = Color.red;
			}

			if(farmTypes[index].cleaningHours != farmTypes[index].waterHours && 
			farmTypes[index].waterHours != farmTypes[index].nutrientsHours)
			{
				icon = farmGO[index].transform.Find("IconPanel").GetComponent<Transform>().Find("icon_Water").GetComponent<Image>();
				icon.color = Color.red;
			}

			if(farmTypes[index].nutrientsHours != farmTypes[index].waterHours && 
			farmTypes[index].cleaningHours != farmTypes[index].nutrientsHours)
			{
				icon = farmGO[index].transform.Find("IconPanel").GetComponent<Transform>().Find("icon_Nutrients").GetComponent<Image>();
				icon.color = Color.red;
			}
		}
	}

	bool resetBool = false;

	void Update()
	{
		if(selectFarm!= null)
		{
			if(typeFarm != null)
			{
				selectFarm.interactable = typeFarm.buyed;
			}

			if(typeFarm == null)
			{
				selectFarm.interactable = false;
			}

			if(farmGO[0] != null)
			{

				for(int c = 0;c<4;c++)
				{
					if(farmTypes[c].buyed)
					{
						farmGO[c].transform.Find("Cost").GetComponent<Transform>().gameObject.SetActive(false);
					}

					if(typeFarm != null)
					{
						if(typeFarm == farmTypes[c])
						{
							farmGO[c].transform.Find("SelectFarm").GetComponent<Image>().enabled = true;
						}
						else
						{
							farmGO[c].transform.Find("SelectFarm").GetComponent<Image>().enabled = false;
						}
					}
				}
			}
		}
	}

	bool overMenuOpen = false;

	public void openMenuOrSelect()
	{
		if(!typeFarm.buyed)
		{
			overMenuOpen = true;
			selectFarm.interactable = true;
			buyBox.SetActive(true);
		}
	}

	public void Close()
	{
		overMenuOpen = false;
	}

	public farmType[] getFarms()
	{
		return farmTypes;
	}

	public farmType GetFarm()
	{
		return typeFarm;
	}

	public void resetSpecial()
	{
		resetBool = true;
	}

	public void setFarmOne()
	{
		if(!overMenuOpen)
			typeFarm = farmTypes[0];
	}

	public void setFarmTwo()
	{
		if(!overMenuOpen)
			typeFarm = farmTypes[1];
	}

	public void setFarmTree()
	{
		if(!overMenuOpen)
			typeFarm = farmTypes[2];
	}

	public void setFarmFour()
	{
		if(!overMenuOpen)
			typeFarm = farmTypes[3];
	}
}
