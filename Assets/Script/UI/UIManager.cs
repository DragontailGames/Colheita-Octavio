using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour {

	private stageManager stageManagerSC;

	public Button[] notInteractButton;

	menuManager MenuManager;

	public TextMeshProUGUI moneyTxt;

	void Start()
	{
		MenuManager = GameObject.Find("MenuManager").GetComponent<menuManager>();
	}

	public GameObject notification;

	void Update()
	{
		if(!MenuManager.canOpenMenu())
		{
			foreach(var aux in notInteractButton)
			{
				aux.interactable = false;
			}
		}

		if(MenuManager.canOpenMenu())
		{
			foreach(var aux in notInteractButton)
			{
				aux.interactable = true;
			}
		}

		moneyTxt.text = PlayerPrefs.GetFloat("Money").ToString("C");
		if(PlayerPrefs.GetFloat("Money") < 0)
		{
			PlayerPrefs.SetFloat("Money",0);
		}
	}

}
