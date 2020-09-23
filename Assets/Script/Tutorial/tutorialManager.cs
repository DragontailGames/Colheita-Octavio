using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using System.Linq;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class tutorialManager : MonoBehaviour
{
    public Dictionary<string,string> texts = new Dictionary<string, string>();
    tutorialWrite write;

    enum tutorialTypes 
    {
        Stage,
        Generic,
        Resources,
        Minigame,
        Event
    }

    tutorialTypes myTutorial;

    public GameObject tutorialStage,tutorialStage_1,tutorialStage_2,tutorialStage_3,tutorialStageClose;
    public GameObject tutorialResources_1,tutorialResources_2;
    public GameObject tutorialMinigame_1,tutorialMinigame_2,tutorialMinigame_3,tutorialMinigame_4,tutorialMinigame_5;
    public GameObject tutorialMinigame_6,tutorialMinigame_7,tutorialMinigame_8,tutorialMinigame_9,tutorialMinigame_10,tutorialMinigame_11;
    public GameObject tutorialEvent_01,tutorialEvent_02,tutorialEvent_03;
    public GameObject tutorialRadialMenu_1,tutorialRadialMenu_2,tutorialRadialMenu_3,tutorialRadialMenu_4,tutorialRadialMenu_5,tutorialRadialMenu_6,tutorialRadialMenu_7;
    public GameObject tutorialCicle_1,tutorialCicle_2,tutorialCicle_3,tutorialCicle_4,tutorialCicle_5;
    public GameObject tutorialMoney, tutorialHouse;
    public GameObject tutorialStore_1,tutorialStore_2,tutorialStore_3,tutorialStore_4,tutorialStore_5;


    int indexTutorial = 0;

    private bool canSkip = false;

    public GameObject invisibleNoClickPanel;

    public static tutorialManager sharedInstance = null;

    private bool canEvent = true;

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

        PlayerPrefs.DeleteAll();

    }

    void Start()
    {
        startRead();
        write = this.gameObject.GetComponent<tutorialWrite>();
        setTutorial(0);
    }

    void Update()
    {
        if(Input.GetMouseButton(0) && canSkip && write.endWrite())
        {
            indexTutorial++;
            canSkip = false;
            if(myTutorial == tutorialTypes.Stage)
            {
                setTutorial(indexTutorial);
            }
            if(myTutorial == tutorialTypes.Resources)
            {
                setTutorial(indexTutorial);
            }
            if(myTutorial == tutorialTypes.Minigame)
            {
                setTutorial(indexTutorial);
            }
            if(myTutorial == tutorialTypes.Event)
            {
                setTutorial(indexTutorial);
            }
        }

        if(namesReadsCount>=3 && Input.GetMouseButton(0) && indexTutorial<7)
        {
            setTutorial(7);
        }

        if(indexTutorial==18 && SceneManager.GetActiveScene().name == "Tutorial" && canEvent)
        {
            setTutorial(19);
        }
    }

    private GameObject radialMenu;

    public void setTutorial(int index)
    {
        switch (index)
        {
            case 0://Abrir o menu de Stage
            {
                disableInvPanel();
                myTutorial = tutorialTypes.Stage;
                write.writeText(texts["STAGE"], tutorialStage.transform.Find("genericText").GetComponent<TextMeshProUGUI>());
                enableBlackPanel(tutorialStage);
                break;
            }
            case 1:
            {
                write.writeText(texts["STAGE_1"], tutorialStage_1.transform.Find("genericText").GetComponent<TextMeshProUGUI>());
                enableBlackPanel(tutorialStage_1);
                indexTutorial = 1;
                canSkip = true;
                break;
            }
            case 2:
            {
                disableTutorial(tutorialStage_1);
                write.writeText(texts["STAGE_2"], tutorialStage_2.transform.Find("genericText").GetComponent<TextMeshProUGUI>());
                enableBlackPanel(tutorialStage_2);
                canSkip = true;
                break;
            }
            case 3:
            {
                disableTutorial(tutorialStage_2);
                write.writeText(texts["STAGE_3"], tutorialStage_3.transform.Find("genericText").GetComponent<TextMeshProUGUI>());
                enableBlackPanel(tutorialStage_3);
                canSkip = true;
                break;
            }
            case 4:
            {
                disableTutorial(tutorialStage_3);
                myTutorial = tutorialTypes.Generic;
                write.writeText(texts["STAGECLOSE"], tutorialStageClose.transform.Find("genericText").GetComponent<TextMeshProUGUI>());
                enableBlackPanel(tutorialStageClose);
                break;
            }
            case 5:
            {
                GameObject.Find("ResourcesManager").GetComponent<resourcesManager>().dropStatus("cleaning",75);
                GameObject.Find("ResourcesManager").GetComponent<resourcesManager>().dropStatus("nutrients",43);
                GameObject.Find("ResourcesManager").GetComponent<resourcesManager>().dropStatus("water",18);
                myTutorial = tutorialTypes.Resources;
                write.writeText(texts["RESOURCES_1"], tutorialResources_1.transform.Find("genericText").GetComponent<TextMeshProUGUI>());
                enableBlackPanel(tutorialResources_1);
                indexTutorial = 5;
                canSkip = true;
                break;
            }
            case 6:
            {
                disableTutorial(tutorialResources_1);
                write.writeText(texts["RESOURCES_2"], tutorialResources_2.transform.Find("genericText").GetComponent<TextMeshProUGUI>());
                enableBlackPanel(tutorialResources_2);
                break;
            }
            case 7:
            {
                disableTutorial(tutorialResources_2);
                myTutorial = tutorialTypes.Minigame;
                write.writeText(texts["MINIGAME_1"], tutorialMinigame_1.transform.Find("genericText").GetComponent<TextMeshProUGUI>());
                enableBlackPanel(tutorialMinigame_1);
                canSkip = true;
                indexTutorial = 7;
                break;
            }
            case 8:
            {
                disableTutorial(tutorialMinigame_1);
                write.writeText(texts["MINIGAME_2"], tutorialMinigame_2.transform.Find("genericText").GetComponent<TextMeshProUGUI>());
                enableBlackPanel(tutorialMinigame_2);
                canSkip = true;
                break;
            }
            case 9:
            {
                disableTutorial(tutorialMinigame_2);
                write.writeText(texts["MINIGAME_3"], tutorialMinigame_3.transform.Find("genericText").GetComponent<TextMeshProUGUI>());
                enableBlackPanel(tutorialMinigame_3);
                canSkip = true;
                break;
            }
            case 10:
            {
                disableTutorial(tutorialMinigame_3);
                write.writeText(texts["MINIGAME_4"], tutorialMinigame_4.transform.Find("genericText").GetComponent<TextMeshProUGUI>());
                enableBlackPanel(tutorialMinigame_4);
                break;
            }
            case 11://Abrindo a cena do minigame
            {
                disableTutorial(tutorialMinigame_5);
                write.writeText(texts["MINIGAME_6"], tutorialMinigame_6.transform.Find("genericText").GetComponent<TextMeshProUGUI>());
                enableBlackPanel(tutorialMinigame_6);
                indexTutorial = 11;
                canSkip = true;

                break;
            }
            case 12:
            {
                disableTutorial(tutorialMinigame_6);
                write.writeText(texts["MINIGAME_7"], tutorialMinigame_7.transform.Find("genericText").GetComponent<TextMeshProUGUI>());
                enableBlackPanel(tutorialMinigame_7);
                break;
            }
            case 13:
            {
                disableTutorial(tutorialMinigame_7);
                write.writeText(texts["MINIGAME_8"], tutorialMinigame_8.transform.Find("genericText").GetComponent<TextMeshProUGUI>());
                enableBlackPanel(tutorialMinigame_8);
                canSkip = true;
                indexTutorial = 13;
                break;
            }
            case 14:
            {
                disableTutorial(tutorialMinigame_8);
                write.writeText(texts["MINIGAME_9"], tutorialMinigame_9.transform.Find("genericText").GetComponent<TextMeshProUGUI>());
                enableBlackPanel(tutorialMinigame_9);
                canSkip = true;
                break;
            }
            case 15:
            {
                disableTutorial(tutorialMinigame_9);
                write.writeText(texts["MINIGAME_10"], tutorialMinigame_10.transform.Find("genericText").GetComponent<TextMeshProUGUI>());
                enableBlackPanel(tutorialMinigame_10);
                canSkip = true;
                break;
            }
            case 16:
            {
                disableTutorial(tutorialMinigame_10);
                GameObject.Find("GameManager").GetComponent<MinigameManager>().startCountdown();
                GameObject.Find("GameManager").GetComponent<MinigameManager>().alternateTutorial();
                break;
            }
            case 17:
            {
                write.writeText(texts["MINIGAME_11"], tutorialMinigame_11.transform.Find("genericText").GetComponent<TextMeshProUGUI>());
                enableBlackPanel(tutorialMinigame_11);
                indexTutorial = 17;
                canSkip = true;
                break;
            }
            case 18:
            {
                SceneManager.LoadScene("Tutorial");
                disableTutorial(tutorialMinigame_11);
                indexTutorial = 18;
                break;
            }
            case 19:
            {
                canEvent = false;
                write.writeText(texts["EVENT_1"], tutorialEvent_01.transform.Find("genericText").GetComponent<TextMeshProUGUI>());
                enableBlackPanel(tutorialEvent_01);
                myTutorial = tutorialTypes.Event;
                break;
            }
            case 20:
            {
                disableTutorial(tutorialEvent_01);
                write.writeText(texts["EVENT_2"], tutorialEvent_02.transform.Find("genericText").GetComponent<TextMeshProUGUI>());
                enableBlackPanel(tutorialEvent_02);
                indexTutorial = 20;
                GameObject.Find("eventManager").GetComponent<castEvent>().callEventTutorial();
                canSkip = true;
                break;
            }
            case 21:
            {
                disableTutorial(tutorialEvent_02);
                write.writeText(texts["EVENT_3"], tutorialEvent_03.transform.Find("genericText").GetComponent<TextMeshProUGUI>());
                enableBlackPanel(tutorialEvent_03);
                canSkip = true;
                break;
            }
            case 22:
            {
                disableTutorial(tutorialEvent_03);
                write.writeText(texts["RADIALMENU_1"], tutorialRadialMenu_1.transform.Find("genericText")
                .GetComponent<TextMeshProUGUI>());
                enableBlackPanel(tutorialRadialMenu_1);
                radialMenu = GameObject.Find("RadialMenu").gameObject;
                radialMenu.SetActive(false);
                indexTutorial = 22;
                canSkip = true;
                break;
            }
            case 23:
            {
                disableTutorial(tutorialRadialMenu_1);
                write.writeText(texts["RADIALMENU_2"], tutorialRadialMenu_2.transform.Find("genericText")
                .GetComponent<TextMeshProUGUI>());
                enableBlackPanel(tutorialRadialMenu_2);
                break;
            }
            case 24:
            {
                disableTutorial(tutorialRadialMenu_2);
                write.writeText(texts["RADIALMENU_3"], tutorialRadialMenu_3.transform.Find("genericText")
                .GetComponent<TextMeshProUGUI>());
                enableBlackPanel(tutorialRadialMenu_3);
                indexTutorial = 24;
                canSkip = true;
                break;
            }
            case 25:
            {
                disableTutorial(tutorialRadialMenu_3);
                write.writeText(texts["RADIALMENU_4"], tutorialRadialMenu_4.transform.Find("genericText")
                .GetComponent<TextMeshProUGUI>());
                enableBlackPanel(tutorialRadialMenu_4);
                canSkip = true;
                break;
            }
            case 26:
            {
                disableTutorial(tutorialRadialMenu_4);
                write.writeText(texts["RADIALMENU_5"], tutorialRadialMenu_5.transform.Find("genericText")
                .GetComponent<TextMeshProUGUI>());
                enableBlackPanel(tutorialRadialMenu_5);
                canSkip = true;
                break;
            }
            case 27:
            {
                disableTutorial(tutorialRadialMenu_5);
                write.writeText(texts["RADIALMENU_6"], tutorialRadialMenu_6.transform.Find("genericText")
                .GetComponent<TextMeshProUGUI>());
                enableBlackPanel(tutorialRadialMenu_6);
                canSkip = true;
                break;
            }
            case 28:
            {
                disableTutorial(tutorialRadialMenu_6);
                write.writeText(texts["RADIALMENU_7"], tutorialRadialMenu_7.transform.Find("genericText")
                .GetComponent<TextMeshProUGUI>());
                enableBlackPanel(tutorialRadialMenu_7);
                canSkip = true;
                break;
            }
            case 29:
            {
                disableTutorial(tutorialRadialMenu_7);
                radialMenu.SetActive(true);
                write.writeText(texts["CICLE_1"], tutorialCicle_1.transform.Find("genericText")
                .GetComponent<TextMeshProUGUI>());
                enableBlackPanel(tutorialCicle_1);
                canSkip = true;
                break;
            }
            case 30:
            {
                disableTutorial(tutorialCicle_1);
                write.writeText(texts["CICLE_2"], tutorialCicle_2.transform.Find("genericText")
                .GetComponent<TextMeshProUGUI>());
                enableBlackPanel(tutorialCicle_2);
                canSkip = true;
                break;
            }
            case 31:
            {
                disableTutorial(tutorialCicle_2);
                write.writeText(texts["CICLE_3"], tutorialCicle_3.transform.Find("genericText")
                .GetComponent<TextMeshProUGUI>());
                enableBlackPanel(tutorialCicle_3);
                canSkip = true;
                break;
            }
            case 32:
            {
                disableTutorial(tutorialCicle_3);
                write.writeText(texts["CICLE_4"], tutorialCicle_4.transform.Find("genericText")
                .GetComponent<TextMeshProUGUI>());
                enableBlackPanel(tutorialCicle_4);
                canSkip = true;
                break;
            }
            case 33:
            {
                disableTutorial(tutorialCicle_4);
                write.writeText(texts["CICLE_5"], tutorialCicle_5.transform.Find("genericText")
                .GetComponent<TextMeshProUGUI>());
                enableBlackPanel(tutorialCicle_5);
                canSkip = true;
                break;
            }
            case 34:
            {
                disableTutorial(tutorialCicle_5);
                write.writeText(texts["DINHEIRO"], tutorialMoney.transform.Find("genericText")
                .GetComponent<TextMeshProUGUI>());
                enableBlackPanel(tutorialMoney);
                canSkip = true;
                break;
            }
            case 35:
            {
                disableTutorial(tutorialMoney);
                write.writeText(texts["HOUSE"], tutorialHouse.transform.Find("genericText")
                .GetComponent<TextMeshProUGUI>());
                enableBlackPanel(tutorialHouse);
                break;
            }
            case 36:
            {
                disableTutorial(tutorialHouse);
                write.writeText(texts["STORE_1"], tutorialStore_1.transform.Find("genericText")
                .GetComponent<TextMeshProUGUI>());
                indexTutorial = 36;
                enableBlackPanel(tutorialStore_1);
                canSkip = true;
                break;
            }
            case 37:
            {
                disableTutorial(tutorialStore_1);
                write.writeText(texts["STORE_2"], tutorialStore_2.transform.Find("genericText")
                .GetComponent<TextMeshProUGUI>());
                enableBlackPanel(tutorialStore_2);
                canSkip = true;
                break;
            }
            case 38:
            {
                disableTutorial(tutorialStore_2);
                write.writeText(texts["STORE_3"], tutorialStore_3.transform.Find("genericText")
                .GetComponent<TextMeshProUGUI>());
                enableBlackPanel(tutorialStore_3);
                canSkip = true;
                break;
            }
            case 39:
            {
                disableTutorial(tutorialStore_3);
                write.writeText(texts["STORE_4"], tutorialStore_4.transform.Find("genericText")
                .GetComponent<TextMeshProUGUI>());
                enableBlackPanel(tutorialStore_4);
                canSkip = true;
                break;
            }
            case 40:
            {
                disableTutorial(tutorialStore_4);
                write.writeText(texts["STORE_5"], tutorialStore_5.transform.Find("genericText")
                .GetComponent<TextMeshProUGUI>());
                enableBlackPanel(tutorialStore_5);
                canSkip = true;
                break;
            }  
            default:
            {
                disableTutorial(tutorialStore_5);
                break;
            }          
        }
    }

    private void disableInvPanel(){invisibleNoClickPanel.SetActive(false);}

    private void enableBlackPanel(GameObject aux)
    {
        aux.SetActive(true);
        aux.transform.Find("Blocker").GetComponent<Image>().enabled = true;
    }

    private void disableTutorial(GameObject aux)
    {
        aux.SetActive(false);
    }

    public void stageMenuOpen()
    {
        disableTutorial(tutorialStage);
        setTutorial(1);
    }
    public void stageMenuClose()
    {
        disableTutorial(tutorialStageClose);
        setTutorial(5);
    }

    public void openMinigameNote()
    {
        write.writeText(texts["MINIGAME_5"], tutorialMinigame_5.transform.Find("genericText").GetComponent<TextMeshProUGUI>());
    }
    public void openMinigameScene(){setTutorial(11);}
    public void startMinigameBt(){setTutorial(13);}
    public void wrongAswer(){setTutorial(20);}


    private string[] namesReads = new string[3];
    private int namesReadsCount = 0;

    public void viewResource(string rName)
    {
        Debug.Log(rName);
        if(!namesReads.Contains(rName))
        {
            for(var i =0;i<namesReads.Length;i++)
            {
                if(namesReads[i]==null || namesReads[i] =="")
                {
                    namesReads[i] = rName;
                    namesReadsCount++;
                    break;
                }
            }
        }
        
    }
    public void startRead()
    {
        string line;
        List<string> baseStrings = new List<string>();

        var info = new DirectoryInfo("Assets/Resources/Dialogue/Texts/");
        var fileInfo = info.GetFiles();

        foreach (var item in fileInfo)
        {
            if(item.Name.EndsWith(".txt"))
            {
                StreamReader sr = new StreamReader("./Assets/Resources/Dialogue/Texts/"+item.Name);

                line = sr.ReadLine();

                while (line != null) 
                {
                    baseStrings.Add(line);
                    //Read the next line
                    line = sr.ReadLine();
                }

                sr.Close();
            }
        }

        foreach (var item in baseStrings)
        {
            var aux = item.Split('>');
            if(!texts.ContainsKey(aux[0].Trim('<')))
            {
                texts.Add(aux[0].Trim('<'),aux[1]);
            }
        }
    }

}

#if UNITY_EDITOR
[CustomEditor(typeof(tutorialManager))]
public class tutorialManager_Editor : Editor
{

    bool foldOne = false;
    int index = 0;

	public override void OnInspectorGUI()
	{

        DrawDefaultInspector();

        tutorialManager script = (tutorialManager)target;

        GUILayout.ExpandWidth (false);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("[TAG]",GUILayout.Width(100));
        EditorGUILayout.LabelField("[Texto]");
        EditorGUILayout.EndHorizontal();

        script.startRead();

        //Draw
        foreach (var item in script.texts)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(item.Key,GUILayout.Width(100));
            EditorGUILayout.LabelField(item.Value);
            EditorGUILayout.EndHorizontal();
        }
	}
}
#endif
