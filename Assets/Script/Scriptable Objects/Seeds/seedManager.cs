using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class seedManager : MonoBehaviour 
{

	public seedType[] seedTypes;
	public seedType typeSeed;

	[Header("Seeds UI")]
	public GameObject[] seedGO;

	public static seedManager sharedInstance = null;

	public Button startCycle;

	public TextMeshProUGUI description;

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
		for(int c = 0; c<4; c++)
			setUiText(c);
	}

	public GameObject noMoney;
	public TextMeshProUGUI actualMoney;

	void Update()
	{
		if(startCycle != null)
		{
			if(typeSeed != null)
			{
				if(noMoney != null)
				{
					if(typeSeed.seedCost > PlayerPrefs.GetFloat("Money"))
					{
						startCycle.interactable = false;
						noMoney.SetActive(true);
					}
					if(typeSeed.seedCost <= PlayerPrefs.GetFloat("Money"))
					{
						startCycle.interactable = true;
						noMoney.SetActive(false);
					}
					description.text = typeSeed.description;

					for(int c = 0;c<4;c++)
					{
						if(typeSeed == seedTypes[c])
						{
							seedGO[c].transform.Find("SelectSeed").GetComponent<Image>().enabled = true;
						}
						else
						{
							seedGO[c].transform.Find("SelectSeed").GetComponent<Image>().enabled = false;
						}
					}
				}
			}

			if(typeSeed == null)
			{
				startCycle.interactable = false;
				description.text = "Selecione um tipo de semente para ver seus detalhes";
			}

			actualMoney.text = PlayerPrefs.GetFloat("Money").ToString("C");
		}
	}

	void setUiText(int index)
	{
		TextMeshProUGUI name = seedGO[index].transform.Find("Name").GetComponent<TextMeshProUGUI>();
		name.text = seedTypes[index].seedName;

		TextMeshProUGUI cost = seedGO[index].transform.Find("Cost").GetComponent<TextMeshProUGUI>();
		cost.text = seedTypes[index].seedCost.ToString("C");		
	}

	public void setSeedOne()
	{
		typeSeed = seedTypes[0];
	}

	public void setFarmTwo()
	{
		typeSeed = seedTypes[1];
	}

	public void setFarmTree()
	{
		typeSeed = seedTypes[2];
	}

	public void setFarmFour()
	{
		typeSeed = seedTypes[3];
	}
}
