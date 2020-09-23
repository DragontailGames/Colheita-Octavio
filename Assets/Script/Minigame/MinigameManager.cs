using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MinigameManager : MonoBehaviour 
{
	private int cdTimer = 3;
	public GameObject visualResult;
	public string PlayerPrefsVariableName;

	public TextMeshProUGUI txtCD;
	private bool canStartPuzzle = true;

	private TextMeshProUGUI txtTimer;
	public float timeLimit;
	private float timer;

	private resourcesManager managerR;

	bool onTutorial;

	void Start()
	{
		if(SceneManager.GetActiveScene().name == "MinigameTutorial")
			onTutorial = true;
			
		Time.timeScale = 0.0000001f;
			
		txtTimer = GameObject.Find("timer").GetComponent<TextMeshProUGUI>();
		managerR = GameObject.Find("ResourcesManager").GetComponent<resourcesManager>();

	}

	public void startCountdown()
	{
		StartCoroutine(CountDown());
	}

	public void StartPuzzle()
	{
		txtCD.text = "";
		timer = 0;
		Time.timeScale = 1;
		canStartPuzzle = false;
	}

	IEnumerator CountDown()
	{
		txtCD.text = cdTimer.ToString();
		yield return new WaitForSecondsRealtime(1f);
		cdTimer--;
		if(cdTimer>0)
			StartCoroutine(CountDown());
	}

	public void alternateTutorial(){onTutorial = !onTutorial;}

	void Update()
	{
		if(cdTimer<=0 && canStartPuzzle)
			StartPuzzle();

		txtTimer.text = Mathf.Round(timeLimit - timer).ToString();

		if(Time.timeScale == 1 && !onTutorial)
		{
			timer += Time.deltaTime;
		}

	}
	private bool canEndPuzzle = true;

	public bool testEndPuzzle()
	{
		if(timer>timeLimit && canEndPuzzle)
		{
			canEndPuzzle = false;
			return true;
		}
		else
			return false;
	}

	public bool EndPuzzle()
	{
		if(timer>timeLimit)
		{
			return true;
		}
		else
			return false;
	}
	
	public void calculateReward(float _pointsForValue, float _valueChange)
	{
		float result = _pointsForValue * _valueChange;
		Destroy(txtTimer.GetComponent<Transform>().gameObject);
		if(result == 0)
			result = 2;
		visualResult.SetActive(true);
		if(PlayerPrefsVariableName != "Money")
			visualResult.transform.Find("Result").GetComponent<TextMeshProUGUI>().text = result.ToString() +"%";
		
		else
			visualResult.transform.Find("Result").GetComponent<TextMeshProUGUI>().text = result.ToString("C") +"%";;

		setLocalVariable(PlayerPrefsVariableName,result);
		Time.timeScale=0;

		if(onTutorial || SceneManager.GetActiveScene().name == "MinigameTutorial")
		{		
			Time.timeScale=1;
			GameObject.Find("TutorialManager").GetComponent<tutorialManager>().setTutorial(17);
		}
	}


	public void setLocalVariable(string variableName, float _value)
	{
		if(variableName == "water")
		{
			managerR.WaterManager = _value;
		}
		if(variableName == "cleaning")
		{
			managerR.CleaningManager = _value;
		}
		if(variableName == "nutrients")
		{
			managerR.NutrientsManager = _value;
		}
		if(variableName == "Money")
		{
			if(!PlayerPrefs.HasKey("Money"))
			{
				PlayerPrefs.SetFloat("Money", 0);
			}
			else
				PlayerPrefs.SetFloat("Money", PlayerPrefs.GetFloat("Money") + _value);
		}
	}

	public void backToGame(string sceneName)
	{
		Time.timeScale = 1;
		SceneManager.LoadScene(sceneName);
	}
}
