using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonsHelper : MonoBehaviour {

	void OnMouseDown() {
		if(gameObject.name == "Play"){
			Application.LoadLevel("Intro");
		}
		else if(gameObject.name == "Quit"){
			Application.Quit();
		}
    }
}
