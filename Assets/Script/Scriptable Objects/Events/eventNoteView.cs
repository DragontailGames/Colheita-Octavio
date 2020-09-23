using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class eventNoteView : MonoBehaviour {


	eventStatus myActualEvent;

	eventStatus[] allEvents;

	TextMeshProUGUI nameTMP;
	TextMeshProUGUI effect;

	void Start()
	{
		if(PlayerPrefs.HasKey("EventName"))
		{
			InvokeRepeating("testActualEvent",0.01f,2f);
		}
		
		nameTMP = this.transform.Find("Name").GetComponent<TextMeshProUGUI>();
		effect = this.transform.Find("Efeito").GetComponent<TextMeshProUGUI>();
	}

	void testActualEvent()
	{
		allEvents = GameObject.Find("StageManager").GetComponent<stageManager>().getEventList();

		if(PlayerPrefs.HasKey("EventName"))
		{
			foreach(var temp in allEvents)
			{
				if(temp.name == PlayerPrefs.GetString("EventName"))
				{
					myActualEvent = temp;
				}
			}
		}
	}

	void Update () 
	{
		if(myActualEvent != null)
		{
			nameTMP.text = myActualEvent.eventName;
			effect.text = PlayerPrefs.GetString("eventEffect");
		}
		
	}

	public void deleteActualEvent()
	{
		myActualEvent = null;
	}

	public void setMyEvent(eventStatus eventAux)
	{
		myActualEvent = eventAux;
	}

	public void clearPlayerPrefs()
	{
		if(!PlayerPrefs.HasKey("EventDuration"))
		{
			PlayerPrefs.DeleteKey("EventName");
			PlayerPrefs.DeleteKey("eventEffect");
		}
	}
}
