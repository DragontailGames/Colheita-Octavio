using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[CreateAssetMenu (menuName = "Event/Event Status")]
public class eventStatus : ScriptableObject
{
    public string eventName;
    
    [TextArea(2,10)]
    public string description;

    public enum effect{Modifier, dropDirect, Money, Conditional, minigameCost, changePoints, stageModifier}

    public effect effectEvent;
    
    public enum dropStatus{cleaning, nutrients, water, nothing, all, cleaning_nutrients, cleaning_water, nutrients_water, caffeine}

    [HideInInspector]
    public dropStatus dropDirectStatus = dropStatus.nothing;

    [HideInInspector]
    public dropStatus dropStatusModifier = dropStatus.nothing;
    
    [HideInInspector]
    public int modifier;

    [HideInInspector]
    public float moneyChange = 0;

    [HideInInspector]
    public int timeDuration;

    [HideInInspector]
    public eventConditional hasCondition;

    public enum minigame{cleaning, nutrients, water, nothing, all, cleaning_nutrients, cleaning_water, nutrients_water}

    [HideInInspector]
    public minigame changeMinigame = minigame.nothing;

    [HideInInspector]
    public float minigameCost;

    [HideInInspector]
    public float timeReduceToEndNextSage;

    public quizType _quiz;

    public void init(string _name, string _description, effect _efc, dropStatus _dropStatus, int _modifier, quizType _quizType)
    {
        eventName = _name;
        description = _description;
        effectEvent = _efc;
        dropDirectStatus = _dropStatus;
        modifier = _modifier;
        _quiz = _quizType;
    }
    public void init(string _name, string _description, effect _efc, dropStatus _dropStatusDirect, int _modifier, int _money, quizType _quizType)
    {
        eventName = _name;
        description = _description;
        effectEvent = _efc;
        dropDirectStatus = _dropStatusDirect;
        modifier = _modifier;
        moneyChange = _money;
        _quiz = _quizType;
    }

    public void init(string _name, string _description, effect _efc, int _modTime,dropStatus _dropStatusModifier, int _modifier, quizType _quizType)
    {
        eventName = _name;
        description = _description;
        effectEvent = _efc;
        dropStatusModifier = _dropStatusModifier;
        modifier = _modifier;
        timeDuration = _modTime;
        _quiz = _quizType;
    }
    public void init(string _name, string _description, effect _efc, dropStatus _dropStatus, int _modifier,int _modTime ,int _money, quizType _quizType)
    {
        eventName = _name;
        description = _description;
        effectEvent = _efc;
        dropStatusModifier = _dropStatus;
        modifier = _modifier;
        timeDuration = _modTime;
        moneyChange = _money;
        _quiz = _quizType;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(eventStatus))]
public class eventStatus_Editor : Editor
{
    bool showBtn = false;

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector(); // for other non-HideInInspector fields

		eventStatus myScript = (eventStatus)target;
        EditorUtility.SetDirty(myScript);


		if (myScript.effectEvent == eventStatus.effect.Modifier)
		{
            myScript.dropStatusModifier = (eventStatus.dropStatus)EditorGUILayout.EnumPopup("Status para Modificar: ", myScript.dropStatusModifier);
            EditorGUILayout.HelpBox ("Aplicar algum valor quando um modificador de status for alterado {Metade: 0.5(Recurso caindo na metade do tempo), Dobro: 2(Recurso caindo no dobro do tempo))", MessageType.Info);
            myScript.modifier = EditorGUILayout.IntField("Modificador: ", myScript.modifier);
            if( myScript.dropStatusModifier == eventStatus.dropStatus.caffeine)
            {
                myScript.timeDuration = 15 * ((EditorGUILayout.IntSlider("Duração do Modificador: ",myScript.timeDuration, 15, 120)) / 15);
            }
            else
            {
                myScript.timeDuration = 15 * ((EditorGUILayout.IntSlider("Duração do Modificador: ",myScript.timeDuration, 15, 60)) / 15);

            }
            showBtn = EditorGUILayout.Toggle("Modifica dinheiro: ", showBtn);
            if (showBtn)
            {
                myScript.moneyChange = EditorGUILayout.FloatField("Dinheiro: ",myScript.moneyChange);
            }
        }

        if (myScript.effectEvent == eventStatus.effect.dropDirect)
		{
            myScript.dropDirectStatus = (eventStatus.dropStatus)EditorGUILayout.EnumPopup("Status para Diminuir: ", myScript.dropDirectStatus);
            myScript.modifier = EditorGUILayout.IntField("Valor para alterar: ", myScript.modifier);
            EditorGUILayout.HelpBox ("Aplicar algum valor quando o status for sofrer uma queda brusca", MessageType.Info);
            showBtn = EditorGUILayout.Toggle("Modifica dinheiro: ", showBtn);
            if (showBtn)
            {
                myScript.moneyChange = EditorGUILayout.FloatField("Dinheiro: ",myScript.moneyChange);
            }
        }

        if (myScript.effectEvent == eventStatus.effect.minigameCost)
		{
            myScript.changeMinigame = (eventStatus.minigame)EditorGUILayout.EnumPopup("Tipo de minigame: ", myScript.changeMinigame);
            myScript.minigameCost = EditorGUILayout.Slider("Porcentagem do custo atual: ", myScript.minigameCost,0,100);
            myScript.timeDuration = 15 * ((EditorGUILayout.IntSlider("Duração do Modificador: ",myScript.timeDuration, 15, 60)) / 15);
        }

        if (myScript.effectEvent == eventStatus.effect.Conditional)
		{
            myScript.hasCondition = EditorGUILayout.ObjectField("Evento condicional: ", myScript.hasCondition, typeof(eventConditional), true) as eventConditional;
        }

        if (myScript.effectEvent == eventStatus.effect.Money)
		{
            myScript.moneyChange = EditorGUILayout.FloatField("Dinheiro: ",myScript.moneyChange);
        }

        if (myScript.effectEvent == eventStatus.effect.changePoints)
		{
            myScript.modifier = EditorGUILayout.IntField("Modificador dos pontos: ", myScript.modifier);
            myScript.timeDuration = 15 * ((EditorGUILayout.IntSlider("Duração do Modificador: ",myScript.timeDuration, 15, 120)) / 15);
            EditorGUILayout.HelpBox ("Modificador vai ser aplicado como uma multiplicação", MessageType.Info);
        }

        if (myScript.effectEvent == eventStatus.effect.stageModifier)
		{
            myScript.timeReduceToEndNextSage = EditorGUILayout.FloatField("Redução da proxima etapa em %: ",myScript.timeReduceToEndNextSage);
        }
	}
}
#endif
