using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class castFriendEvent : MonoBehaviour {

	private stageManager _stageManager;
	public GameObject eventNote;

	private bool eventIsCalled = false;

	TextMeshProUGUI nameTMP;
	TextMeshProUGUI effect;

	void Start () 
	{
		_stageManager = GameObject.Find("StageManager").GetComponent<stageManager>();

		nameTMP = eventNote.transform.Find("Name").GetComponent<TextMeshProUGUI>();
		effect = eventNote.transform.Find("Efeito").GetComponent<TextMeshProUGUI>();

		InvokeRepeating("testEvent",3f,3f);
	}

	void testEvent()
	{
	}

	float modifierValue = 0;
	int ticks = 0;

	eventStatus eventCalled;

	void callEvent()
	{
		eventNote.SetActive(true);
		eventStatus[] eventList = new eventStatus[5];

		int randomValue = UnityEngine.Random.Range(0,eventList.Length);
		eventCalled = eventList[randomValue];


		nameTMP.text = eventCalled.eventName;
		//description.text = eventList[randomValue].description;
		PlayerPrefs.SetString("EventNameFriend", eventCalled.eventName);
		
		modifierValue = eventCalled.modifier;

		if(eventCalled.effectEvent != eventStatus.effect.Modifier)
		{
			int ticks = eventCalled.timeDuration/15;
			statusEffectModifier(effect, ticks, modifierValue, eventCalled.description, eventCalled.dropStatusModifier.ToString().ToLower());
		}
		
		if(eventCalled.effectEvent != eventStatus.effect.dropDirect)
		{
			statusEffectDrop(effect,modifierValue, eventCalled.description, eventCalled.dropDirectStatus.ToString().ToLower());
		}

		if(eventCalled.effectEvent != eventStatus.effect.minigameCost)
		{
			minigameCostChange(eventCalled.minigameCost,eventCalled.timeDuration,eventCalled.changeMinigame);
		}
		if(eventCalled.effectEvent != eventStatus.effect.changePoints)
		{
			changePoints((int)eventCalled.modifier,eventCalled.timeDuration);
		}
		if(eventCalled.effectEvent != eventStatus.effect.stageModifier)
		{
			reduceDurationStage(eventCalled.timeReduceToEndNextSage);
		}

		if(eventCalled.moneyChange != 0)
		{
			PlayerPrefs.SetFloat("money", PlayerPrefs.GetFloat("money") + eventCalled.moneyChange);
		}
	}

	void statusEffectModifier(TextMeshProUGUI effect,int ticks,float modifierValue, string effectTxt, string playerPrefsName)
	{
		effect.text = effectTxt;
		PlayerPrefs.SetString("StatusFriendModifier", playerPrefsName+ "/"+ticks+"/"+modifierValue);
		PlayerPrefs.SetString("EventFriendDuration",(_stageManager.getCurrentTime().AddMinutes(ticks * 15)).ToString());
	}

	void statusEffectDrop(TextMeshProUGUI effect, float modifierValue, string effectTxt, string playerPrefsName)
	{
		effect.text = effectTxt;
		GameObject.Find("ResourcesManager").GetComponent<resourcesManager>().dropStatus(playerPrefsName, modifierValue);
	}

	void minigameCostChange(float newCost_inPorc, float duration,eventStatus.minigame minigameType)
	{
		//quando essa string nao existir mais o minigame volta para o custo standart
		GameObject.Find("MinigameManager").GetComponent<MinigameSceneManager>().friendHelp(newCost_inPorc,minigameType);
		PlayerPrefs.SetString("MinigameChangeCost", GameObject.Find("TimerManager").GetComponent<TimeManager>().currentTime().AddMinutes(duration).ToString());
	}

	void changePoints(int modifier, float duration)
	{
		PlayerPrefs.SetInt("PointModifier", modifier);
		PlayerPrefs.SetFloat("PointModifierDuration", duration/15);
	}

	void reduceDurationStage(float reduceTime)
	{
		PlayerPrefs.SetFloat("stageDurationReduce",reduceTime);
	}
}


