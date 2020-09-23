using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrassManager : MonoBehaviour {

	public int life;

	void Update()
	{
		if(life<=0)
		{
			GameObject.Find("Farm_Camp").GetComponent<ColliderBridgeFarm>().removeList(this.gameObject);
			Destroy(this.gameObject,0.5f);
		}
	} 

	public void dropLife()
	{
		life--;
	}

	void setSpriteFrame()
	{
		//this.gameObject.GetComponent<Image>().	Ajustar fazer o frame certo
	}
}
