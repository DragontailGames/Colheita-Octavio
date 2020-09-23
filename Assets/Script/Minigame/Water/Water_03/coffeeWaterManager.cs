using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coffeeWaterManager : MonoBehaviour 
{

	public int coffeesToDrain;
	public int coffeesDrained;

	public GameObject coffeeModel;

	private MinigameManager manager;
	
	void Start()
	{
		manager = this.gameObject.GetComponent<MinigameManager>();

		createCoffeeItens(coffeesToDrain);
	}

	void createCoffeeItens(int qtd)
	{
		for(int c=0;qtd>c;c++)
		{
			if(c<4)
			{
				float value = 3.25f * (c+1) + (-7);
				Instantiate(coffeeModel, new Vector3(value + (Random.Range(0.5f, -0.5f)),Random.Range(-0.8f,-3.0f),0),Quaternion.identity);	
			}
			else
			{
				float value = 3.25f * ((c+1)-5) + (-7);
				Instantiate(coffeeModel, new Vector3(value + (Random.Range(0, -1f)),Random.Range(0.8f,3.0f),0),Quaternion.identity);	
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
		manager.calculateReward(coffeesDrained,2.5f);
	}

	public void coffeDrained()
	{
		coffeesDrained++;
	}
}
