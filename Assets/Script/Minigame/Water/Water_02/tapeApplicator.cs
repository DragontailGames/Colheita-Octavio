using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tapeApplicator : MonoBehaviour 
{
	public Vector2 baseApp;

	private void Awake() 
	{
		baseApp = this.transform.position;	
	}

	void Update()
	{
		if(GameObject.Find("GameManager").GetComponent<MinigameManager>().EndPuzzle())
            DropItem();

        if (dragging)
        {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Vector3 rayPoint = ray.GetPoint(distance);
            transform.position = new Vector3(rayPoint.x,rayPoint.y,0);
        }

		if(dragging == false)
		{
			backToBase();
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

	public void backToBase()
	{
		this.transform.position = baseApp;
	}

	public void DropItem()
    {
        dragging = false;
    }

    void OnMouseUp()
    {
        dragging = false;
    }
}
