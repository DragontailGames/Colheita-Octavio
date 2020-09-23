using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class bugMove : MonoBehaviour 
{
	float speed;

	void Start () 
	{
		transform.rotation = Quaternion.Euler(new Vector3(0,0,UnityEngine.Random.Range(0,361)));
		this.transform.position = new Vector3(this.transform.position.x+(UnityEngine.Random.Range(-2f,2f)),this.transform.position.y+(UnityEngine.Random.Range(-4f,4f)),0);
		speed = UnityEngine.Random.Range(2f,3f);
		
	}
	bool canRotate = true;
	bool canMove = false;

	bool setTurnOrientation;
	int turnOrientation;

	void Update () 
	{
		if(canRotate && !dragging)
		{
			if(setTurnOrientation)
			{
				setTurnOrientation = false;
				System.Random random = new System.Random();
				int[] arrayAux = {1,-1};
				int randomAux = random.Next(0, arrayAux.Length);
				turnOrientation = arrayAux[randomAux];
			}
			this.transform.Rotate(new Vector3(0,0,this.transform.rotation.z + ((speed/2) * turnOrientation)));
			if(UnityEngine.Random.Range(0,100)==7)
			{
				canRotate = false;
				setTurnOrientation = true;
				canMove = true;
			}
		}

		if(canMove && !dragging)
		{
			float speedMove;
			if(canRotate)
			{
				speedMove = speed/6;
			}

			else
			{
				speedMove = speed/3;
			}

			transform.position += transform.up * speedMove * Time.deltaTime;

			if(UnityEngine.Random.Range(0,40)==7)
			{
				canRotate = true;
			}
		}

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
		if(other.transform.name.Contains("Farm_Camp"))
		{
			Destroy(this.gameObject,0.2f);
		}
	}

	public void stopWalk()
	{
		speed = 0;
		canMove = false;
		canRotate = false;
	}
}
