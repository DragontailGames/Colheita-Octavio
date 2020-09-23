using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pipe : MonoBehaviour 
{
	int life = 5;
	[Tooltip("0=Normal,1=Furada,2=Arrumada")]
	public Sprite[] pipesSprite;

	int myType;

	void Start () 
	{
		this.GetComponent<SpriteRenderer>().sprite = pipesSprite[myType];
	}

	void Update()
	{
		if(life<=0)
		{
			myType = 2;
			this.GetComponent<SpriteRenderer>().sprite = pipesSprite[2];
		}
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.transform.name.Contains("Tape Applicator") && myType == 1)
		{
			life--;
		}
	}
	public int getMyType()
	{
		return myType;
	}

	public void setMyType(int value)
	{
		myType=value;
		this.GetComponent<SpriteRenderer>().sprite = pipesSprite[myType];
	}
}
