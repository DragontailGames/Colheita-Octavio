using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class currentStage : MonoBehaviour {

	stageManager manager;

	void Start () 
	{
		manager = GameObject.Find("StageManager").GetComponent<stageManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		TextMeshProUGUI auxText = this.gameObject.transform.Find("Name").GetComponent<TextMeshProUGUI>();
		auxText.text = "Estagio Atual: " + manager.getActualStageStatus().stageName;

		Image loadbar = this.gameObject.transform.Find("loadbarFront").GetComponent<Image>();

		if(PlayerPrefs.HasKey("stageEnd"))
		{
			DateTime minuteFill = DateTime.Parse(PlayerPrefs.GetString("stageEnd"));
			DateTime startEvent = DateTime.Parse(PlayerPrefs.GetString("stageStart"));

			double auxMinutes = (minuteFill-startEvent).TotalMinutes;
			double currentMinutes = (manager.getCurrentTime()-startEvent).TotalMinutes;

			loadbar.fillAmount = Convert.ToSingle((currentMinutes * 100 / auxMinutes)/100);
		}
	}
}
