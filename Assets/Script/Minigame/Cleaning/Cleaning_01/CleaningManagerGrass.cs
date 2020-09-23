using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningManagerGrass : MonoBehaviour
{
	public GameObject modelGrass;
	public GameObject farm;
	public int spawnGrassQTD;
	public float grassPointsValue;

	private MinigameManager manager;
	
	void Start()
	{
		manager = this.gameObject.GetComponent<MinigameManager>();
		createGrassItens(spawnGrassQTD);
	}

	void createGrassItens(int qtd)
	{
		float oscilateX = 5f;
		float oscilateY = 2.5f;
		for(int c=0;qtd>c;c++)
		{
			Instantiate(modelGrass, new Vector3(Random.Range(farm.transform.position.x - oscilateX,farm.transform.position.x+oscilateX),Random.Range(farm.transform.position.y - oscilateY,farm.transform.position.y+oscilateY),0),Quaternion.identity);
		}
	}

	void Update ()
	{
		if(manager.testEndPuzzle())
			endPuzzle();
	}

	void endPuzzle()
	{
		manager.calculateReward(grassPointsValue,spawnGrassQTD - farm.GetComponent<ColliderBridgeFarm>().getColliderList().Count);
		GameObject.Find("Sickle").GetComponent<DragSickle>().DropSickle();
	}
}
