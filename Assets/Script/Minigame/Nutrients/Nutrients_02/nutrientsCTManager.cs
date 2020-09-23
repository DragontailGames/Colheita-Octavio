using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nutrientsCTManager : MonoBehaviour 
{
	public int spawnQtd;
	private MinigameManager manager;
	public GameObject modelCoffeTree;

	public GameObject[] coffesTrees;
	
	void Start()
	{
		manager = this.gameObject.GetComponent<MinigameManager>();
		createCoffeTreeItens(spawnQtd);

		coffesTrees = GameObject.FindGameObjectsWithTag("coffeeTree");
	}

	void createCoffeTreeItens(int qtd)
	{
		for(int c=0;qtd>c;c++)
		{
			if(c<5)
			{
				float value = 2.8f * (c+1) + (-7) - 0.8f;
				Debug.Log("V1 " + c + " - " + value);
				Instantiate(modelCoffeTree, new Vector3(value + (Random.Range(0, -1f)),Random.Range(-0.8f,-3.0f),0),Quaternion.identity);	
			}
			else
			{
				float value = 2.8f * ((c+1)-5) + (-7) - 0.8f;
				Debug.Log("V2 " + c + " - " + value);
				Instantiate(modelCoffeTree, new Vector3(value + (Random.Range(0, -1f)),Random.Range(0.8f,3.0f),0),Quaternion.identity);	
			}	
		}
	}

	void Update ()
	{
		if(manager.testEndPuzzle())
			endPuzzle();
	}

	void endPuzzle()
	{
		GameObject.Find("wagon").GetComponent<dragWagon>().DropWagon();
		int countOfLife = 0;
		foreach (var item in coffesTrees)
		{
			countOfLife += item.GetComponent<coffeeTree>().getLifeCount();
			Debug.Log(item.transform.name + " - " + item.GetComponent<coffeeTree>().getLifeCount());
		}
		Debug.Log(countOfLife);
		manager.calculateReward(countOfLife,0.5f);
	}
}
