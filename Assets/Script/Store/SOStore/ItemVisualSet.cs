using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemVisualSet : MonoBehaviour {

	public ItemToBuyStatus itemSO;
	private storeManager managerStore;

	void Start () 
	{
		gameObject.transform.Find("Icon").GetComponent<Image>().sprite = itemSO.itemIcon;
		gameObject.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = itemSO.nameItem;
		gameObject.transform.Find("Price").GetComponent<TextMeshProUGUI>().text = "$" + itemSO.price + ".00";
		
		managerStore = GameObject.Find("StoreManager").GetComponent<storeManager>();

		Invoke("TestBuyed",1.0f);
	}

	void TestBuyed()
	{
		if(itemSO.buyed)
		{
			this.gameObject.GetComponent<Button>().interactable = false;
			gameObject.transform.Find("Sold Out").gameObject.SetActive(true);
		}
	}
	
	public void BuyThisItem()
	{
		//managerStore.addNewItem(itemSO);
		this.gameObject.GetComponent<Button>().interactable = false;
		gameObject.transform.Find("Sold Out").gameObject.SetActive(true);

	} 

	public void setThisItem()
	{
		managerStore.setThisForSale(this.itemSO);
		TestBuyed();
	}
}
