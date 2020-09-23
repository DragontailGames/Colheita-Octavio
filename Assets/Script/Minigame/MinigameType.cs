using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu (menuName = "Octavio Types/Minigame")]
public class MinigameType : ScriptableObject
{
	public enum miniGameStatus{Water, Cleaning, Nutrients};
	public miniGameStatus status;
	public string sceneName;
	public float caffeineStandartCost;

	public float caffeineCost;

	public float changeWithFriend;

	public Dictionary<string,float> changeCostStage = new Dictionary<string, float>();
}
