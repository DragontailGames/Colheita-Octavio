using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu (menuName = "Stages/Stage Type")]
public class stageStatus : ScriptableObject
{
	public string stageName;
	[Tooltip("Time duration in Minutes.")]
	public double stageDuration;

	[Tooltip("Order number")]
	public int stageOrder;

	[TextArea(2,12)]
	public string stageDescription;

	public bool iEnd = false;

	//Pedro quando acabar o ciclo resetar esses dois em todos
	public float myFillAmount = 0;

	void Update()
	{
		if(iEnd)
		{
			myFillAmount = 1;
		}
		if(myFillAmount>=1.0f)
		{
			myFillAmount = 1;
			iEnd = true;
		}
	}
}
