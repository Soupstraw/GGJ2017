using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonsHelper : MonoBehaviour {

	void OnMouseDown() {
		if(gameObject.name == "Play"){
			SceneManager.LoadScene ("Intro");
		}
		else if(gameObject.name == "Quit"){
			Application.Quit();
		}
    }
}
