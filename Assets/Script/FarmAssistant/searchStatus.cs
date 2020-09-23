using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

#if UNITY_EDITOR
    using UnityEditor;
#endif

[CreateAssetMenu (menuName = "Octavio Types/search Status")]
public class searchStatus : ScriptableObject 
{
	public Sprite thumb;
	public string nameThing = "";
	[TextArea(4,10)]
	public string description = "";

	public enum categorieType
	{
		video,
		texto
	}

	public categorieType categorie;
	public VideoClip videoToPlay;

	[Header("-- Key Words --")]
	public int minCharKW = 2;
	public List<string> keySearch;

	public void setKeySearch()
	{
		keySearch.Clear();
		string [] keySearchAux = description.Split(' ');
		foreach(string aux in keySearchAux)
		{
			if(aux.ToCharArray().Length > minCharKW)
			{
				keySearch.Add(aux);
			}
		}
	}
}

#if UNITY_EDITOR

[CustomEditor(typeof(searchStatus))]
public class searchEditor : Editor
{
    public override void OnInspectorGUI()
    {        
		DrawDefaultInspector();

        searchStatus myScript = (searchStatus)target;
        if(GUILayout.Button("Save KeyWords"))
        {
            myScript.setKeySearch();
        }
    }
}
#endif
