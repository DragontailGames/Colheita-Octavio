using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class searchManager : MonoBehaviour 
{
	List<searchStatus> allSearchTypes = new List<searchStatus>();

	List<searchStatus> selectedSearchTypes = new List<searchStatus>();

	Dictionary<string,int> encounters = new Dictionary<string, int>();
	List<searchStatus> orderedSearch = new List<searchStatus>();

	public GameObject searchGO;

	public GameObject content;
	public GameObject textGO;

	void Start()
	{
		var searchTypesAux = Resources.LoadAll("SearchTypes",typeof(searchStatus));
		foreach(var aux in searchTypesAux)
		{
			allSearchTypes.Add((searchStatus)aux);
		}		
	}


	public void searchKeyWords(string keyWords)
	{
		clearAll();

		var keys = keyWords.Split(' ');
		foreach(var aux in keys)
		{
			search(aux);
		}
		
		
		foreach(var temp in selectedSearchTypes)
		{
			if(encounters.ContainsKey(temp.nameThing))
			{
				encounters[temp.nameThing]++;
			}
			else
			{
				encounters.Add(temp.nameThing,1);
			}
		}

		encounters = orderDict(encounters);

		
		foreach(var temp in encounters)
		{
			foreach(var aux in allSearchTypes)
			{
				if(temp.Key == aux.nameThing)
				{
					orderedSearch.Add(aux);
				}
			}
		}

		List<searchStatus> videoStatus = new List<searchStatus>();
		List<searchStatus> textStatus = new List<searchStatus>();


		foreach(var temp in orderedSearch)
		{
			if(temp.categorie == searchStatus.categorieType.video)
			{
				videoStatus.Add(temp);
			}
			if(temp.categorie == searchStatus.categorieType.texto)
			{
				textStatus.Add(temp);
			}
		}

		GameObject auxGoVideo = Instantiate(textGO,content.transform.position,Quaternion.identity);
		auxGoVideo.GetComponentInChildren<TextMeshProUGUI>().text = " - Videos - ";
		auxGoVideo.transform.SetParent(content.transform);
		auxGoVideo.transform.localScale = new Vector3(1,1,1);

		foreach(var temp in videoStatus)
		{
			GameObject createGO = Instantiate(searchGO,content.transform.position,Quaternion.identity);
			createGO.GetComponent<visualSetSearchThing>().setSearchThing(temp);
			createGO.transform.SetParent(content.transform);
			createGO.transform.localScale = new Vector3(1,1,1);
		}

		GameObject auxGoText = Instantiate(textGO,content.transform.position,Quaternion.identity);
		auxGoText.GetComponentInChildren<TextMeshProUGUI>().text = " - Textos - ";
		auxGoText.transform.SetParent(content.transform);
		auxGoText.transform.localScale = new Vector3(1,1,1);

		foreach(var temp in textStatus)
		{
			GameObject createGO = Instantiate(searchGO,content.transform.position,Quaternion.identity);
			createGO.GetComponent<visualSetSearchThing>().setSearchThing(temp);
			createGO.transform.SetParent(content.transform);
			createGO.transform.localScale = new Vector3(1,1,1);
		}

	}

	void search(string keyW)
	{
		foreach(searchStatus aux in allSearchTypes)
		{
			foreach(string temp in aux.keySearch)
			{
				if(temp.ToLower().Trim() == keyW.ToLower().Trim())
				{
					selectedSearchTypes.Add(aux);
				}
			}
		}
	}

	Dictionary<string,int> orderDict(Dictionary<string,int> dicAux)
	{
		Dictionary<string,int> returnDict = new Dictionary<string, int>();
		
		var items = from pair in dicAux
                    orderby pair.Value descending
                    select pair;

        foreach (KeyValuePair<string, int> pair in items)
        {
           returnDict.Add(pair.Key, pair.Value);
        }

		return returnDict;
	}

	public void loadAllVideoThing()
	{
		clearAll();

		GameObject auxGoVideo = Instantiate(textGO,content.transform.position,Quaternion.identity);
		auxGoVideo.GetComponentInChildren<TextMeshProUGUI>().text = " - Videos - ";
		auxGoVideo.transform.SetParent(content.transform);
		auxGoVideo.transform.localScale = new Vector3(1,1,1);

		foreach(var temp in allSearchTypes)
		{
			if(temp.categorie == searchStatus.categorieType.video)
			{
				GameObject createGO = Instantiate(searchGO,content.transform.position,Quaternion.identity);
				createGO.GetComponent<visualSetSearchThing>().setSearchThing(temp);
				createGO.transform.SetParent(content.transform);
				createGO.transform.localScale = new Vector3(1,1,1);
			}
		}
	}

	public void loadAllTextThing()
	{
		clearAll();

		GameObject auxGoVideo = Instantiate(textGO,content.transform.position,Quaternion.identity);
		auxGoVideo.GetComponentInChildren<TextMeshProUGUI>().text = " - Textos - ";
		auxGoVideo.transform.SetParent(content.transform);
		auxGoVideo.transform.localScale = new Vector3(1,1,1);

		foreach(var temp in allSearchTypes)
		{
			if(temp.categorie == searchStatus.categorieType.texto)
			{
				GameObject createGO = Instantiate(searchGO,content.transform.position,Quaternion.identity);
				createGO.GetComponent<visualSetSearchThing>().setSearchThing(temp);
				createGO.transform.SetParent(content.transform);
				createGO.transform.localScale = new Vector3(1,1,1);
			}
		}
	}

	public void clearAll()
	{
		foreach (Transform child in content.transform) 
		{
     		GameObject.Destroy(child.gameObject);
		}		
		selectedSearchTypes.Clear();
		encounters.Clear();
		orderedSearch.Clear();
	}
}
