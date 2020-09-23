using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
[CreateAssetMenu (menuName = "Octavio Types/Farm")]
public class farmType : ScriptableObject 
{
	public string farmName;
	public float cost;
	
	[Header("Hours to this resource to become 0")]
	public float waterHours;
	public float cleaningHours;
	public float nutrientsHours;

	public bool buyed;
}

#if UNITY_EDITOR
[CustomEditor(typeof(farmType))]
public class farmType_Editor : Editor
{
	farmType myScript;
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector(); 

		myScript = (farmType)target;

		EditorUtility.SetDirty(myScript);

		if(GUILayout.Button("reset"))
			 reset();
	}

	public void reset()
	{
		myScript.buyed = false;
	}
}
#endif
