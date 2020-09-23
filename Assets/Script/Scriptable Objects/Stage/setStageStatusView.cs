using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class setStageStatusView : MonoBehaviour {

	private stageStatus myStageStatus;

	void Start () 
	{
		TextMeshProUGUI name = this.transform.Find("Name").GetComponent<TextMeshProUGUI>();
		name.text = myStageStatus.stageName;

		TextMeshProUGUI duration = this.transform.Find("Duration").GetComponent<TextMeshProUGUI>();
		Double durationAux = 60 * myStageStatus.stageDuration;
		duration.text = durationAux + "m";
	}

	void Update()
	{
		this.transform.Find("Closed").GetComponent<Image>().enabled = myStageStatus.iEnd;
		this.transform.Find("loadbarFront").GetComponent<Image>().fillAmount = myStageStatus.myFillAmount;
	}

	public void SetMyStage(stageStatus aux)
	{
		myStageStatus = aux;
	}
}
