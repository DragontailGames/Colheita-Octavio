using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class cicleManager : MonoBehaviour {

	public TextMeshProUGUI cicleList;
	public TextMeshProUGUI currentCicle;

	void Start()
	{
		Invoke("setCicleValue",2f);
	}

	void setCicleValue()
	{
		if(PlayerPrefs.HasKey("CiclePoint"))
		{
			string[] pointsAtCicle = PlayerPrefs.GetString("CiclePoint").Split(',');
			for (int c = 0; c < pointsAtCicle.Length; c++)
			{
				cicleList.text += "\nCiclo " + (c + 1) + ": " + pointsAtCicle[c] +"\n";
			}
		}
		else
		{
			cicleList.text = "Voce ainda nao finalizou nenhum Ciclo";
		}

		currentCicle.text = "\nCiclo Atual: " + PlayerPrefs.GetFloat("Points");
	}
}
