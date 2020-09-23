using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class eventManager : MonoBehaviour 
{
    public GameObject eventRecaller;

    private Image ringAmount;

    private TimeManager _timeManager;

    void Start()
    {
        ringAmount = eventRecaller.transform.Find("RingTime").GetComponent<Image>();
        _timeManager = GameObject.Find("TimerManager").GetComponent<TimeManager>();
    }

    bool notification = false;

    void Update()
    {
        if(PlayerPrefs.HasKey("EventDuration"))
        {
            DateTime aux = DateTime.Parse(PlayerPrefs.GetString("EventDuration"));

            if(_timeManager.currentTime() < aux)
            {
                eventRecaller.GetComponent<Button>().interactable = true;
                if(!notification)
                {
                    eventRecaller.transform.Find("Notification").gameObject.SetActive(true);
                    notification = true;
                }
            }
            else
            {
                PlayerPrefs.DeleteKey("EventDuration");
                notification = false;
            }
        }
        else
        {
            eventRecaller.GetComponent<Button>().interactable = false;
        }

        if(PlayerPrefs.HasKey("EventDuration"))
        {
            ringAmount.fillAmount = Mathf.Round((float)((DateTime.Parse(PlayerPrefs.GetString("EventDuration"))-_timeManager.currentTime()).TotalMinutes))/100;
        }
    }

}
