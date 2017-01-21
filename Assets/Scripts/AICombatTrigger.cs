using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AICombatTrigger : MonoBehaviour {

	public GameObject combatCanvas;

    public Transform t_SelfCameraLocation;
    public float f_MoveCamSpeed;
    public Transform t_PlayerLocation;
    private Transform t_Player;

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
        }
	}

	public void FleeCombat(){
		t_Player.GetComponent<MovementController>().enabled = true;
		Camera.main.transform.GetComponent<CameraController>().enabled = true;

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		combatCanvas.SetActive (false);

		GetComponentInChildren<Animator> ().SetTrigger ("Win");
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
}
