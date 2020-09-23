using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class stageViewer : MonoBehaviour {

	stageManager manager;

	public TextMeshProUGUI descriptionCurrentState;

	public GameObject content;
	public GameObject stageModel;

	List<stageStatus> stageList = new List<stageStatus>();

	void Start () 
	{
		manager = GameObject.Find("StageManager").GetComponent<stageManager>();
		stageList = manager.GetStages();

		for(int c = 0;c<stageList.Count;c++)
		{
			GameObject temp = Instantiate(stageModel,content.transform.position, Quaternion.identity);
			temp.GetComponent<setStageStatusView>().SetMyStage(stageList[c]);
			temp.transform.SetParent(content.transform);
			temp.transform.localScale = Vector3.one;
		}
	}
	
	void Update()
	{
		if(PlayerPrefs.HasKey("stageEnd"))
			setViewer();
	}
	
	void setViewer()
	{
		descriptionCurrentState.text = "\"" + manager.getActualStageStatus().stageName +"\"\n\n"+manager.getActualStageStatus().stageDescription;
	}
}
