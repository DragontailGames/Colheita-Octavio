using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class storeManager : MonoBehaviour 
{
	private List<ItemToBuyStatus> buyedItens = new List<ItemToBuyStatus>();
	private GameObject[] storeItens;
	public List<ItemToBuyStatus> storeItensToBuy = new List<ItemToBuyStatus>();

	public GameObject context;
	public GameObject saleItem_prefab;

	public GameObject textDescription;
	public ItemToBuyStatus buyItem;
	public GameObject btBuy;

	void Start()
	{
		if(PlayerPrefs.HasKey("BuyedItens"))
		{
			string[] buyedItenTx = PlayerPrefs.GetString("BuyedItens").Split(',');
			List<int> buyedItensId = new List<int>();
			foreach(var i in buyedItenTx)
			{
			 	buyedItensId.Add(int.Parse(i));
			}
			foreach(var i in storeItensToBuy)
			{
				foreach(var c in buyedItensId)
				if(i.Id == c)
					i.buyed = true;
			}
		}
		refatoringStore();
	}

	void Update()
	{
		if(buyItem == null)
		{
			btBuy.SetActive(false);
			textDescription.SetActive(false);
		}
		else
		{
			btBuy.SetActive(true);
			textDescription.SetActive(true);
		}
		if(buyItem != null)
		{
			if(PlayerPrefs.GetFloat("Money") >= buyItem.price)
			{
				btBuy.GetComponent<Button>().interactable = true;
			}
			if(PlayerPrefs.GetFloat("Money") < buyItem.price)
			{
				btBuy.GetComponent<Button>().interactable = false;
			}
		}
	}

	List<ItemToBuyStatus> orderList(List<ItemToBuyStatus> aux)
	{
		List<ItemToBuyStatus> aux_1500 = new List<ItemToBuyStatus>();
		List<ItemToBuyStatus> aux_2500 = new List<ItemToBuyStatus>();
		List<ItemToBuyStatus> aux_3500 = new List<ItemToBuyStatus>();
		List<ItemToBuyStatus> aux_buyed = new List<ItemToBuyStatus>();

		foreach(var temp in aux)
		{
			if(temp.price == 1500 && !temp.buyed)
			{
				aux_1500.Add(temp);
			}
			else if(temp.price == 2500 && !temp.buyed)
			{
				aux_2500.Add(temp);
			}
			else if(temp.price == 3500 && !temp.buyed)
			{
				aux_3500.Add(temp);
			}
			else
			{
				aux_buyed.Add(temp);
			}
		}

    	aux_1500.Sort((a, b) => { return a.name.CompareTo(b.name);});
		aux_2500.Sort((a, b) => { return a.name.CompareTo(b.name);});
    	aux_3500.Sort((a, b) => { return a.name.CompareTo(b.name);});

		List<ItemToBuyStatus> aux_return = new List<ItemToBuyStatus>();

		foreach(var temp in aux_1500)
		{
			aux_return.Add(temp);
		}

		foreach(var temp in aux_2500)
		{
			aux_return.Add(temp);
		}

		foreach(var temp in aux_3500)
		{
			aux_return.Add(temp);
		}

		foreach(var temp in aux_buyed)
		{
			aux_return.Add(temp);
		}

		return aux_return;
	}

	private void pickBuyed()
	{
		string[] aux = PlayerPrefs.GetString("buyedItensId").Split(',');
		foreach(ItemToBuyStatus auxItens in storeItensToBuy)
		{
			foreach(string temp in aux)
			if(auxItens.Id.ToString() == temp)
			{
				auxItens.buyed = true;
				buyedItens.Add(auxItens);
			}
		}
	}

	private void refatoringStore()
	{
		storeItensToBuy = orderList(storeItensToBuy);

		foreach (Transform child in context.transform) 
		{
     		GameObject.Destroy(child.gameObject);
 		}

		foreach(var i in storeItensToBuy)
		{
			createSaleItem(i);
		}
	}

	private void createSaleItem(ItemToBuyStatus aux)
	{
		GameObject temp = Instantiate(saleItem_prefab,context.transform.position, Quaternion.identity);
		temp.GetComponent<ItemVisualSet>().itemSO = aux;
		temp.transform.SetParent(context.transform);
		temp.transform.localScale = new Vector3(1,1,1);
	}

	public void setEffectStoreItem()
	{
		if(buyItem.effectItem == ItemToBuyStatus.effectStore.minigameChange)
		{
			if(buyItem.stages==0)
				GameObject.Find("MinigameManager").GetComponent<MinigameSceneManager>().changeStandart(buyItem.valueToChange,buyItem.minigameToChange);
			else
			{
				changeMinigameCost();
			}

		}

		if(buyItem.effectItem == ItemToBuyStatus.effectStore.stageDuration)
		{
			foreach(var stagesAux in buyItem.stageCondition)
			{
				stagesAux.stageDuration *= (100-buyItem.valueToChange)/100;
			}
		}

		if(buyItem.effectItem == ItemToBuyStatus.effectStore.removeEvent)
		{
			foreach(var i in buyItem.eventCondition)
			{
				foreach(var temp in GameObject.Find("Seed_Selector").GetComponent<seedManager>().typeSeed.GroupList)
				{
					foreach(var aux in temp.events)
					{
						if(i == aux)
						{
							temp.events.Remove(aux);
						}
					}
				}
			}
		}

		foreach(var temp in buyItem.itemSO)
		{
			if(temp.effectItem == ItemToBuyStatus.effectStore.minigameChange)
			{
				if(temp.stages==0)
					GameObject.Find("MinigameManager").GetComponent<MinigameSceneManager>().changeStandart(temp.valueToChange,temp.minigameToChange);
				else
				{
					changeMinigameCost();
				}

			}

			if(temp.effectItem == ItemToBuyStatus.effectStore.stageDuration)
			{
				foreach(var stagesAux in temp.stageCondition)
				{
					stagesAux.stageDuration *= (100-temp.valueToChange)/100;
				}
			}

			if(temp.effectItem == ItemToBuyStatus.effectStore.removeEvent)
			{
				foreach(var i in temp.eventCondition)
				{
 					foreach(var auxI in GameObject.Find("Seed_Selector").GetComponent<seedManager>().typeSeed.GroupList)
					{
						foreach(var aux in auxI.events)
						{
							if(i == aux)
							{
								auxI.events.Remove(aux);
							}
						}
					}
				}
			}
		}
	}

	public void changeMinigameCost()
	{
		foreach(var aux in buyItem.stageCondition)
		{
			GameObject.Find("MinigameManager").GetComponent<MinigameSceneManager>().addDictionaryCost(buyItem.valueToChange,buyItem.minigameToChange,aux.stageName);
		}
	}

	public void setThisForSale(ItemToBuyStatus aux)
	{
		buyItem = aux;
		textDescription.GetComponentInChildren<TextMeshProUGUI>().text = aux.description;
	}

	public void buyButtonItem()
	{
		if(PlayerPrefs.GetFloat("Money") >= buyItem.price)
		{
			PlayerPrefs.SetFloat("Money",PlayerPrefs.GetFloat("Money")-buyItem.price);
			buyItem.buyed = true;

			string buyedItenTx = "";

			if(PlayerPrefs.HasKey("BuyedItens"))
			{
				buyedItenTx = PlayerPrefs.GetString("BuyedItens");
				buyedItenTx+=(","+buyItem.Id);
			}
			else
			{
				buyedItenTx=buyItem.Id.ToString();
			}

			PlayerPrefs.SetString("BuyedItens", buyedItenTx);

			setEffectStoreItem();
			refatoringStore();
		}
	}
}
