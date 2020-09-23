using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class canvasInvisibleSet : MonoBehaviour 
{
	public GameObject panel;

	void Start()
	{
		panel.GetComponent<Image>().CrossFadeAlpha(0f,0.1f,false);

	}


	void Update () 
	{
	}

	public List<GameObject> childToInv = new List<GameObject>();

	public void reorder(GameObject topOfList)
	{
		List<GameObject> childs = new List<GameObject>();
		for(int i = 0; i<transform.childCount;i++)
		{
			childs.Add(transform.GetChild(i).gameObject);
		}

		int positionToObj = 0;

		for(int i = 0;i<childs.Count;i++)
		{
			if(childs[i].name == topOfList.name)
			{
				positionToObj = i;
				break;
			}
		}
		GameObject temp = childs[transform.childCount-1];
		childs[transform.childCount-1] = childs[positionToObj];
		childs[positionToObj] = temp;

		for(int i = 0; i<transform.childCount;i++)
		{
			childs[i].transform.SetSiblingIndex(i);
		}

		Invoke("openMenu",0.2f);
	}
	
	void openMenu () 
	{
		panel.SetActive(true);
		panel.GetComponent<Image>().CrossFadeAlpha(1f,0.5f,false);

		childToInv = new List<GameObject>();
		for(int i = 0; i<transform.childCount;i++)
		{
			if(transform.GetChild(i).name != panel.name)
			{
				childToInv.Add(transform.GetChild(i).gameObject);
			}
			if(transform.GetChild(i).name == panel.name)
			{
				break;
			}
		}

		foreach (var aux in childToInv)
		{
			Image[] temp = aux.GetComponentsInChildren<Image>();
			TextMeshProUGUI[] tempText = aux.GetComponentsInChildren<TextMeshProUGUI>();
			foreach(var imgTemp in temp)
			{
				imgTemp.CrossFadeAlpha(1f,0.5f,false);
			}

			foreach(var txtTemp in tempText)
			{
				txtTemp.CrossFadeAlpha(1f,0.5f,false);
			}
		}

		

	}

	public void closeMenu()
	{
		panel.GetComponent<Image>().CrossFadeAlpha(0f,0.5f,false);

		childToInv = new List<GameObject>();
		for(int i = 0; i<transform.childCount;i++)
		{
			if(transform.GetChild(i).name != panel.name)
			{
				childToInv.Add(transform.GetChild(i).gameObject);
			}
			if(transform.GetChild(i).name == panel.name)
			{
				break;
			}
		}

		foreach (var aux in childToInv)
		{
			Image[] temp = aux.GetComponentsInChildren<Image>();
			TextMeshProUGUI[] tempText = aux.GetComponentsInChildren<TextMeshProUGUI>();
			foreach(var imgTemp in temp)
			{
				imgTemp.CrossFadeAlpha(1f,0.5f,false);
			}

			foreach(var txtTemp in tempText)
			{
				txtTemp.CrossFadeAlpha(1f,0.5f,false);
			}
		}


		Invoke("panelOff",0.5f);

	}

	void panelOff()
	{
		panel.SetActive(false);
	}
}
