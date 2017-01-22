using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OutroHelper : MonoBehaviour {
	private Vector3 distance = new Vector3(0, -5, 0);
	private bool clicked = false;

	public float speed;
	public float max;
	public bool lastSlide;
	public Text subtitle;
	public int index;
	
	private string[] story;

	void Start(){

		story = new string[] {
			"",
			"I defeated the monsters but we decided to share our knowledge with them.",
			"We became friends and they decided to share all the knowledge they stole."
		};

		subtitle.text = story[1];
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
			SceneManager.LoadScene("menu");
		}
	}
}
