using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
	public static GameManager sharedInstance = null;

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else if (sharedInstance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

    }

	/// <summary>
	/// Resolvendo o bug do jogador ficar fora do jogo
	/// </summary>
	void OnApplicationPause(bool pauseStatus)
	{
        if(pauseStatus==false)
        {
            if(PlayerPrefs.HasKey("outTime"))
            {
                TimeSpan aux = new TimeSpan(0,30,0);//TEMPO DE RESET
                if(DateTime.Now - DateTime.Parse(PlayerPrefs.GetString("outTime"))>=aux)
                {
                    foreach(var item in GameObject.FindGameObjectsWithTag("DDOL"))
                    {
                        Destroy (item,0.5f);
                    }

                    GameObject.Find("Loader").GetComponent<loadScene>().loadWithDelay("Farm_Seed Selector");
                }
            }
        }
        if(pauseStatus == true)
        {
            PlayerPrefs.SetString("outTime",DateTime.Now.ToString());
        }
	}

    void testChangeScene()
    {
        SceneManager.LoadScene("Farm_Seed Selector");

    }
}
