using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class statusUI : MonoBehaviour 
{
	public string playerPrefsName;

	public bool canShowStatusText = true;

	private GameObject childText;

	void Start()
	{
		childText = this.gameObject.transform.Find("StatusText").gameObject;
	}
	
	void Update () 
	{
		float aux = PlayerPrefs.GetFloat(playerPrefsName);
		gameObject.transform.Find("Front").GetComponent<Image>().fillAmount = aux/100;

		childText.GetComponent<TextMeshProUGUI>().text = (int)aux + "%";
	}

	public void MoveStatus()
	{
		if(canShowStatusText)
		{
			GetComponent<Animator>().Play("Up");
			canShowStatusText = false;
		}
	}

	public void resetMoveStatus()
	{
		canShowStatusText = true;
	}
}
