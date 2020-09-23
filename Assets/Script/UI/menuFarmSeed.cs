using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class menuFarmSeed : MonoBehaviour 
{

	public TextMeshProUGUI moneyText;

	private bool overMenu;

	public GameObject buyItem;

	void Start()
	{
		if(!PlayerPrefs.HasKey("Money"))
			PlayerPrefs.SetFloat("Money",550);//Pedro dinheiro inicial

		PlayerPrefs.SetInt("showOutNotice",1);
	}
	
	void Update () 
	{
		moneyText.text = PlayerPrefs.GetFloat("Money").ToString("C");

		overMenu = buyItem.activeSelf;
	}
}
