using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class stageManager : MonoBehaviour
{
	private TimeManager timeManager;
	private stageStatus[] stageList;

	public seedType seed;

	private int indexStage;
	private int indexGroupStage;

	private Button bt_NextStage;
	public GameObject eventEndNotification;

	private stageStatus actualStageStatus;

	private castEvent managerEvent;

	public GameObject stageUI;

	private GameObject[] coffeTrees;

	void Awake()
	{
		bt_NextStage = stageUI.transform.Find("NextStage").GetComponent<Button>();
		timeManager = GameObject.Find("TimerManager").GetComponent<TimeManager>();
		seed = GameObject.Find("Seed_Selector").GetComponent<seedManager>().typeSeed;
		managerEvent = GameObject.Find("eventManager").GetComponent<castEvent>();
	}

	void Start()
	{
		coffeTrees = GameObject.FindGameObjectsWithTag("coffeTree");
		stageUI.SetActive(false);

		InvokeRepeating("testStage",0.5f,0.5f);
		InvokeRepeating("setVisualCurrentStage",2f,60f);

		if(!PlayerPrefs.HasKey("indexStageGroup"))
		{
			PlayerPrefs.SetInt("indexStageGroup",0);
			indexGroupStage = 0;
		}
		else
		{
			indexGroupStage = PlayerPrefs.GetInt("indexStageGroup");
		}

		if(!PlayerPrefs.HasKey("indexStage"))
		{
			PlayerPrefs.SetInt("indexStage",0);
			indexStage = 0;
			StartCoroutine("StartNewCycle");
		}
		else
		{
			indexStage = PlayerPrefs.GetInt("indexStage");
		}

		setStageGroup(indexGroupStage);

		actualStageStatus = stageList[indexStage];
		stageSetCorrect();

		if(PlayerPrefs.HasKey("AnimationCount"))
		{
			foreach(var aux in coffeTrees)
			{
				aux.GetComponent<CoffeAnimationManager>().setAnimator(PlayerPrefs.GetInt("AnimationCount"));
			}
		}
		else
		{
			PlayerPrefs.SetInt("AnimationCount",-1);
		}
	}

	void stageSetCorrect()
	{
		for(int i = 0;i<seed.GroupList.Length;i++)
		{
			if(indexGroupStage>i)
			{
				foreach(var aux in seed.GroupList[i].stages)
				{
					aux.iEnd = true;
					aux.myFillAmount = 1;
				}
				if(i>0)
				{
					for(int c = i;c>=0;c--)
					{
						foreach(var aux in seed.GroupList[c].stages)
						{
							aux.iEnd = true;
							aux.myFillAmount = 1;
						}
					}
				}
			}
			if(indexGroupStage==i)
			{
				//pedro erro de estagio
				for(int c =0;c<seed.GroupList[i].stages.Length;c++)
				{
					if(c<indexStage)
					{
						seed.GroupList[i].stages[c].myFillAmount = 1;
						seed.GroupList[i].stages[c].iEnd = true;
					}
					if(c>indexStage)
					{
						seed.GroupList[i].stages[c].myFillAmount = 0;
						seed.GroupList[i].stages[c].iEnd = false;
					}
					if(c==indexStage)
					{
						seed.GroupList[i].stages[c].myFillAmount = 0;
						seed.GroupList[i].stages[c].iEnd = false;
					}
				}
			}
			if(indexGroupStage<i)
			{
				foreach(var aux in seed.GroupList[i].stages)
				{
					aux.iEnd = false;
					aux.myFillAmount = 0;
				}
			}
		}
	}

	public eventStatus[] getEventList()
	{
		return seed.GroupList[indexGroupStage].events.ToArray();
	}

	void testStage()
	{
		if(PlayerPrefs.HasKey("stageEnd"))
		{
			int validNewStage = DateTime.Compare(timeManager.currentTime(), DateTime.Parse(PlayerPrefs.GetString("stageEnd")));
			if(eventEndNotification != null)
			{
				if(validNewStage == -1)
				{
					bt_NextStage.interactable = false;
					eventEndNotification.SetActive(false);
				}
				else
				{
					eventEndNotification.SetActive(true);
					bt_NextStage.interactable = true;
				}
			}
		}
	}

	public DateTime getCurrentTime()
	{
		return timeManager.currentTime();
	}

	DateTime minuteFill;
	DateTime startStage;

	public void setVisualCurrentStage()
	{
		if(PlayerPrefs.HasKey("stageEnd"))
		{
			minuteFill = DateTime.Parse(PlayerPrefs.GetString("stageEnd"));
			startStage = DateTime.Parse(PlayerPrefs.GetString("stageStart"));
		}		

		double auxMinutes = (minuteFill-startStage).TotalMinutes;
		double currentMinutes = (getCurrentTime() - startStage).TotalMinutes;

		getActualStageStatus().myFillAmount = Convert.ToSingle(((currentMinutes * 100) / auxMinutes)/100);
		
		if(getActualStageStatus().myFillAmount>=1)
		{
			getActualStageStatus().iEnd = true;
			getActualStageStatus().myFillAmount = 1;
		}
	}

	IEnumerator StartNewCycle()
	{
		yield return new WaitForSeconds(1.0f);
		setStage(stageList[indexStage]);
	}

	void savePlayerPrefs()
	{
		PlayerPrefs.SetInt("indexStage",indexStage);
		PlayerPrefs.SetInt("indexStageGroup",indexGroupStage);
	}

	public GameObject cicleEndNotice;

	public void nextStage()
	{
		bt_NextStage.interactable = false;
		if((indexStage + 1) >= stageList.Length)
		{
			Debug.Log((indexGroupStage + 1) >= seed.GroupList.Length-1);
			if((indexGroupStage + 1) >= seed.GroupList.Length-1)
			{
				cicleEndNotice.SetActive(true);
				GameObject.Find("Canvas").GetComponent<canvasInvisibleSet>().reorder(cicleEndNotice);
				GameObject.Find("MenuManager").GetComponent<menuManager>().MenuOpen();
				return;
			}
			else
			{
				indexStage = -1;
				indexGroupStage++;
				setStageGroup(indexGroupStage);
			}
		}
		indexStage += 1;
		setStage(stageList[indexStage]);
		savePlayerPrefs();
		GameObject.Find("MinigameManager").GetComponent<MinigameSceneManager>().testDictionaryCost(getActualStageStatus().stageName);
		stageSetCorrect();
		setVisualCurrentStage();
	}

	public List<stageStatus> GetStages()
	{
		List<stageStatus> auxReturn = new List<stageStatus>();
	
		for(int c = 0;c<seed.GroupList.Length;c++)
		{
			for(int i = 0; i<seed.GroupList[c].stages.Length; i++)
				auxReturn.Add(seed.GroupList[c].stages[i]);

		}

		return auxReturn;
	}

	public stageStatus getActualStageStatus()
	{
		return actualStageStatus;
	}

	
	void setStageGroup(int aux)
	{
		stageList = seed.GroupList[aux].stages;
	}

	public GameObject notificationReference;

	public void setStage(stageStatus _stage)
	{
		DateTime endTime;
		actualStageStatus = _stage;
		if(PlayerPrefs.HasKey("stageDurationReduce"))
		{
			float reduce = PlayerPrefs.GetFloat("stageDurationReduce");
			endTime = timeManager.currentTime().AddHours(_stage.stageDuration * (100-reduce)/100);
			PlayerPrefs.DeleteKey("stageDurationReduce");
		}
		else
		{
			endTime = timeManager.currentTime().AddHours(_stage.stageDuration);
		}

		PlayerPrefs.SetInt("AnimationCount",(PlayerPrefs.GetInt("AnimationCount") +1));

		foreach(var aux in coffeTrees)
		{
			aux.GetComponent<CoffeAnimationManager>().setAnimator(PlayerPrefs.GetInt("AnimationCount"));
		}

		PlayerPrefs.SetString("stageEnd",endTime.ToString());
		PlayerPrefs.SetString("stageStart",timeManager.currentTime().ToString());
		managerEvent.setTimeEvent();
		notificationReference.GetComponent<NotificationTest>().stageEnd();
	} 
}
