using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;

[System.Serializable]
public class ResetToBuild : EditorWindow
{
	[MenuItem("PCO/Reset para Build %#m")]
	public static void ShowWindown()
	{
		GetWindow<ResetToBuild>("Resetar para Build");
	}

	farmType[] farmTypes = new farmType[4];
	stageStatus[] stageTypes;
	ItemToBuyStatus[] toBuyTypes;

	void OnGUI()
	{
		farmTypes = (farmType[])Resources.FindObjectsOfTypeAll(typeof(farmType));
		stageTypes = (stageStatus[])Resources.FindObjectsOfTypeAll(typeof(stageStatus));
		toBuyTypes = (ItemToBuyStatus[])Resources.FindObjectsOfTypeAll(typeof(ItemToBuyStatus));

		if(GUILayout.Button("Reset"))
		{
			foreach(var temp in farmTypes)
			{
				if(temp.buyed==true)
				{	
					Debug.Log(temp.farmName + " a compra foi reiniciada");
					temp.buyed = false;
				}
			}

			foreach(var temp in stageTypes)
			{
				if(temp.iEnd == true)
				{	
					Debug.Log(temp.stageName + " o estagio foi reiniciada");
					temp.iEnd = false;
					temp.myFillAmount = 0;
				}
			}

			foreach(var temp in toBuyTypes)
			{
				if(temp.buyed==true)
				{	
					Debug.Log(temp.nameItem + " a compra foi reiniciada");
					temp.buyed = false;
				}
			}

			PlayerPrefs.DeleteAll();
			Debug.Log("TUDO FOI RESETADO");
		}

	}
}
