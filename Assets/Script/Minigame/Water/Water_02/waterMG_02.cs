using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterMG_02 : MonoBehaviour 
{
	public float holePointsValue;
	public int pipeHole = 10;

	public Transform basePipe;

	private MinigameManager manager;

	private GameObject[] pipeList;

	void Start()
	{
		pipeList = GameObject.FindGameObjectsWithTag("pipe");
		manager = this.gameObject.GetComponent<MinigameManager>();
		Invoke("createHoles",0.1f);
	}

	void createHoles()
	{
		List<int> spawnedInts = new List<int>();
		for(int c=0;pipeHole>c;c++)
		{
			bool cantExitWhile = true;
			int randValue = 0;
			while(cantExitWhile)
			{
				randValue = Random.Range(0,pipeList.Length);
				if(!spawnedInts.Contains(randValue))
				{
					cantExitWhile = false;
				}
			}
			spawnedInts.Add(randValue);
			pipeList[randValue].transform.GetComponent<pipe>().setMyType(1);
		}
	}

	void Update ()
	{
		if(manager.testEndPuzzle())
			endPuzzle();
	}

	void endPuzzle()
	{
		int pointCounter = 10;
		foreach (var item in pipeList)
		{
			if(item.transform.GetComponent<pipe>().getMyType() == 1)
			{
				pointCounter--;
			}
		}
		
		manager.calculateReward(holePointsValue,pointCounter);
	}
}
