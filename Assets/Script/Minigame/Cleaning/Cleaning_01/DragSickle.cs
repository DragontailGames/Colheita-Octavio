using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragSickle : MonoBehaviour 
{
    private bool dragging = false;
    private float distance;
	private Vector2 startPosition;

	void Start()
	{
		startPosition = this.gameObject.transform.position;
	}

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

	public void DropSickle()
	{
		dragging = false;
		this.gameObject.transform.position = startPosition;
	}

    void OnMouseUp()
    {
        dragging = false;
		this.gameObject.transform.position = startPosition;
    }
    
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.name.Contains("Grass"))
		{
			other.gameObject.GetComponent<GrassManager>().dropLife();
		}
	} 

    void Update()
    {
        if (dragging)
        {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Vector3 rayPoint = ray.GetPoint(distance);
            transform.position = new Vector3(rayPoint.x,rayPoint.y,0);
        }
    }
}
