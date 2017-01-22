using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonsHelper : MonoBehaviour {
	private Renderer rend;

	void Start() {
        Shader rend = GetComponent<Shader>();
    }

	void OnMouseDown() {
		if(gameObject.name == "Play"){
			SceneManager.LoadScene ("Intro");
		}
		else if(gameObject.name == "Quit"){
			Application.Quit();
		}
    }

	void OnMouseOver(){
        gameObject.GetComponent<Renderer> ().material.color = Color.yellow;
	}

	void OnMouseExit(){
        gameObject.GetComponent<Renderer> ().material.color = Color.white;
	}
}
