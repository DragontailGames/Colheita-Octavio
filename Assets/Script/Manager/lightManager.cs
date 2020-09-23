using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class lightManager : MonoBehaviour 
{
	public List<lightType> listLights = new List<lightType>();

	private Light myLight;

	public GameObject house;
	[Tooltip("M1 normal, M2 Brilhante")]
	public Material especialMaterial;

	void Awake () 
	{
		myLight = this.GetComponent<Light>();

		listLights.Clear();

		foreach(var item in Resources.LoadAll("LightType",typeof(lightType)))
		{
			listLights.Add((lightType)item);
		}

		foreach (var item in listLights)
		{
			if(item.begin<DateTime.Now.Hour && item.end>DateTime.Now.Hour)
			{
				myLight.color = item.ligthColor;
				myLight.intensity = item.intensity;

				if(item.turnOnLights)
				{
					turnOnLigths();
				}
			}
			else if((DateTime.Now.Hour>item.begin || DateTime.Now.Hour<item.end) && item.ligthName == "Noite")
			{
				myLight.color = item.ligthColor;
				myLight.intensity = item.intensity;

				if(item.turnOnLights)
				{
					turnOnLigths();
				}
			}
		}
	}

	public void testLigth(Color _color, float _intensity)
	{
		myLight = this.GetComponent<Light>();
		myLight.color = _color;
		myLight.intensity = _intensity;
	}

	public void turnOnLigths()
	{
		foreach (var item in GameObject.FindObjectsOfType<Light>())
		{
			item.transform.GetComponent<Light>().enabled = true;
		}

		var cachedMaterial = house.GetComponent<Renderer>().materials;

		var intMaterials = new Material[cachedMaterial.Length];
		for(int i=0; i<intMaterials.Length;i++)
		{
			if(i == 1)
				intMaterials[i] = especialMaterial;
				
			else
				intMaterials[i] = house.GetComponent<Renderer>().materials[i];
		}
		house.GetComponent<Renderer>().materials = intMaterials;				
	}
}

#if UNITY_EDITOR
[SerializeField]
[CustomEditor(typeof(lightManager))]
public class lightManagerEditor : Editor
{
	string _ligthName;
	Color _ligthColor;
	float _intensity = 1.00f;

	int _begin = 0,_end = 0;

	bool _turnOnLights = false;

	public override void OnInspectorGUI()
	{
		lightManager myTarget = (lightManager)target;

		DrawDefaultInspector ();

		EditorUtility.SetDirty(myTarget);

		foreach(var item in Resources.LoadAll("LightType",typeof(lightType)))
		{
			lightType aux = (lightType)item;
			if(!myTarget.listLights.Contains(aux))
				myTarget.listLights.Add(aux);
		}
	
		_ligthName = EditorGUILayout.TextField("Nome: ", _ligthName);

		EditorGUILayout.BeginHorizontal();
		
		_begin = EditorGUILayout.IntField("Begin: ", _begin,GUILayout.Width(175));

		_end = EditorGUILayout.IntField("End: ", _end,GUILayout.Width(175));

		EditorGUILayout.EndHorizontal();

		_ligthColor = EditorGUILayout.ColorField("Cor da Luz: ", _ligthColor);

		EditorGUILayout.BeginHorizontal();

		_intensity = EditorGUILayout.Slider(_intensity, 0, 5.00f) ;

		_turnOnLights = EditorGUILayout.Toggle("On Ligths: ", _turnOnLights);

		EditorGUILayout.EndHorizontal();

		if(GUILayout.Button("Testar Luz"))
		{
			myTarget.testLigth(_ligthColor,_intensity);
		}

		EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

		if(GUILayout.Button("Salvar Luzes"))
		{
			var ligthType = ScriptableObject.CreateInstance<lightType>();
			ProjectWindowUtil.CreateAsset(ligthType, "Assets/Resources/LightType/" + _ligthName + ".asset");
		
			ligthType.init(_ligthName,_ligthColor,_intensity, _begin,_end,_turnOnLights);
		}
	}
	
}
#endif



