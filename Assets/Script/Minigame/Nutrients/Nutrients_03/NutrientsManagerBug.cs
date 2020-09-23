using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NutrientsManagerBug : MonoBehaviour
{
	public GameObject[] modelCoffes;
	public int spawnCoffeQTD;
	public float coffePointsValue;
    public GameObject circleToUse;
	private MinigameManager manager;
	
	void Start()
	{
		manager = this.gameObject.GetComponent<MinigameManager>();
		Invoke("createCoffeItens",0.1f);
	}

	void createCoffeItens()
	{
		for(int c=0;spawnCoffeQTD>c;c++)
		{
            Vector2 prePos = new Vector2 (circleToUse.transform.position.x, circleToUse.transform.position.y);
            Vector2 poS = (prePos + (Random.insideUnitCircle * circleToUse.GetComponent<CircleCollider2D>().radius * 3f));
            Instantiate(modelCoffes[1], poS,Quaternion.identity);
            poS = (prePos + (Random.insideUnitCircle * circleToUse.GetComponent<CircleCollider2D>().radius * 3f));
            Instantiate(modelCoffes[0], poS, Quaternion.identity);

        }
	}

	void Update ()
	{
		if(manager.testEndPuzzle())
			endPuzzle();
	}

	void endPuzzle()
	{
		int qtdAuxGood = 0;
		int qtdAuxBad = spawnCoffeQTD;
		int qtdValue = 0;

		foreach (var gameObj in FindObjectsOfType(typeof(GameObject)) as GameObject[])
		{
			if(gameObj.name.Contains("Coffe"))
			{
				if(gameObj.name.Contains("CoffeBad"))
				{
					qtdAuxBad--;
				}
				if(gameObj.name.Contains("CoffeGood"))
				{
					qtdAuxGood++;
				}
			}
		}

		qtdValue = qtdAuxBad - (spawnCoffeQTD - qtdAuxGood);
		if(qtdValue <=0)
			qtdValue = 0;
		manager.calculateReward(coffePointsValue,qtdValue);
	}
}
