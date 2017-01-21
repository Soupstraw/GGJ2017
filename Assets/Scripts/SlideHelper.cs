using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SlideHelper : MonoBehaviour {
	private Vector3 startPoint;
	private Vector3 endPoint;
	private Vector3 distance = new Vector3(0, -5, 0);
	private bool clicked = false;

	public float speed;
	public float max;
	public bool lastSlide;
	public Text subtitle;
	public int index;

	private string[] story;

	void Start(){
		startPoint 	= gameObject.transform.position;
		endPoint	= startPoint + distance;

		story = new string[] {
			"",
			"Our planet was living in peace and harmony for a long time.",
			"Mind Stones and the Mind Crystal kept us wise and nobody did stupid stuff.",
			"But one day an meteor appeared in the sky and was getting closer every secong.",
			"We learned that they were an alien race invading every planet in the universe to steal peoples knowledge.",
			"It hit our world and started to spread weird waves.",
			"Lots of monster started to come out of the meteor.",
			"And after the hit everybody lost their mind and started to act weird."
		};

		subtitle.text = story[index];
	}

	void OnMouseDown() {
		if(!lastSlide)
			subtitle.text = story[index + 1];
		else
			subtitle.text = "";

		clicked = true;
    }

	void Update(){
		if(clicked && transform.position.y > -max)
    		transform.position += distance * Time.deltaTime * speed;

		if(lastSlide && transform.position.y <= -max){
			Debug.Log("next scene");
			SceneManager.LoadScene("scene0");
		}
	}
}
