using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class tutorialWrite : MonoBehaviour 
{
	private IEnumerator coroutine;
	
	bool onWrite = false;

	void Update()
    {
        if(Input.GetMouseButton(0) && onWrite)
		{
			StopCoroutine(coroutine);
			myTextTMPRO.text = "\t" + myTextString;
			StartCoroutine(setEndWrite());
			onWrite = false;
		}
	}

	IEnumerator setEndWrite()
	{
		yield return new WaitForSecondsRealtime(0.1f);
		isEndWrite = true;
	}
	
	IEnumerator setOnWrite()
	{
		yield return new WaitForSecondsRealtime(0.1f);
		onWrite = true;
	}

	public void writeText(string text, TextMeshProUGUI uiTxt)
    {
		StartCoroutine(setOnWrite());
		isEndWrite = false;
        coroutine = writeTextEnum(text,uiTxt);
		StartCoroutine(coroutine);
    }
	
	bool isEndWrite = true;

	public bool endWrite()
	{
		return isEndWrite;
	}

	int i;

	TextMeshProUGUI myTextTMPRO;
	string myTextString;

	IEnumerator writeTextEnum(string text, TextMeshProUGUI textPro)
	{
		myTextTMPRO = textPro;
		myTextString = text;

		textPro.text = "\t";
		char[] textArray = text.ToCharArray();
		for(i = 0;i<textArray.Length;i++)
		{
			textPro.text+=textArray[i];
			yield return new WaitForSecondsRealtime(0.03f);
		}
		StopCoroutine(coroutine);
		textPro.text = "\t" + text;
		isEndWrite = true;
		onWrite = false;
	}
}
