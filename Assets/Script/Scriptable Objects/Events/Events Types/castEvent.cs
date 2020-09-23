using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class castEvent : MonoBehaviour {

	private stageManager _stageManager;
	public GameObject eventNote;

	public GameObject quizNote;

	private bool eventIsCalled = false;

	private TextMeshProUGUI effect;
	
	public GameObject notificationReference;

	void Start () 
	{
		_stageManager = GameObject.Find("StageManager").GetComponent<stageManager>();

		InvokeRepeating("testEvent",3f,3f);
	}
	
	void Update () 
	{
	}

	void testEvent()
	{
		if(PlayerPrefs.HasKey("eventCallTime"))
		{
			if(_stageManager.getCurrentTime() >= DateTime.Parse(PlayerPrefs.GetString("eventCallTime")) && !eventIsCalled)
			{
				callQuiz();
			}
		}
	}

	public void setTimeEvent()
	{
		double range = (DateTime.Parse(PlayerPrefs.GetString("stageEnd")) - _stageManager.getCurrentTime()).TotalMinutes;

		if(range > 60)
		{
			eventIsCalled = false;
			PlayerPrefs.SetString("eventCallTime",(_stageManager.getCurrentTime().AddMinutes(UnityEngine.Random.Range(20,(Convert.ToSingle(range)-30))).ToString()));
			notificationReference.GetComponent<NotificationTest>().eventNotification();
		}
	}

	float modifierValue = 0;
	int ticks = 0;

	public eventStatus eventCalled;

	public void callQuiz()
	{
		quizNote.SetActive(true);

		eventIsCalled = true;
		
		eventStatus[] eventList = _stageManager.getEventList();

		int randomValue = UnityEngine.Random.Range(0,eventList.Length);
		eventCalled = eventList[randomValue];

		quizNote.GetComponent<quizManagerViewer>().setEvent(eventCalled);
		quizNote.GetComponent<quizManagerViewer>().setQuest();
	}

	public void resetEvent()
	{
		PlayerPrefs.DeleteKey("eventCallTime");
		setTimeEvent();
	}

	public void callEvent()
	{		
		eventNote.SetActive(true);
		GameObject.Find("MenuManager").GetComponent<menuManager>().MenuOpen();

		//description.text = eventList[randomValue].description;
		PlayerPrefs.SetString("EventName", eventCalled.eventName);
		
		modifierValue = eventCalled.modifier;

		if(eventCalled.effectEvent == eventStatus.effect.Modifier)
		{
			int ticks = eventCalled.timeDuration/15;
			statusEffectModifier(effect, ticks, modifierValue, eventCalled.description, eventCalled.dropStatusModifier.ToString().ToLower());
		}
		
		if(eventCalled.effectEvent == eventStatus.effect.dropDirect)
		{
			statusEffectDrop(effect,modifierValue, eventCalled.description, eventCalled.dropDirectStatus.ToString().ToLower());
		}
		
		if(eventCalled.effectEvent == eventStatus.effect.Conditional)
		{
			conditionalEffect();
		}
		if(eventCalled.effectEvent == eventStatus.effect.minigameCost)
		{
			minigameCostChange(eventCalled.minigameCost,eventCalled.description,eventCalled.timeDuration,eventCalled.changeMinigame);		
		}

		if(eventCalled.moneyChange != 0)
		{
			PlayerPrefs.SetFloat("Money", PlayerPrefs.GetFloat("Money") + eventCalled.moneyChange);
		}

		PlayerPrefs.DeleteKey("eventCallTime");
		eventNote.GetComponent<eventNoteView>().setMyEvent(eventCalled);
		setTimeEvent();
	}

	public void callEventTutorial()
	{		
		modifierValue = 20;
		
		int ticks = 60/15;
		statusEffectModifier(effect, ticks, modifierValue, eventCalled.description, eventCalled.dropStatusModifier.ToString().ToLower());
	}
	
	void conditionalEffect()
	{
		if(eventCalled.hasCondition.conditionThis == eventConditional.contitionTypes.all)
		{
			//ticks = eventList[randomValue].timeDuration/15;
			if(PlayerPrefs.GetFloat("water") > 50f && PlayerPrefs.GetFloat("cleaning") > 50f && PlayerPrefs.GetFloat("nutrients") > 50f)
			{
				statusEffectDrop(effect, modifierValue, eventCalled.hasCondition.goodCondition.description, "caffeine");
			}
			else
			{
				eventStatus badCondition = eventCalled.hasCondition.badCondition;

				if(badCondition.dropStatusModifier != eventStatus.dropStatus.nothing && badCondition.dropDirectStatus == eventStatus.dropStatus.nothing)
				{
					int ticks = badCondition.timeDuration/15;
					statusEffectModifier(effect,ticks,modifierValue,badCondition.description,badCondition.dropStatusModifier.ToString().ToLower());
				}
				if(badCondition.dropStatusModifier == eventStatus.dropStatus.nothing && badCondition.dropDirectStatus != eventStatus.dropStatus.nothing)
				{
					statusEffectDrop(effect,modifierValue,badCondition.description,badCondition.dropStatusModifier.ToString().ToLower());
				}
				
			}
		}
		if(eventCalled.hasCondition.conditionThis != eventConditional.contitionTypes.all)
		{
			if(PlayerPrefs.GetFloat(eventCalled.hasCondition.conditionThis.ToString().ToLower()) > 50f)
			{
				statusEffectDrop(effect, modifierValue, eventCalled.hasCondition.goodCondition.description, eventCalled.hasCondition.goodCondition.dropDirectStatus.ToString().ToLower());
			}
			else
			{
				eventStatus badCondition = eventCalled.hasCondition.badCondition;

				if(badCondition.dropStatusModifier != eventStatus.dropStatus.nothing && badCondition.dropDirectStatus == eventStatus.dropStatus.nothing)
				{
					int ticks = badCondition.timeDuration/15;
					statusEffectModifier(effect,ticks,modifierValue,badCondition.description,badCondition.dropStatusModifier.ToString().ToLower());
				}
				if(badCondition.dropStatusModifier == eventStatus.dropStatus.nothing && badCondition.dropDirectStatus != eventStatus.dropStatus.nothing)
				{
					statusEffectDrop(effect,modifierValue,badCondition.description,badCondition.dropStatusModifier.ToString().ToLower());
				}
				
			}
		}
	}

	void statusEffectModifier(TextMeshProUGUI effect,int ticks,float modifierValue, string effectTxt, string playerPrefsName)
	{
		PlayerPrefs.SetString("eventEffect", effectTxt);
		PlayerPrefs.SetString("StatusModifier", playerPrefsName+ "/"+ticks+"/"+modifierValue);
		PlayerPrefs.SetString("EventDuration",(_stageManager.getCurrentTime().AddMinutes(ticks * 15)).ToString());
	}

	void statusEffectDrop(TextMeshProUGUI effect, float modifierValue, string effectTxt, string playerPrefsName)
	{
		PlayerPrefs.SetString("eventEffect",effectTxt);
		GameObject.Find("ResourcesManager").GetComponent<resourcesManager>().dropStatus(playerPrefsName, modifierValue);
	}

	void minigameCostChange(float newCost_inPorc, string effectTxt,float duration,eventStatus.minigame minigameType)
	{
		PlayerPrefs.SetString("eventEffect", effectTxt);

		//quando essa string nao existir mais o minigame volta para o custo standart
		GameObject.Find("MinigameManager").GetComponent<MinigameSceneManager>().friendHelp(newCost_inPorc,minigameType);
		PlayerPrefs.SetString("MinigameChangeCost", GameObject.Find("TimerManager").GetComponent<TimeManager>().currentTime().AddMinutes(duration).ToString());
	}
}


