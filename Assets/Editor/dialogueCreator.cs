using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;

public class dialogueCreator : EditorWindow 
{
    [MenuItem("PCO/Dialogue Creator")]
    private static void ShowWindow() 
    {
        var window = GetWindow<dialogueCreator>();
        window.titleContent = new GUIContent("Dialogue Creator");
        window.Show();
    }

    private string arquiveName = "MainText";

    private string textString;
    private string stringTag;

    GUIStyle effectText = new GUIStyle();

    private void OnGUI() 
    {
		EditorGUIUtility.labelWidth = 75f;

		GUIContent padrao = new GUIContent();

        EditorGUILayout.BeginHorizontal();

        padrao.text = "Archieve:";
		arquiveName = EditorGUILayout.TextField(padrao, arquiveName, GUILayout.Width(200));

        if(GUILayout.Button("Create", GUILayout.Width(65)))
        {
            if(arquiveName != null)
            {
                if(!File.Exists("./Assets/Resources/Dialogue/Texts/"+arquiveName+".txt"))
                {
                    Debug.Log("Create");
                    File.CreateText("./Assets/Resources/Dialogue/Texts/"+arquiveName+".txt");
                }
            }
        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        padrao.text = "Tag:";
		stringTag = EditorGUILayout.TextField(padrao, stringTag, GUILayout.Width(200));

        if(GUILayout.Button("Clear", GUILayout.Width(50)))
        {
            textString = "";
            stringTag = "";
        }

        if(GUILayout.Button("Add NL", GUILayout.Width(50)))
        {
            string final = "<" + stringTag +">" + textString;

            //Pass the filepath and filename to the StreamWriter Constructor
            string line;

            List<string> baseStrings = new List<string>();

            //Pass the file path and file name to the StreamReader constructor
            StreamReader sr = new StreamReader("./Assets/Resources/Dialogue/Texts/"+arquiveName+".txt");

            line = sr.ReadLine();

            while (line != null) 
            {
                baseStrings.Add(line);
                //Read the next line
                line = sr.ReadLine();
            }

            sr.Close();

            StreamWriter sw = new StreamWriter("./Assets/Resources/Dialogue/Texts/MainText.txt");


            foreach (var item in baseStrings)
            {
                sw.WriteLine(item);
            }

            sw.WriteLine(final);

            Debug.Log("Texto adicionado");

            //Close the file
            sw.Close();        
        }

        EditorGUILayout.EndHorizontal();

        padrao.text = "Text:";

        textString = EditorGUILayout.TextField(padrao, textString, GUILayout.Width(320), GUILayout.Height(75));
		EditorStyles.textField.wordWrap = true;

        EditorGUILayout.HelpBox("Digite uma TAG em Caps e depois o texto relacionado a TAG, depois Aperte ADD NL para concluir",MessageType.Info);

        var info = new DirectoryInfo("Assets/Resources/Dialogue/Texts/");
        var fileInfo = info.GetFiles();
        
		GUILayout.Space(8);

        effectText.alignment = TextAnchor.MiddleCenter;
		effectText.fontStyle = FontStyle.Bold;
		effectText.fontSize = 12;

        EditorGUILayout.LabelField("ARQUIVOS CRIADOS:",effectText);

		GUILayout.Space(5);

        effectText.fontSize = 12;
        effectText.fontStyle = FontStyle.Normal;

        foreach (var item in fileInfo)
        {
            if(item.Name.EndsWith(".txt"))
            {
                EditorGUILayout.LabelField(item.Name,effectText);
            }
        }
    }
}
