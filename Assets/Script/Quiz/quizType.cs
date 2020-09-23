using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Octavio Types/quiz Type")]
public class quizType : ScriptableObject 
{
	public string _quizName;
	[TextArea]
	public string _quizQuest;
	public string _wrongAswer;
	public string _rigthAswer;
	[TextArea]
	public string _rigthAswerResult = "Parabens, você acertou por isso...";
	[TextArea]
	public string _wrongAswerResult = "Infelizmente a resposta esta errada entao...";

	public void init(string _name, string _quest, string _wrong, string _rigth, string _wrongResult, string _rigthResult)
	{
		_quizName = _name;
		_quizQuest = _quest;
		_wrongAswer = _wrong;
		_rigthAswer = _rigth;
		_wrongAswerResult = _wrongResult;
		_rigthAswerResult = _rigthResult;
	}
}
