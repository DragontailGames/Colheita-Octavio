using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderBridge : MonoBehaviour 
{
	private List<GameObject> colliderList = new List<GameObject>();
	void OnTriggerStay2D(Collider2D collision)
	{
		if(!colliderList.Contains(collision.transform.gameObject))
		{
			colliderList.Add(collision.transform.gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D collision)
	{
		colliderList.Remove(collision.transform.gameObject);
		Destroy(collision.transform.gameObject,0.2f);
	}

	public List<GameObject> getColliderList()
	{
		return colliderList;
	}
}
