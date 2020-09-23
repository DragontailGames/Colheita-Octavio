using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenStore : MonoBehaviour 
{
    AudioSource myAudio;
    public AudioClip myClip;
	public GameObject StoreUI;

	menuManager MenuManager;

	void Start()
	{
        myAudio = GameObject.Find("FxAudio").GetComponent<AudioSource>();
        MenuManager = GameObject.Find("MenuManager").GetComponent<menuManager>();
	}

	void OnMouseDown()
	{
		if(MenuManager.canOpenMenu())
		{
			StoreUI.SetActive(true);
            PlaySoundOnClick();
			MenuManager.MenuOpen();
		}
	}

    public void PlaySoundOnClick()
    {
        myAudio.PlayOneShot(myClip);
    }
}
