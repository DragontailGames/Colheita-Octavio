using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaterMG_01 : MonoBehaviour 
{
	public GameObject pool;
	public Transform trash;
	public int startQTDTrash;

	public Sprite[] trashs;

	public int trashPointsValue;
	
	private MinigameManager manager;

	void Start () 
	{
		manager = this.gameObject.GetComponent<MinigameManager>();
		createTrashItens(startQTDTrash);
	}

	void StartPuzzle()
	{

	}

	void createTrashItens(int qtd)
	{
		float oscilateX = 2.5f;
		float oscilateY = 2.5f;
		for(int c=0;qtd>c;c++)
		{
			Transform aux = Instantiate(trash, new Vector3(Random.Range(pool.transform.position.x - oscilateX,pool.transform.position.x+oscilateX),Random.Range(pool.transform.position.x - oscilateY,pool.transform.position.x+oscilateY),0),Quaternion.identity);
			aux.gameObject.GetComponent<SpriteRenderer>().sprite = trashs[Random.Range(0,trashs.Length)];
		}
	}
	
	void Update ()
	{
		if(manager.testEndPuzzle())
			endPuzzle();
	}

	void endPuzzle()
	{
		manager.calculateReward(trashPointsValue,startQTDTrash - pool.GetComponent<ColliderBridge>().getColliderList().Count);
	}
}
