using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AICombatTrigger : MonoBehaviour {

	public GameObject combatCanvas;

    public Transform t_SelfCameraLocation;
    public float f_MoveCamSpeed;
    public Transform t_PlayerLocation;
	public Transform fleeLocation;
    private Transform t_Player;

	public float shakeDuration = 1f;

	void Start(){
		t_Player = GameObject.FindWithTag ("Player").transform;
	}

    void OnTriggerEnter(Collider c)
	{
		if(c.CompareTag("Player"))
		{
            c.transform.GetComponent<MovementController>().enabled = false;
            Camera.main.transform.GetComponent<CameraController>().enabled = false;
            t_Player = c.transform;
			FindObjectOfType<QuizScript> ().aiTrigger = this;
            StartCoroutine(CO_MoveCam());
			StartCoroutine (CO_MovePlayer(t_PlayerLocation));
        }
	}

	public void FleeCombat(){

		StartCoroutine (CO_Flee(fleeLocation));

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		combatCanvas.SetActive (false);

		GetComponentInChildren<Animator> ().SetTrigger ("Win");
	}

	public void EndCombat(){
		t_Player.GetComponent<MovementController>().enabled = true;
		Camera.main.transform.GetComponent<CameraController>().enabled = true;
		t_Player.GetComponentInChildren<MovementController> ().ResetAnimation ();

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		combatCanvas.SetActive (false);
	}

	IEnumerator CO_MoveCam()
	{
        float i = 0.0f;
		Vector3 currentPos = Camera.main.transform.position;
		Quaternion currentRot = Camera.main.transform.rotation;

        Vector3 targetPos = t_SelfCameraLocation.position;
        Quaternion targetRot = t_SelfCameraLocation.rotation;

        Vector3 currentPlayerPos = t_Player.position;
        Quaternion currentPlayerRot = t_Player.rotation;


        while(i < 1.0f)
		{
            i += Time.deltaTime * f_MoveCamSpeed;
			Camera.main.transform.position = Vector3.Lerp(currentPos, targetPos, i);
			Camera.main.transform.rotation = Quaternion.Slerp(currentRot, targetRot, i);
            t_Player.position = Vector3.Lerp(currentPlayerPos, t_PlayerLocation.position, i);
            t_Player.rotation = Quaternion.Slerp(currentPlayerRot, t_PlayerLocation.rotation, i);
            yield return null;
        }
			
		// INITIATE FAYT SEQUENS
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		combatCanvas.SetActive (true);

		t_Player.GetComponentInChildren<MovementController> ().ResetAnimation ();

		FindObjectOfType<QuizScript> ().CombatStart ();
    }

	IEnumerator CO_MovePlayer(Transform target){
		float i = 0.0f;

		Vector3 currentPlayerPos = t_Player.position;
		Quaternion currentPlayerRot = t_Player.rotation;

		t_Player.GetComponentInChildren<Animator> ().Play ("Flee");

		while(i < 1.0f)
		{
			i += Time.deltaTime * f_MoveCamSpeed;
			t_Player.position = Vector3.Lerp(currentPlayerPos, target.position, i);
			t_Player.rotation = Quaternion.Slerp(currentPlayerRot, target.rotation, i);
			yield return null;
		}

		t_Player.GetComponentInChildren<MovementController> ().ResetAnimation ();
	}

	IEnumerator CO_Flee(Transform target){
		float i = 0.0f;

		Vector3 currentPlayerPos = t_Player.position;
		Quaternion currentPlayerRot = t_Player.rotation;

		t_Player.GetComponentInChildren<Animator> ().Play ("Flee");

		while(i < 1.0f)
		{
			i += Time.deltaTime * f_MoveCamSpeed;
			t_Player.position = Vector3.Lerp(currentPlayerPos, target.position, i);
			t_Player.rotation = Quaternion.Slerp(currentPlayerRot, target.rotation, i);
			yield return null;
		}

		t_Player.GetComponent<MovementController>().enabled = true;
		Camera.main.transform.GetComponent<CameraController>().enabled = true;
		t_Player.GetComponentInChildren<MovementController> ().ResetAnimation ();
	}

	public IEnumerator CO_ShakeAndGlow(){
		Vector3 cameraPosition = Camera.main.transform.position;

		float time = 0f;

		yield return new WaitForSeconds (1f);

		while (time < shakeDuration) {
			time += Time.deltaTime;
			Camera.main.transform.position = cameraPosition + new Vector3 (Mathf.Sin(time*10f*Mathf.PI)*0.5f*Mathf.Sin(time/shakeDuration*Mathf.PI), Mathf.Sin(time)*0.1f, 0);
			GetComponentInChildren<SkinnedMeshRenderer> ().material.color = Color.Lerp (Color.white, Color.red, Mathf.Sin(time*Mathf.PI*2));
			yield return null;
		}
	}
}
