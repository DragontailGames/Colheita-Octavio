using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Event/Event Conditional")]
public class eventConditional : ScriptableObject
{
	public enum contitionTypes{water,cleaning,nutrients,all}
	public contitionTypes conditionThis;
	
	public eventStatus goodCondition;
	public eventStatus badCondition;
}
