using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coffeeTree : MonoBehaviour 
{

	private float treeLife = 100;
	public Color32 greenColor;
	public Color32 yellowColor;
	private SpriteRenderer mySpecialColor;

	private int lifeCount;

	void Start () 
	{
		mySpecialColor = this.GetComponent<SpriteRenderer>();
	}
	
	void Update () 
	{
		if(Time.timeScale==1)
		{
			mySpecialColor.color = Color.Lerp(yellowColor,greenColor, treeLife/100);
			treeLife -= 0.2f;

			setLifeCount();

			if(treeLife>100)
				treeLife = 100;
			if(treeLife<0)
				treeLife = 0;
		}
	
	}

	void setLifeCount()
	{
		if(treeLife>80)
			lifeCount = 4;
		else if(treeLife>60)
			lifeCount = 3;
		else if(treeLife>40)
			lifeCount = 2;
		else if(treeLife>20)
			lifeCount = 1;
		else
			lifeCount = 0;
	}

	void addTreeLife()
	{
		treeLife += 20;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.transform.name.Contains("wagon"))
		{
			addTreeLife();
		}
	}

	public int getLifeCount()
	{
		return lifeCount;
	}
}
