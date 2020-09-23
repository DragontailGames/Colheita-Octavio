using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
 
public class TimeManager : MonoBehaviour 
{
    public static TimeManager sharedInstance = null;
    //url link em php o modelo da pagina tem que ser:
    /*
        <?php
        date_default_timezone_set('America/Sao_Paulo');
        $currenttime = date("m-d-Y H:i:s");
        list($ddd,$ttt) = explode(' ',$currenttime);
        echo "$ddd/$ttt\n";
        ?>
     */
     //depois colocar o link da pagina online aqui
    private string _url = "https://timermanager.000webhostapp.com/";
    //Todos os arquivos dentro da pagina online
    private string _timeData;
    //A hora atual
    private string _startDateTime;

    private bool canStart = true;
 
    private TimeSpan outTime;
    
    //Tratativa para intancia duplicada
    void Awake() 
    {
        StartCoroutine ("getTime");
        if (sharedInstance == null)
        {
            sharedInstance = this;
        } 
        else if (sharedInstance != this)
        {
            Destroy (gameObject);  
        }
        DontDestroyOnLoad(gameObject);
        
    }
  
    void Start()
    {
        if(!PlayerPrefs.HasKey("entranceOnline"))
        {
            Invoke("setEntranceOnline",2.5f);
        }
        //Invoke("tryResetCycle",3.0f); Pedro melhorar o final do ciclo
        if(PlayerPrefs.HasKey("outTime"))
        {
            PlayerPrefs.DeleteKey("outTime");
        }

    }

    void tryResetCycle()
    {
        if(currentTime().Day == 1)
        {
            if(PlayerPrefs.HasKey("LastCicleReset"))
            {
                if(PlayerPrefs.GetString("LastCicleReset") != DateTime.Now.Date.ToString())
                {
                    PlayerPrefs.DeleteAll();
                    PlayerPrefs.SetString("LastCicleReset", currentTime().ToString());
                }
            }
            else
            {
                PlayerPrefs.SetString("LastCicleReset", DateTime.Now.Date.ToString());
            }
        }
    }

    public void InvokeSaveLastTime()
    {
        InvokeRepeating("saveLastTime",2f,2f);
    }

    void setEntranceOnline()
    {
        PlayerPrefs.SetString("entranceOnline", currentTime().ToString());
    }

    void Update()
    {
        currentTime().ToString();
    }

    void saveLastTime()
    {
        PlayerPrefs.SetString("lastDateOnline", currentTime().ToString());
    }

    //Metodo que retorna o DateTime atual (data e hora)
    public DateTime currentTime()
    {
        return DateTime.Now;
    }

    public float getHour{get{return currentTime().Hour;}}
 
    //Corrotina que busca a hora e o dia no site
    public IEnumerator getTime()
    {
        WWW www = new WWW (_url);
        yield return www;
        _timeData = www.text;
        string[] words = _timeData.Split('/');

        _startDateTime = words[0] + " " +words[1];
        if((DateTime.Parse(_startDateTime).ToString("g")) != DateTime.Now.ToString("g"))
        {
            Debug.Log("Algum erro ocorreu com o tempo");
            //pedro
        }

    }
}
