using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coffeForSale: MonoBehaviour 
{

	private bool dragging = false;
    private float distance;
    private bool dropOnTruck = false;

    void Start()
    {

    }

    void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().sortingOrder = 2;
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
        if(Time.timeScale==1)
        {
            GameObject.Find("GameManager").GetComponent<SaleManager>().createCoffeSack();
            saleThis();
        }
        Destroy(this.gameObject,0.1f);
    }

    void OnMouseUp()
    {
        DropItem();
    }

    void Update()
    {

        if(GameObject.Find("GameManager").GetComponent<MinigameManager>().EndPuzzle())
        {
            DropItem();
            
        }

        if (dragging)
        {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Vector3 rayPoint = ray.GetPoint(distance);
            transform.position = new Vector3(rayPoint.x,rayPoint.y,0);
        }
    }

	void saleThis()
	{
		GameObject.Find("GameManager").GetComponent<SaleManager>().addCoffePoints();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Truck")
        {
            dropOnTruck = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.name == "Truck")
        {
            dropOnTruck = false;
        }
    }
}
