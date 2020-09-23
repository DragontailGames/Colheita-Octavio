using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class resourcesManager : MonoBehaviour 
{
	private TimeManager timeManager;

	private float timerCounter;

	#region variableResources;
	private float caffeine;
	private string caffeinePP = "caffeine";
	private float water;
	private string waterPP = "water";
	private float cleaning;
	private string cleaningPP = "cleaning";
	private float nutrients;
	private string nutrientsPP = "nutrients";

	private string moneyPP = "Money";

	#endregion

	#region modifiers

	private float caffeineModifier = 1;

	private float waterModifier = -1;

	private float cleaningModifier = -1;

	private float nutrientsModifier = -1;


	#endregion

	public float hoursCaffeineEnd;

	private pointManager managerPoints;

	float[] outResources; //cafeina,agua,nutrients, limpeza


	DateTime checkOneValue;
	DateTime checkTwoValue;
	DateTime checkTreeValue;
	DateTime checkFourValue;

	public static resourcesManager sharedInstance = null;

	void Awake() 
	{
		if(!PlayerPrefs.HasKey("Money"))
		{
			PlayerPrefs.SetFloat("Money",0);
		}

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
	void Start () 
	{
		testPlayerPrefs(caffeinePP);
		testPlayerPrefs(waterPP);
		testPlayerPrefs(cleaningPP);
		testPlayerPrefs(nutrientsPP);

		timeManager = GameObject.Find("TimerManager").GetComponent<TimeManager>();
		managerPoints = GameObject.Find("PointManager").GetComponent<pointManager>();

		ModifierChange(GameObject.Find("Farm_Selector").GetComponent<farmManager>().typeFarm);

		Invoke("calculateOutTime",1f);
		InvokeRepeating("testMinute",2f,10f);
		InvokeRepeating("testAndSetPlayerPrefs",1f,0.5f);

		checkOneValue = DateTime.Parse(PlayerPrefs.GetString("entranceOnline"));
		checkTwoValue = DateTime.Parse(PlayerPrefs.GetString("entranceOnline")).AddMinutes(15);
		checkTreeValue = DateTime.Parse(PlayerPrefs.GetString("entranceOnline")).AddMinutes(30);
		checkFourValue = DateTime.Parse(PlayerPrefs.GetString("entranceOnline")).AddMinutes(45);

		timeManager.InvokeSaveLastTime();

	}

	private void testPlayerPrefs(string name)
	{
		if(PlayerPrefs.HasKey(name))
		{
			switch(name)
			{
				case "caffeine":
				caffeine = PlayerPrefs.GetFloat(name);
				break;

				case "water":
				water = PlayerPrefs.GetFloat(name);
				break;

				case "cleaning":
				cleaning = PlayerPrefs.GetFloat(name);
				break;

				case "nutrients":
				nutrients = PlayerPrefs.GetFloat(name);
				break;
			}
		}
		else
		{
			PlayerPrefs.SetFloat(name,100);
			switch(name)
			{
				case "caffeine":
				caffeine = PlayerPrefs.GetFloat(name);
				break;

				case "water":
				water = PlayerPrefs.GetFloat(name);
				break;

				case "cleaning":
				cleaning = PlayerPrefs.GetFloat(name);
				break;

				case "nutrients":
				nutrients = PlayerPrefs.GetFloat(name);
				break;
			}
		}
	}
	
	public void SetPlayerPrefs(string name, float value)
	{
		if(PlayerPrefs.HasKey(name))
		{
			PlayerPrefs.SetFloat(name,value);
		}
	}

	public void testAndSetPlayerPrefs()
	{

		if(PlayerPrefs.HasKey(caffeinePP))
		{
			if(PlayerPrefs.GetFloat(caffeinePP) != caffeine)
				SetPlayerPrefs(caffeinePP,caffeine);
		}

		if(PlayerPrefs.HasKey(waterPP))
		{
			if(PlayerPrefs.GetFloat(waterPP) != water)
				SetPlayerPrefs(waterPP,water);
		}
		
		if(PlayerPrefs.HasKey(cleaningPP))
		{
			if(PlayerPrefs.GetFloat(cleaningPP) != cleaning)
				SetPlayerPrefs(cleaningPP,cleaning);
		}

		if(PlayerPrefs.HasKey(nutrientsPP))
		{
			if(PlayerPrefs.GetFloat(nutrientsPP) != nutrients)
				SetPlayerPrefs(nutrientsPP,nutrients);
		}
	}

	

	public float[] outTimeResourcesChanged()
	{
		return outResources;
	}

	private void calculateOutTime()
	{
		outResources = new float[4];
		if(PlayerPrefs.HasKey("lastDateOnline"))
		{
			TimeSpan aux = new TimeSpan(0,30,0);
			TimeSpan dif;

			DateTime lastOnline = DateTime.Parse(PlayerPrefs.GetString("lastDateOnline"));


			dif = timeManager.currentTime() - DateTime.Parse(PlayerPrefs.GetString("lastDateOnline"));

			double totalMinutes = dif.TotalMinutes;

			while(totalMinutes > 0)
			{
				if(lastOnline.ToString("g") != timeManager.currentTime().ToString("g"))
				{
					if(lastOnline.Minute == checkOneValue.Minute || lastOnline.Minute == checkTwoValue.Minute ||
					lastOnline.Minute == checkTreeValue.Minute || lastOnline.Minute == checkFourValue.Minute)
					{
						setAboutTimeResources();
					}
				}

				lastOnline = lastOnline.AddMinutes(1);
				totalMinutes--;
			}
		}
	}

	public void dropStatus(string status, float value)
	{
		switch(status)
		{
			case "water": 
			{
				water -= value;
				break;
			}
			case "nutrients": 
			{
				nutrients -= value;
				break;
			}
			case "cleaning": 
			{
				cleaning -= value;
				break;
			}
			case "caffeine":
			{
				caffeine += value;
				break;
			}
			case "cleaning_nutrients":
			{ 
				cleaning += value;
				nutrients += value;
				break;
			}
			case "cleaning_water":
			{
				cleaning += value;
				water += value;
				break;
			}
			case "nutrients_water":
			{
				water += value;
				nutrients += value;
				break;
			}
			case "all":
			{
				cleaning += value;
				nutrients += value;
				water += value;
				break;
			}
		}
	}

	void testMinute()
	{
		if(timeManager.currentTime().Minute == checkOneValue.Minute || timeManager.currentTime().Minute == checkTwoValue.Minute ||
		timeManager.currentTime().Minute == checkTreeValue.Minute || timeManager.currentTime().Minute == checkFourValue.Minute)
		{
			setAboutTimeResources();
		}
		
	}

	float convertHoursIntoModifiers(float hours)
	{
		return 25/hours;
	}
	void Update()
	{
		testMaxResources();

	}

	public float toFullCaffeine()
	{
		float auxReturn = 0;

			auxReturn = (hoursCaffeineEnd * (100 - caffeine)) / 100;

		return auxReturn;
	}

	public void testMaxResources()
	{
		if(caffeine > 100)
			caffeine = 100;
		if(caffeine < 0)
			caffeine = 0;
		if(water > 100)
			water = 100;
		if(water < 0)
			water = 0;
		if(cleaning > 100)
			cleaning = 100;
		if(cleaning < 0)
			cleaning = 0;
		if(nutrients > 100)
			nutrients = 100;
		if(nutrients < 0)
			nutrients = 0;

	}

	/// <summary>
	/// Metodo para mexer com os recursos com o tempo padrao de 15 Minutos
	/// </summary>
	public void setAboutTimeResources()
	{
		if(PlayerPrefs.HasKey("StatusModifier"))
		{
			string[] aux = PlayerPrefs.GetString("StatusModifier").Split('/');
			string modifierName = aux[0];
			int ticksToChange = int.Parse(aux[1]);
			float value = float.Parse(aux[2]);

			switch(modifierName)
			{
				case "cleaning":
				{
					cleaningModifier *= value;
					break;
				}
				case "water":
				{
					waterModifier *= value;
					break;
				}
				case "nutrients":
				{
					nutrientsModifier *= value;
					break;
				}
				case "cleaning_nutrients":
				{ 
					cleaningModifier *= value;
					nutrientsModifier *= value;
					break;
				}
				case "cleaning_water":
				{
					cleaningModifier *= value;
					waterModifier *= value;
					break;
				}
				case "nutrients_water":
				{
					nutrientsModifier *= value;
					waterModifier *= value;
					break;
				}
				case "all":
				{
					waterModifier *= value;
					cleaningModifier *= value;
					nutrientsModifier *= value;
					break;
				}
			}

			ticksToChange--;
			if(ticksToChange >0)
			{
				PlayerPrefs.SetString("StatusModifier",modifierName+"/"+ticksToChange+"/"+value);
			}
			else
			{
				PlayerPrefs.DeleteKey("StatusModifier");
				PlayerPrefs.DeleteKey("EventDuration");
				PlayerPrefs.DeleteKey("EventName");
			}

		}

		if(PlayerPrefs.HasKey("PointModifier"))
		{
			float ticks = PlayerPrefs.GetFloat("PointModifierDuration");
			managerPoints.setPoints(WaterManager,NutrientsManager,CleaningManager, PlayerPrefs.GetInt("PointModifier"));
			ticks--;
			PlayerPrefs.SetFloat("PointModifierDuration",ticks);
			if(ticks == 0)
			{
				PlayerPrefs.DeleteKey("PointModifierDuration");
				PlayerPrefs.DeleteKey("PointModifier");
			}
		}
		else
		{
			managerPoints.setPoints(WaterManager,NutrientsManager,CleaningManager, 1);
		}

		outResources[0] += caffeineModifier;
		outResources[1] += waterModifier;
		outResources[2] += nutrientsModifier;
		outResources[3] += cleaningModifier;

		CaffeineManager = caffeineModifier;
		WaterManager = waterModifier;
		CleaningManager = cleaningModifier;
		NutrientsManager = nutrientsModifier;
	}

	#region ManagerModifiers

	private void ModifierChange(farmType type)
	{
		waterModifier = -convertHoursIntoModifiers(type.waterHours);
		cleaningModifier = -convertHoursIntoModifiers(type.cleaningHours);
		nutrientsModifier = -convertHoursIntoModifiers(type.nutrientsHours);
		caffeineModifier = +convertHoursIntoModifiers(hoursCaffeineEnd);

	}

	#endregion

	#region  ManagerGetterSetter
	public float CaffeineManager
	{
		get { return caffeine; }
        set { caffeine += value; }
	}
	public float WaterManager
	{
		get { return water; }
        set { water += value; }
	}
		public float CleaningManager
	{
		get { return cleaning; }
        set { cleaning += value; }
	}
		public float NutrientsManager
	{
		get { return nutrients; }
        set { nutrients += value; }
	}

	#endregion
}
