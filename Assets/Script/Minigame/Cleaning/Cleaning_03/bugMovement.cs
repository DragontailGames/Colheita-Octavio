using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bugMovement : MonoBehaviour 
{
	GameObject destination;

	float mySpeed;

	void Start () 
	{
		mySpeed = Random.Range(0.6f,1.0f);
		destination = GameObject.Find("CoffeeBag");

		Vector3 targ = destination.transform.position;
		targ.z = 0f;

		Vector3 objectPos = transform.position;
		targ.x = targ.x - objectPos.x;
		targ.y = targ.y - objectPos.y;
		float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
	}
	
	void Update () 
	{
		transform.position += transform.right * Time.deltaTime * mySpeed;
	}

	void OnMouseDown()
	{
		DestroyImmediate(this.gameObject);
	}
}
