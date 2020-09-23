using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class touchObject : MonoBehaviour 
{
	public GameObject minigameNotice;
    AudioSource myAudio;
    public AudioClip myClip;
    public enum MinigameType {water, cleaninig, nutrients};
	public MinigameType typeMinigame;
	
	menuManager MenuManager;

	void Start()
	{
        myAudio = GameObject.Find("FxAudioButton").GetComponent<AudioSource>();
		MenuManager = GameObject.Find("MenuManager").GetComponent<menuManager>();
	}

	void OnMouseDown()
    {
		if(MenuManager.canOpenMenu())
		{
			switch (typeMinigame)
			{			
				case MinigameType.water:
					Transform auxWater = minigameNotice.transform.Find("Name");
					auxWater.GetComponent<TextMeshProUGUI>().text = "Você deseja iniciar o minigame da Água com custo de 20 Caffeinas";
					minigameNotice.transform.Find("bt_Water").gameObject.SetActive(true);
					break;
				case MinigameType.cleaninig:
					Transform auxCleaning = minigameNotice.transform.Find("Name");
					auxCleaning.GetComponent<TextMeshProUGUI>().text = "Você deseja iniciar o minigame da Limpeza com custo de 20 Caffeinas";
					minigameNotice.transform.Find("bt_Cleaning").gameObject.SetActive(true);
					break;
				case MinigameType.nutrients:
					Transform auxNutrients = minigameNotice.transform.Find("Name");
					auxNutrients.GetComponent<TextMeshProUGUI>().text = "Você deseja iniciar o minigame de Nutrients com custo de 20 Caffeinas";
					minigameNotice.transform.Find("bt_Nutrients").gameObject.SetActive(true);
					break;
			}
            PlaySoundOnClick();
            minigameNotice.SetActive(true);
			MenuManager.MenuOpen();
			GameObject.Find("Canvas").GetComponent<canvasInvisibleSet>().reorder(minigameNotice);

		}

	}
    public void PlaySoundOnClick()
    {
        myAudio.PlayOneShot(myClip);
    }
}
