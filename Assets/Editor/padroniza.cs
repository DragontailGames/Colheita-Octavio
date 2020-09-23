using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class padroniza:MonoBehaviour
{
	public TMP_FontAsset fontTarget;

	public void SetFont()
	{
		GameObject[] aux = FindObjectsOfType<GameObject> ();

		foreach(GameObject temp in aux)
		{
			if(temp.GetComponent<TextMeshProUGUI>())
			{
				temp.GetComponent<TextMeshProUGUI>().font = fontTarget;
			}
		}
	}
}


[CustomEditor(typeof(padroniza))]
public class Padroniza : Editor
{

	public override void OnInspectorGUI()
	{
		padroniza myTarget = (padroniza)target;


		DrawDefaultInspector ();

		if (GUILayout.Button ("Padroniza Font"))
			myTarget.SetFont();
	}
}
