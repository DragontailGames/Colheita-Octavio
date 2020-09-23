using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class eventFriendManager : MonoBehaviour 
{
    public GameObject eventRecaller;

    private TextMeshProUGUI txtTimer;

    private TimeManager _timeManager;

    void Start()
    {
        txtTimer = eventRecaller.transform.Find("EventTxt").GetComponent<TextMeshProUGUI>();
        _timeManager = GameObject.Find("TimerManager").GetComponent<TimeManager>();
    }

    void Update()
    {
        if(PlayerPrefs.HasKey("EventDuration"))
        {
            DateTime aux = DateTime.Parse(PlayerPrefs.GetString("EventDuration"));
            if(_timeManager.currentTime() < aux)
            {
                eventRecaller.SetActive(true);
            }
            else
            {
                PlayerPrefs.DeleteKey("EventDuration");
            }
        }
        else
        {
            eventRecaller.SetActive(false);

        }

        if(PlayerPrefs.HasKey("EventDuration"))
        {
            txtTimer.text = "O evento atual ira acabar em: " + Mathf.Round((float)((DateTime.Parse(PlayerPrefs.GetString("EventDuration"))-_timeManager.currentTime()).TotalMinutes)) + "m";
        }
    }

}
