using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

[RequireComponent(typeof(NavMeshAgent))]
public class farmAssistent : MonoBehaviour 
{
	#region Movimento

	GameObject[] wayPointList;
	GameObject pointDestiny;

	menuManager MenuManager;
    AudioSource myAudio;
    public AudioClip myClip;
    resourcesManager _resourcesManager;
	stageManager _stageManager;

	void Start()
	{
        myAudio = GameObject.Find("FxAudioButton").GetComponent<AudioSource>();
        MenuManager = GameObject.Find("MenuManager").GetComponent<menuManager>();
		wayPointList = GameObject.FindGameObjectsWithTag("WayPoint");
		_resourcesManager = GameObject.Find("ResourcesManager").GetComponent<resourcesManager>();
		_stageManager = GameObject.Find("StageManager").GetComponent<stageManager>();
	
		if(PlayerPrefs.HasKey("TutorialEnd") && PlayerPrefs.GetInt("showOutNotice") == 1)
		{
			Invoke("showOutNotice",2f);
			StartCoroutine("setWayPoint");
			pointDestiny = wayPointList[0];
			PlayerPrefs.SetInt("showOutNotice",0);
		}
		else
		{
			PlayerPrefs.SetString("TutorialEnd", "...");
		}
	}

	IEnumerator setWayPoint()
	{
		yield return new WaitForSeconds(5f);

		int aux = Random.Range(0,wayPointList.Length);
		if(pointDestiny != wayPointList[aux])
			{pointDestiny = wayPointList[aux];}
		else if(aux > 0)
		{
			pointDestiny = wayPointList[aux - 1];
		}
		else
			{pointDestiny = wayPointList[aux + 1];}

		transform.GetComponentInChildren<Animator>().SetBool("CanMove", true);
		GetComponent<NavMeshAgent>().destination=pointDestiny.transform.position;
		
	}

	public GameObject outNotice;

	void showOutNotice()
	{
		float[] aux = _resourcesManager.outTimeResourcesChanged();
		string debugString;
		for(int c = 0;c<aux.Length;c++)
		{
			if(aux[c]>100)
			{
				aux[c] = 100;
			}
		}
		debugString = ("<b>Veja o que aconteceu enquanto estava fora: </b>" +
		"\nCafeina ganha: " + Mathf.Clamp(Mathf.Abs((int)aux[0]),0,100) +
		"\nAgua perdida: " + Mathf.Clamp(Mathf.Abs((int)aux[1]),0,100) + 
		"\nNutrientes perdidos: " + Mathf.Clamp(Mathf.Abs((int)aux[2]),0,100) + 
		"\nLimpeza perdida: " + Mathf.Clamp(Mathf.Abs((int)aux[3]),0,100) +
		"\n\n<b>Para se lembrar onde estavamos:</b>" + 
		"\n\nO seu estagio atual é: " + _stageManager.getActualStageStatus().stageName);

		if(PlayerPrefs.HasKey("EventName"))
		{
			debugString+="\nE você tem um evento ativo: " + PlayerPrefs.GetString("EventName");
		}
		else
		{
			debugString+="\nE você não tem eventos ativos";
		}

		outNotice.SetActive(true);
		outNotice.GetComponentInChildren<TextMeshProUGUI>().text = debugString;
		
		StartCoroutine("disableOutNotice");
		Time.timeScale = 0.000001f;

	}

	IEnumerator disableOutNotice()
	{
		yield return new WaitForSecondsRealtime(5f);
		outNotice.SetActive(false);
		Time.timeScale = 1;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "WayPoint" && other.gameObject == pointDestiny)
		{
			Invoke("disableAnimation" , 0.3f);

		}
	}

	void disableAnimation()
	{
			transform.GetComponentInChildren<Animator>().SetBool("CanMove", false);
			StartCoroutine("setWayPoint");
	}

	#endregion

	#region Menu

	public GameObject menu;

	void OnMouseDown()
	{
		if(MenuManager.canOpenMenu())
		{
            PlaySoundOnClick();
			GameObject.Find("Canvas").GetComponent<canvasInvisibleSet>().reorder(menu);
            menu.SetActive(true);
			MenuManager.MenuOpen();
		}
	}

	public void VideoCalled()
	{	
		Debug.Log("Video");
	}
	public void LicoesCalled()
	{	
		Debug.Log("Lições");
	}

	public void QuizCalled()
	{	
		Debug.Log("Quiz");
	}
    public void PlaySoundOnClick()
    {
        myAudio.PlayOneShot(myClip);
    }

    #endregion
}
