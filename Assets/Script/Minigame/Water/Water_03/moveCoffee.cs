using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCoffee : MonoBehaviour 
{
	public Sprite[] coffeGradual;

	private Vector3 currentPosition;

	private bool dragging = false;
    private float distance;
    private float myLife = 20;

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

    void Update()
    {
        if (dragging)
        {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Vector3 rayPoint = ray.GetPoint(distance);
            transform.position = new Vector3(rayPoint.x,rayPoint.y,0);
            StartCoroutine("testPosition");
        }
        else
        {
            StopCoroutine("testPosition");
        }
        if(myLife<=0)
        {
            GameObject.Find("GameManager").GetComponent<coffeeWaterManager>().coffeDrained();
            Destroy(this.gameObject);
        }
    }

    IEnumerator testPosition()
    {
        if(this.transform.position!=currentPosition)
        {
            float variable = Mathf.Abs(Vector2.Distance(currentPosition,this.transform.position));
            myLife -= variable;
            currentPosition = this.transform.position;
        }
        yield return new WaitForSeconds(.1f);
    }
}

