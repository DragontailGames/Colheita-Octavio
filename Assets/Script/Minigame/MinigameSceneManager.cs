using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameSceneManager : MonoBehaviour 
{
	resourcesManager managerR;

	public string saleSceneName;

	public GameObject noCaffeine;

	public List<MinigameType> minigameTypes = new List<MinigameType>();

	public stageManager _stageManager;

	void Start()
	{
		managerR = GameObject.Find("ResourcesManager").GetComponent<resourcesManager>();

		_stageManager = GameObject.Find("StageManager").GetComponent<stageManager>();


		foreach(var aux in waterMinigame)
		{
			minigameTypes.Add(aux);
		}
		foreach(var aux in cleaningMinigame)
		{
			minigameTypes.Add(aux);
		}
		foreach(var aux in nutrientsMinigame)
		{
			minigameTypes.Add(aux);
		}
	}

	public MinigameType[] waterMinigame;
	public void WaterPuzzle()
	{
		int sceneType = PlayerPrefs.GetInt("indexStageGroup");
		if(testCaffeine(waterMinigame[sceneType].caffeineCost))
		{
			managerR.CaffeineManager = -cleaningMinigame[sceneType].caffeineCost;
			SceneManager.LoadScene(waterMinigame[sceneType].sceneName);
		}
	}

	void Update()
	{
		if(!PlayerPrefs.HasKey("MinigameChangeCost"))
		{
			foreach(var aux in minigameTypes)
			{
				aux.changeWithFriend = 0;
			}
		}
	}

	public MinigameType[] nutrientsMinigame;

	public void NutrientsPuzzle()
	{
		int sceneType = PlayerPrefs.GetInt("indexStageGroup");
		if(testCaffeine(nutrientsMinigame[sceneType].caffeineCost))
		{
			managerR.CaffeineManager = -cleaningMinigame[sceneType].caffeineCost;
			SceneManager.LoadScene(nutrientsMinigame[sceneType].sceneName);
		}
	}
	public MinigameType[] cleaningMinigame;

	public void CleaningPuzzle()
	{
		int sceneType = PlayerPrefs.GetInt("indexStageGroup");
		if(testCaffeine(cleaningMinigame[sceneType].caffeineCost))
		{
			managerR.CaffeineManager = -cleaningMinigame[sceneType].caffeineCost;
			SceneManager.LoadScene(cleaningMinigame[sceneType].sceneName);
		}
	}

	public string tutorialSceneMinigame;

	public void TutorialPuzzle()
	{
		managerR.CaffeineManager = -20;
		SceneManager.LoadScene(tutorialSceneMinigame);
	}

	public void changeStandart(float valueChange_inPorc, eventStatus.minigame minigameToChange)
	{
		if(minigameToChange == eventStatus.minigame.all)
		{
			foreach(var aux in minigameTypes)
			{
				aux.caffeineStandartCost *= (100-valueChange_inPorc)/100;
			}
		}

		resetAndResetCaffeineCost(_stageManager.getActualStageStatus().stageName);

	}

	public void friendHelp(float valueChange_inPorc, eventStatus.minigame minigameToChange)
	{
		if(minigameToChange == eventStatus.minigame.all)
		{
			foreach(var aux in minigameTypes)
			{
				aux.changeWithFriend = valueChange_inPorc;
			}
		}

		resetAndResetCaffeineCost(_stageManager.getActualStageStatus().stageName);
	}

	public void testDictionaryCost(string stageName)
	{
		resetAndResetCaffeineCost(stageName);
	}

	public void addDictionaryCost(float valueChange_inPorc, eventStatus.minigame minigameToChange, string stage)
	{
		if(minigameToChange == eventStatus.minigame.all)
		{
			foreach(var aux in minigameTypes)
			{
				if(aux.changeCostStage.ContainsKey(stage))
				{
					aux.changeCostStage[stage] += valueChange_inPorc;
				}
				else
				{
					aux.changeCostStage.Add(stage,valueChange_inPorc);
				}
			}
		}

		if(minigameToChange == eventStatus.minigame.cleaning)
		{
			foreach(var aux in cleaningMinigame)
			{
				if(aux.changeCostStage.ContainsKey(stage))
				{
					aux.changeCostStage[stage] += valueChange_inPorc;
				}
				else
				{
					aux.changeCostStage.Add(stage,valueChange_inPorc);
				}
			}
		}

		if(minigameToChange == eventStatus.minigame.nutrients)
		{
			foreach(var aux in nutrientsMinigame)
			{
				if(aux.changeCostStage.ContainsKey(stage))
				{
					aux.changeCostStage[stage] += valueChange_inPorc;
				}
				else
				{
					aux.changeCostStage.Add(stage,valueChange_inPorc);
				}
			}
		}

		if(minigameToChange == eventStatus.minigame.water)
		{
			foreach(var aux in waterMinigame)
			{
				if(aux.changeCostStage.ContainsKey(stage))
				{
					aux.changeCostStage[stage] += valueChange_inPorc;
				}
				else
				{
					aux.changeCostStage.Add(stage,valueChange_inPorc);
				}
			}
		}

		resetAndResetCaffeineCost(_stageManager.getActualStageStatus().stageName);

	}

	public void resetAndResetCaffeineCost(string stageName)
	{
		foreach(var temp in minigameTypes)
		{
			temp.caffeineCost = temp.caffeineStandartCost;

			foreach(var aux in minigameTypes)
			{
				if(aux.changeCostStage.ContainsKey(stageName))
				{
					aux.caffeineCost *= (100-aux.changeCostStage[stageName])/100;
				}
			}

			if(PlayerPrefs.HasKey("MinigameChangeCost"))
			{
				temp.caffeineCost *= (100-temp.changeWithFriend)/100;
			}
		}
	}
	public void SalePuzzle()
	{
		SceneManager.LoadScene(saleSceneName);
	}	

	private bool testCaffeine(float testValue)
	{
		bool returnBool;
		
		if(PlayerPrefs.HasKey("caffeine"))
		{
			if(testValue<=managerR.CaffeineManager)
			{
				returnBool = true;
			}
			else
			{
				returnBool = false;
			}
		}
		else
			returnBool = false;

		
		if(returnBool == false)
		{
			noCaffeine.SetActive(true);
			GameObject.Find("Canvas").GetComponent<canvasInvisibleSet>().reorder(noCaffeine);
		}

		return returnBool;
	}
}
