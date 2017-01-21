using UnityEngine;

public class OutroHelper : MonoBehaviour {
	private Vector3 startPoint;
	private Vector3 endPoint;
	private Vector3 distance = new Vector3(0, -5, 0);
	private bool clicked = false;

	public float speed;
	public float max;
	public bool lastSlide;

	void Start(){
		startPoint 	= gameObject.transform.position;
		endPoint	= startPoint + distance;
	}

	void OnMouseDown() {
        Debug.Log("test");
		clicked = true;
    }

	void Update(){
		if(clicked && transform.position.y > -max)
    		transform.position += distance * Time.deltaTime * speed;

		if(lastSlide && transform.position.y <= -max){
			Debug.Log("next scene");
			Application.LoadLevel("menu");
		}
	}
}
