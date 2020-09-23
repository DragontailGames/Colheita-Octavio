using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal; 

#endif
[CreateAssetMenu (menuName = "Octavio Types/Item Store")]
public class ItemToBuyStatus : ScriptableObject
{
	[HideInInspector]
	public string nameItem;

	[HideInInspector]
	public Sprite itemIcon;

	[HideInInspector]
	public float price;

	[HideInInspector]
	public string description;

	[HideInInspector]
	public int Id;

	[HideInInspector]
	public bool buyed = false;

	[HideInInspector]
	public float valueToChange;

	public enum effectStore {minigameChange, stageDuration, removeEvent}
	
	[HideInInspector]
	public effectStore effectItem;

	[HideInInspector]		
	public List<stageStatus> stageCondition = new List<stageStatus>();

	[HideInInspector]		
	public List<eventStatus> eventCondition = new List<eventStatus>();

	[HideInInspector]		
	public List<ItemToBuyStatus> itemSO = new List<ItemToBuyStatus>();
	
	[HideInInspector]
	public int stages = 0;
	
	[HideInInspector]
	public int auxEffect = 0;

	[HideInInspector]
	public eventStatus.minigame minigameToChange;
	
}

#if UNITY_EDITOR
[CustomEditor(typeof(ItemToBuyStatus))]
public class itemToBuyStatus_Editor : Editor
{
	bool fold = false;
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector(); 

		ItemToBuyStatus myScript = (ItemToBuyStatus)target;

		EditorUtility.SetDirty(myScript);

		myScript.nameItem = EditorGUILayout.TextField("Nome: ", myScript.nameItem);
		myScript.Id = EditorGUILayout.IntField("Id da Maquina: ", myScript.Id);
		myScript.itemIcon = EditorGUILayout.ObjectField("Icone: ", myScript.itemIcon, typeof(Sprite), true) as Sprite;
		myScript.price = EditorGUILayout.FloatField("Preço: ", myScript.price);
		myScript.description = EditorGUILayout.TextField("Descrição: ", myScript.description,GUILayout.Height(75));
		EditorStyles.textField.wordWrap = true;
		myScript.buyed = EditorGUILayout.Toggle("Comprado: ", myScript.buyed);
		myScript.effectItem = (ItemToBuyStatus.effectStore)EditorGUILayout.EnumPopup("Efeito: ", myScript.effectItem);
	
		if(myScript.effectItem == ItemToBuyStatus.effectStore.minigameChange)
		{
			myScript.minigameToChange = (eventStatus.minigame)EditorGUILayout.EnumPopup("Minigame: ",myScript.minigameToChange);
			myScript.valueToChange = EditorGUILayout.FloatField("Valor para mudar em %", myScript.valueToChange);

			myScript.stages = EditorGUILayout.DelayedIntField("Quantidade de Estagios: ",myScript.stages);

			while (myScript.stages < myScript.stageCondition.Count)
				myScript.stageCondition.RemoveAt( myScript.stageCondition.Count - 1 );
			while (myScript.stages > myScript.stageCondition.Count)
				myScript.stageCondition.Add(null);
			
			for(int i = 0; i < myScript.stageCondition.Count; i++)
			{
				myScript.stageCondition[i] = EditorGUILayout.ObjectField("Estagio " + (i+1) + ": ",myScript.stageCondition[i], typeof(stageStatus), true) as stageStatus;
			}

			EditorGUILayout.HelpBox("Manter em 0 para todos os estagios.", MessageType.Info);

			EditorGUILayout.Space();
			fold = EditorGUILayout.Foldout(fold, "Efeito Extra ("+myScript.itemSO.Count+")");
            if (fold)
            {
				myScript.auxEffect = EditorGUILayout.DelayedIntField("Efeito Extra: ",myScript.auxEffect);

				while (myScript.auxEffect < myScript.itemSO.Count)
					myScript.itemSO.RemoveAt( myScript.itemSO.Count - 1 );
				while (myScript.auxEffect > myScript.itemSO.Count)
					myScript.itemSO.Add(null);
				
				for(int i = 0; i < myScript.itemSO.Count; i++)
				{
					myScript.itemSO[i] = EditorGUILayout.ObjectField("Efeito Auxiliar " + (i+1) + ": ",myScript.itemSO[i], typeof(ItemToBuyStatus), true) as ItemToBuyStatus;
				}
			}
		}

		if(myScript.effectItem == ItemToBuyStatus.effectStore.stageDuration)
		{
			myScript.valueToChange = EditorGUILayout.FloatField("Duraçao para mudar em %", myScript.valueToChange);

			myScript.stages = EditorGUILayout.DelayedIntField("Quantidade de Estagios: ",myScript.stages);

			while (myScript.stages < myScript.stageCondition.Count)
				myScript.stageCondition.RemoveAt( myScript.stageCondition.Count - 1 );
			while (myScript.stages > myScript.stageCondition.Count)
				myScript.stageCondition.Add(null);
			
			for(int i = 0; i < myScript.stageCondition.Count; i++)
			{
				myScript.stageCondition[i] = EditorGUILayout.ObjectField("Estagio " + (i+1) + ": ",myScript.stageCondition[i], typeof(stageStatus), true) as stageStatus;
			}

			EditorGUILayout.Space();
			fold = EditorGUILayout.Foldout(fold, "Efeito Extra ("+myScript.itemSO.Count+")");
            if (fold)
            {
				myScript.auxEffect = EditorGUILayout.DelayedIntField("Efeito Extra: ",myScript.auxEffect);

				while (myScript.auxEffect < myScript.itemSO.Count)
					myScript.itemSO.RemoveAt( myScript.itemSO.Count - 1 );
				while (myScript.auxEffect > myScript.itemSO.Count)
					myScript.itemSO.Add(null);
				
				for(int i = 0; i < myScript.itemSO.Count; i++)
				{
					myScript.itemSO[i] = EditorGUILayout.ObjectField("Efeito Auxiliar " + (i+1) + ": ",myScript.itemSO[i], typeof(ItemToBuyStatus), true) as ItemToBuyStatus;
				}
			}
		}

		if(myScript.effectItem == ItemToBuyStatus.effectStore.removeEvent)
		{
			myScript.stages = EditorGUILayout.DelayedIntField("Quantidade de Eventos: ",myScript.stages);

			while (myScript.stages < myScript.eventCondition.Count)
				myScript.eventCondition.RemoveAt( myScript.eventCondition.Count - 1 );
			while (myScript.stages > myScript.eventCondition.Count)
				myScript.eventCondition.Add(null);
			
			for(int i = 0; i < myScript.eventCondition.Count; i++)
			{
				myScript.eventCondition[i] = EditorGUILayout.ObjectField("Evento " + (i+1) + ": ",myScript.eventCondition[i], typeof(eventStatus), true) as eventStatus;
			}
			
			EditorGUILayout.Space();
			fold = EditorGUILayout.Foldout(fold, "Efeito Extra ("+myScript.itemSO.Count+")");
            if (fold)
            {
				myScript.auxEffect = EditorGUILayout.DelayedIntField("Efeito Extra: ",myScript.auxEffect);

				while (myScript.auxEffect < myScript.itemSO.Count)
					myScript.itemSO.RemoveAt( myScript.itemSO.Count - 1 );
				while (myScript.auxEffect > myScript.itemSO.Count)
					myScript.itemSO.Add(null);
				
				for(int i = 0; i < myScript.itemSO.Count; i++)
				{
					myScript.itemSO[i] = EditorGUILayout.ObjectField("Efeito Auxiliar " + (i+1) + ": ",myScript.itemSO[i], typeof(ItemToBuyStatus), true) as ItemToBuyStatus;
				}
			}
		}
	}
}
#endif
