using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class visualSetSearchThing : MonoBehaviour 
{
	searchStatus searchThing;

	public void setSearchThing(searchStatus auxSearch)
	{
		searchThing = auxSearch;

		this.transform.Find("Thumb").GetComponent<Image>().sprite = searchThing.thumb;
		this.transform.Find("Title").GetComponent<TextMeshProUGUI>().text = searchThing.nameThing;
		this.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = searchThing.description;
	}

	public void playVideo()
	{		
		GameObject.Find("VideoManager").GetComponent<playVideoOnCanvas>().StartVideo(searchThing.videoToPlay);
	}
}
