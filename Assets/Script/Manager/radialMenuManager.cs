using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class radialMenuManager : MonoBehaviour 
{

	public bool isOpen =false;
	private bool inTransition = false;

	public GameObject[] child;

	menuManager MenuManager;

	void Start()
	{
		if(GameObject.Find("MenuManager"))
			MenuManager = GameObject.Find("MenuManager").GetComponent<menuManager>();
	}

	void Update()
	{
		transform.Find("Notification").GetComponent<Transform>().gameObject.SetActive(
		transform.Find("Event").GetComponent<Transform>().Find("Notification").gameObject.activeSelf);

		transform.Find("RingTime").GetComponent<Image>().fillAmount = 
		transform.Find("Event").GetComponent<Transform>().Find("RingTime").GetComponent<Image>().fillAmount;

		transform.Find("Event").GetComponent<Transform>().Find("Notification").GetComponent<Image>().color = transform.Find("Event").GetComponent<Image>().color; 
		transform.Find("Event").GetComponent<Transform>().Find("RingTime").GetComponent<Image>().color = transform.Find("Event").GetComponent<Image>().color; 
	}

	public void menuChangeState () 
	{
		isOpen = !isOpen;
		inTransition = false;
	}

	float ToMove = 100;


	public void openMenu()
	{
		if(MenuManager.canOpenMenu())
		{
			if(!inTransition)
			{
				if(!isOpen)
				{
					GetComponent<Animator>().Play("OpenRadialMenu");
					setButtonInteractable(true);
					Invoke("closeMenu",8f);
				}
				else if(isOpen)
				{
					GetComponent<Animator>().Play("CloseRadialMenu");
					setButtonInteractable(false);
				}
				inTransition = true;
			}
		}
	}
	public void openMenuTutotial()
	{
		if(!inTransition)
		{
			if(!isOpen)
			{
				GetComponent<Animator>().Play("OpenRadialMenu");
				setButtonInteractable(true);
				Invoke("closeMenu",8f);
			}
			else if(isOpen)
			{
				GetComponent<Animator>().Play("CloseRadialMenu");
				setButtonInteractable(false);
			}
			inTransition = true;
		}
	}

	public void closeMenu()
	{
		if(!inTransition && isOpen)
		{
			GetComponent<Animator>().Play("CloseRadialMenu");
			setButtonInteractable(false);
			inTransition = true;
		}
	}

	public void setButtonInteractable(bool inter)
	{
		for(int c = 0;c<child.Length;c++)
		{
			child[c].GetComponent<Button>().interactable = inter;
		}
	}
}
