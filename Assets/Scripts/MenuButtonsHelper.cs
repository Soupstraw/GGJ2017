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
			transform.localScale += new Vector3(0.1f, 0.1f, 0);
			SceneManager.LoadScene ("Intro");
		}
		else if(gameObject.name == "Quit"){
			Application.Quit();
		}
    }

	void OnMouseOver(){
        gameObject.GetComponent<Renderer> ().material.color = Color.red;
	}

	void OnMouseExit(){
        gameObject.GetComponent<Renderer> ().material.color = Color.blue;
	}
}
