using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CoffeAnimationManager : MonoBehaviour 
{

	private GameObject tradeParticle;

	private stageManager _stageManager;

	void Start () 
	{
		tradeParticle = transform.Find("Trade").GetComponent<Transform>().gameObject;
		_stageManager = GameObject.Find("StageManager").GetComponent<stageManager>();
	}

	void Update()
	{
		if(_stageManager.getActualStageStatus().stageName == "Maturação")
		{
			this.GetComponent<Animator>().SetFloat("MaturationEnd",Convert.ToSingle((DateTime.Parse(PlayerPrefs.GetString("stageEnd")) - _stageManager.getCurrentTime()).TotalMinutes));
		}
	}
	
	public void setAnimator(int c)
	{
		this.GetComponent<Animator>().SetInteger("crescimento",c);
	}

	public void playParticle()
	{
		tradeParticle.GetComponent<ParticleSystem>().Play();
		tradeParticle.transform.GetComponentInChildren<ParticleSystem>().Play();
	}
}
