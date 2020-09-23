using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class lightType: ScriptableObject
{
	public string ligthName;
	public Color ligthColor;
	public float intensity = 1.00f;

	public int begin = 0,end = 0;

	public bool turnOnLights = false;

	public void init(string _name, Color _ligthColor, float _intensity, int _begin,int _end, bool _turnOnLights)
	{
		this.ligthName = _name;
		this.ligthColor = _ligthColor;
		this.intensity = _intensity;
		this.begin = _begin;
		this.end = _end;	
		this.turnOnLights = _turnOnLights;
	} 

}
