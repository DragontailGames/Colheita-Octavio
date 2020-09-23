using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class quizManagerViewer : MonoBehaviour 
{
	public quizType _quiz;
	public eventStatus _event;

	private GameObject _result;

	public GameObject EventNote;

	public bool onTutorial = false;

	void Start()
	{	
		if(onTutorial)
		{
			setQuest();
		}	
	}

	public void setQuest()
	{
		if(_quiz != null && this.gameObject.activeSelf)
		{
			_result = this.transform.Find("Result").GetComponent<Transform>().gameObject;
			_result.SetActive(false);
			this.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = _quiz._quizName;
			this.transform.Find("Quest").GetComponent<TextMeshProUGUI>().text = _quiz._quizQuest;
			GameObject bt1 = this.transform.Find("AS_1").GetComponent<Transform>().gameObject;
			GameObject bt2 = this.transform.Find("AS_2").GetComponent<Transform>().gameObject;
			bt1.GetComponent<Button>().onClick.RemoveAllListeners();
			bt2.GetComponent<Button>().onClick.RemoveAllListeners();

			if(Random.Range(0,2) == 1)
			{
				bt1.transform.Find("AS_1_Text").GetComponent<TextMeshProUGUI>().text = _quiz._wrongAswer;
				bt2.transform.Find("AS_2_Text").GetComponent<TextMeshProUGUI>().text = _quiz._rigthAswer;
				bt1.GetComponent<Button>().onClick.AddListener(wrongAswerSet);
				bt2.GetComponent<Button>().onClick.AddListener(rigthAswerSet);
			}
			else
			{
				bt1.transform.Find("AS_1_Text").GetComponent<TextMeshProUGUI>().text = _quiz._rigthAswer;
				bt2.transform.Find("AS_2_Text").GetComponent<TextMeshProUGUI>().text = _quiz._wrongAswer;
				bt2.GetComponent<Button>().onClick.AddListener(wrongAswerSet);
				bt1.GetComponent<Button>().onClick.AddListener(rigthAswerSet);			
			}		
		}
	}

	void rigthAswerSet()
	{
		_result.SetActive(true);
		_result.transform.Find("Rigth").GetComponent<Transform>().gameObject.SetActive(true);
		_result.transform.Find("Wrong").GetComponent<Transform>().gameObject.SetActive(false);
		_result.transform.Find("Rigth").GetComponent<TextMeshProUGUI>().text = _quiz._rigthAswerResult;
		GameObject.Find("eventManager").GetComponent<castEvent>().resetEvent();
	}

	public void wrongAswerSet()
	{
		this.gameObject.SetActive(false);
		_result.transform.Find("Wrong").GetComponent<Transform>().gameObject.SetActive(true);
		_result.transform.Find("Rigth").GetComponent<Transform>().gameObject.SetActive(false);
		_result.transform.Find("Wrong").GetComponent<TextMeshProUGUI>().text = _quiz._wrongAswerResult;
		EventNote.SetActive(true);
		GameObject.Find("eventManager").GetComponent<castEvent>().callEvent();
	}

	public void setEvent(eventStatus _eventAux)
	{
		_quiz = _eventAux._quiz;
		_event = _eventAux;
	}
}
