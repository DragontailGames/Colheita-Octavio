using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Octavio Types/Seed")]
public class seedType : ScriptableObject 
{
	public string seedName;
	public float seedCost;

	[TextArea(5,12)]
	public string description;
	public stageGroupStatus[] GroupList;
}
