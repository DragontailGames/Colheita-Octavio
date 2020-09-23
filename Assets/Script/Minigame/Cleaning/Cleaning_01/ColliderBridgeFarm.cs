using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderBridgeFarm : MonoBehaviour 
{
	public List<GameObject> colliderList = new List<GameObject>();
	void OnTriggerStay2D(Collider2D collision)
	{
		if(!colliderList.Contains(collision.transform.gameObject) && !collision.name.Contains("Sickle"))
		{
			colliderList.Add(collision.transform.gameObject);
		}
	}

	public void removeList(GameObject _remove)
	{
		colliderList.Remove(_remove);
	}

	public List<GameObject> getColliderList()
	{
		return colliderList;
	}
}
