using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;

public class EventAndQuizCreator : EditorWindow 
{

	[MenuItem("PCO/Event Creator %#m")]
	public static void ShowWindown()
	{
		GetWindow<EventAndQuizCreator>("Criador de Evento");
	}

	string _name;
	string _quest;

	enum groupList{Grupo_1,Grupo_2,Grupo_3};
	groupList _groupList;

	string _rigthAswer;
	string _rigthAswerResult = "Parabens, você acertou por isso o efeito não se ativara";
	string _wrongAswer;
	string _wrongAswerResult = "[Efeito negativo...]";

	GUIStyle effectText = new GUIStyle();

	eventStatus.effect _eventType;

	eventStatus.dropStatus _dropEfx;
	int _valueChange;
	int _duration;
	bool _moneyChange;
	int _money;

	void OnGUI()
	{
		GUILayout.Space(15);
		EditorGUIUtility.labelWidth = 200f;

		GUIContent padrao = new GUIContent();
		padrao.text = "Nome do Evento: ";
		_name = EditorGUILayout.TextField(padrao, _name, GUILayout.Width(500));
		GUILayout.Space(5);

		padrao.text = "Grupo: ";
		_groupList = (groupList)EditorGUILayout.EnumPopup(padrao, _groupList, GUILayout.Width(500));
		GUILayout.Space(9);

		EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
		effectText.alignment = TextAnchor.MiddleCenter;
		effectText.fontStyle = FontStyle.Bold;
		effectText.fontSize = 20;
		GUILayout.Space(10);
		GUILayout.TextField("EVENTO",effectText);
		GUILayout.Space(10);
		EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
		GUILayout.Space(20);

		//Evento

		_eventType = (eventStatus.effect)EditorGUILayout.EnumPopup("Tipo de Evento: ", _eventType);
		GUILayout.Space(5);


		if(_eventType == eventStatus.effect.dropDirect)
		{
			_dropEfx = (eventStatus.dropStatus)EditorGUILayout.EnumPopup("Status para Diminuir: ", _dropEfx);
			GUILayout.Space(5);

			_valueChange = EditorGUILayout.IntField("Valor para tirar: ",_valueChange);
		}

		if(_eventType == eventStatus.effect.Modifier)
		{
			_dropEfx = (eventStatus.dropStatus)EditorGUILayout.EnumPopup("Status para Diminuir: ", _dropEfx);
			GUILayout.Space(5);

			_valueChange = EditorGUILayout.IntField("Valor para tirar: ",_valueChange);

			_duration = 15 * ((EditorGUILayout.IntSlider("Duração do Modificador: ",_duration, 15, 120)) / 15);
		}

		GUILayout.Space(10);
		
		EditorGUILayout.BeginHorizontal();

		_moneyChange = EditorGUILayout.Toggle("Alteração no dinheiro: ",_moneyChange);
		if(_moneyChange == true)
		{
			_money = EditorGUILayout.IntField("Dinheiro: ", _money);
		}

		EditorGUILayout.EndHorizontal();

		GUILayout.Space(10);
		//Final do Evento

		EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
		effectText.alignment = TextAnchor.MiddleCenter;
		effectText.fontStyle = FontStyle.Bold;
		effectText.fontSize = 20;
		GUILayout.Space(10);
		GUILayout.TextField("QUIZ",effectText);
		GUILayout.Space(10);
		EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
		GUILayout.Space(20);


		padrao.text = "Pergunta: ";
		_quest = EditorGUILayout.TextField(padrao, _quest, GUILayout.Width(500), GUILayout.Height(50));
		EditorStyles.textField.wordWrap = true;

		EditorGUILayout.Separator();

		padrao.text = "Resposta Certa: ";
		_rigthAswer = EditorGUILayout.TextField(padrao, _rigthAswer, GUILayout.Width(500),GUILayout.Height(30));
		EditorStyles.textField.wordWrap = true;
		GUILayout.Space(5);

		padrao.text = "Resultado da resposta Certa: ";
		_rigthAswerResult = EditorGUILayout.TextField(padrao, _rigthAswerResult, GUILayout.Width(500), GUILayout.Height(30));
		EditorStyles.textField.wordWrap = true;
		GUILayout.Space(10);

		
		padrao.text = "Resposta Errada: ";
		_wrongAswer = EditorGUILayout.TextField(padrao, _wrongAswer, GUILayout.Width(500), GUILayout.Height(30));
		EditorStyles.textField.wordWrap = true;
		GUILayout.Space(5);

		padrao.text = "Resultado da resposta Errada: ";
		_wrongAswerResult = EditorGUILayout.TextField(padrao, _wrongAswerResult, GUILayout.Width(500), GUILayout.Height(30));
		EditorStyles.textField.wordWrap = true;
		EditorGUILayout.HelpBox("Colocar aqui a descrição do evento",MessageType.Info);	

		GUILayout.Space(15);

		if(GUILayout.Button("Criar Evento E Quiz"))
		{
			var assetQuiz = ScriptableObject.CreateInstance<quizType>();
			ProjectWindowUtil.CreateAsset(assetQuiz, "Assets/Resources/Event/Quiz/" +_name + "_Quiz.asset");
			assetQuiz.init(_name,_quest,_wrongAswer,_rigthAswer,_wrongAswerResult,_rigthAswerResult);
			
			var assetEvent = ScriptableObject.CreateInstance<eventStatus>();

			switch(_groupList)
			{
				case groupList.Grupo_1:
				{
					ProjectWindowUtil.CreateAsset(assetEvent, "Assets/Resources/Event/GroupStage_1/" +_name + "_Event.asset");
					break;
				}
				case groupList.Grupo_2:
				{
					ProjectWindowUtil.CreateAsset(assetEvent, "Assets/Resources/Event/GroupStage_2/" +_name + "_Event.asset");
					break;
				}
				case groupList.Grupo_3:
				{
					ProjectWindowUtil.CreateAsset(assetEvent, "Assets/Resources/Event/GroupStage_3/" +_name + "_Event.asset");
					break;
				}
			}

			if(_eventType == eventStatus.effect.dropDirect)
			{
				if(_moneyChange==true)
				{
					assetEvent.init(_name,_wrongAswerResult,_eventType,_dropEfx,_valueChange,_money,assetQuiz);
				}
				if(_moneyChange==false)
				{
					assetEvent.init(_name,_wrongAswerResult,_eventType,_dropEfx,_valueChange,assetQuiz);
				}
			}
			if(_eventType == eventStatus.effect.Modifier)
			{
				if(_moneyChange==true)
				{
					assetEvent.init(_name,_wrongAswerResult,_eventType,_dropEfx,_valueChange,_duration,_money,assetQuiz);
					
				}
				if(_moneyChange==false)
				{
					assetEvent.init(_name,_wrongAswerResult,_eventType,_duration,_dropEfx,_valueChange, assetQuiz);
				}
			}
			Debug.Log("Criado com sucesso");
		}	
	}

	void createEvent()
	{

	}
}
