using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuManager : MonoBehaviour {

	public bool oneMenuOpen = false;

	public void MenuOpen()
	{
		oneMenuOpen = true;
	}
		public void MenuClose()
	{
		oneMenuOpen = false;
	}

	public bool canOpenMenu()
	{
		return !oneMenuOpen;
	}
}
