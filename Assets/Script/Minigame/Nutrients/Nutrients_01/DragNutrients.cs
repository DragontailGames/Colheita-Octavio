using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragNutrients : MonoBehaviour 
{
    private bool dragging = false;
    private float distance;

	public GameObject DropZone;

	private Collider2D colliderCol;

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

    void OnMouseUp()
    {
        dragging = false;
    }
    
	void OnTriggerStay2D(Collider2D other)
	{
		colliderCol = other;
	} 

    void Update()
    {
		if(colliderCol != null)
		{
			if(colliderCol.gameObject.name == DropZone.name && !dragging)
			{
				GameObject.Find("GameManager").GetComponent<NutrientsManager>().addNutrients();
				Destroy(this.gameObject,0.01f);
			}
		}

		if(Time.timeScale != 1)
		{
			dragging = false;
		}

        if (dragging)
        {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Vector3 rayPoint = ray.GetPoint(distance);
            transform.position = new Vector3(rayPoint.x,rayPoint.y,0);
        }
    }
}
