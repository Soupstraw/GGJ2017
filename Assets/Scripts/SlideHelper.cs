using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideHelper : MonoBehaviour {

	private Vector3 startPoint;
	private Vector3 endPoint;
	private Vector3 distance = new Vector3(0, -5, 0);
	private bool clicked = false;

	public float speed;
	public float max;

	void Start(){
		startPoint 	= gameObject.transform.position;
		endPoint	= startPoint + distance;
	}

	void OnMouseDown() {
        Debug.Log("test");
		clicked = true;
    }

	void Update(){
		if(clicked && transform.position.z > -max)
    		transform.position += distance * Time.deltaTime * 10;
	}
}
