using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningManagerBug : MonoBehaviour
{
	public GameObject modelBug;
	public int spawnBugQTD;
	public float bugPointsValue;

	private MinigameManager manager;
	
	void Start()
	{
		manager = this.gameObject.GetComponent<MinigameManager>();
		Invoke("createBugItens",0.1f);
	}

	void createBugItens()
	{
		for(int c=0;spawnBugQTD>c;c++)
		{
			Instantiate(modelBug, new Vector3(0,0,0),Quaternion.identity);
		}
	}

	void Update ()
	{
		if(manager.testEndPuzzle())
			endPuzzle();
	}

	void endPuzzle()
	{
		int qtdAux = spawnBugQTD;
		foreach (var gameObj in FindObjectsOfType(typeof(GameObject)) as GameObject[])
		{
			if(gameObj.name.Contains("Bug"))
			{
				gameObj.GetComponent<bugMove>().stopWalk();
				qtdAux--;
			}
		}
		Debug.Log(qtdAux);
		manager.calculateReward(bugPointsValue,qtdAux);
	}
}
