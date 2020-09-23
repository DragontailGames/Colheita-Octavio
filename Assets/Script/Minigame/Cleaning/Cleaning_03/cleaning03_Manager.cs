using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cleaning03_Manager : MonoBehaviour 
{
	public GameObject modelBug;
	public int spawnBugQTD;
	private float bugPointsValue;

	private MinigameManager manager;
	
	void Start()
	{
		manager = this.gameObject.GetComponent<MinigameManager>();

		bugPointsValue = 20/spawnBugQTD;

		StartCoroutine("createBug");
	}

	IEnumerator createBug()
	{
		for(int c=0;spawnBugQTD>c;c++)
		{
			Vector3 position = new Vector3();

			int inXorY = Random.Range(0,4);

			if(inXorY==0)
			{
				position.y = 6;
				position.x = Random.Range(-9,9);
			}
			if(inXorY==2)
			{
				position.y = -6;
				position.x = Random.Range(-9,9);
			}

			if(inXorY==1)
			{
				position.y = Random.Range(-5,5);;
				position.x = 10;
			}
			if(inXorY==3)
			{
				position.y = Random.Range(-5,5);;
				position.x = -10;
			}
			Instantiate(modelBug, position,Quaternion.identity);
			yield return new WaitForSeconds(0.2f);
		}
	}

	void Update ()
	{
		if(manager.testEndPuzzle())
			endPuzzle();
	}

	int bugsInBags = 0;

	public void addBugsInBags()
	{
		bugsInBags++;
	}

	void endPuzzle()
	{
		int qtdAux = spawnBugQTD-bugsInBags;
		manager.calculateReward(bugPointsValue,qtdAux);
	}
}
