using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NutrientsManager : MonoBehaviour
{
	public GameObject[] modelNutrients;
	public GameObject camp;
	public float nutrientsPointsValue;

	private MinigameManager manager;
	
	void Start()
	{
		manager = this.gameObject.GetComponent<MinigameManager>();
		modelNutrients = Shuffle(modelNutrients);
		createNutrientsItens();
	}
	System.Random _random = new System.Random();

	public T[] Shuffle<T>(T[] array)
	{
		var random = _random;
		for (int i = array.Length; i > 1; i--)
		{
			// Pick random element to swap.
			int j = random.Next(i); // 0 <= j <= i-1
			// Swap.
			T tmp = array[j];
			array[j] = array[i - 1];
			array[i - 1] = tmp;
		}
		return array;
	}


	void createNutrientsItens()
	{
		float positionX = -6;
		float positionYBegin = 3;


		for(int c=0;4>c;c++)
		{
			Instantiate(modelNutrients[c], new Vector2(positionX, positionYBegin - (2*c)),Quaternion.identity);
		}
	}

	int rigthsNutrients = 0;

	public void addNutrients()
	{
		if(rigthsNutrients<4)
			rigthsNutrients++;
	}

	void Update ()
	{
		if(manager.testEndPuzzle())
			endPuzzle();
	}

	void endPuzzle()
	{
		manager.calculateReward(nutrientsPointsValue,rigthsNutrients);
	}
}
