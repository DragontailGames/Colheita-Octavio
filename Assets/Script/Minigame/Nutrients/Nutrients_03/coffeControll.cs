using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class coffeControll : MonoBehaviour 
{

	void Start () 
	{
        /*
		this.transform.position = new Vector3(this.transform.position.x+(UnityEngine.Random.Range(-5f,5f)),this.transform.position.y+(UnityEngine.Random.Range(-3f,
		3f)),0);
        */
	}

	void Update () 
	{
		if(GameObject.Find("GameManager").GetComponent<MinigameManager>().EndPuzzle())
            DropItem();

        if (dragging)
        {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Vector3 rayPoint = ray.GetPoint(distance);
            transform.position = new Vector3(rayPoint.x,rayPoint.y,0);
        }
	}

	private bool dragging = false;
    private float distance;

    void OnMouseDown()
    {
		if(Time.timeScale==1)
		{
			distance = Vector3.Distance(transform.position, Camera.main.transform.position);
			if (dragging == false)
			{
				dragging = true;
			}
		}
		else
			dragging = false;

    }

	public void DropItem()
    {
        dragging = false;
    }

    void OnMouseUp()
    {
        dragging = false;
    }


	void OnTriggerExit2D(Collider2D other)
	{
		if(other.transform.name.Contains("Table"))
		{
			Destroy(this.gameObject,0.2f);
		}
	}
}
