using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coffeeBag : MonoBehaviour 
{

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.transform.name.Contains("Bug"))
		{
			GameObject.Find("GameManager").GetComponent<cleaning03_Manager>().addBugsInBags();
			Destroy(other.gameObject);
		}
	}

}
