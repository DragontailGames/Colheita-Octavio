using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Stages/Stage Group")]
public class stageGroupStatus : ScriptableObject
{
	public stageStatus[] stages;
	public List<eventStatus> events;
}
