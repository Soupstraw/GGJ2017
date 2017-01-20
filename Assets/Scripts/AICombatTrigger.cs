using UnityEngine;
using System.Collections;

public class AICombatTrigger : MonoBehaviour {

	public GameObject combatCanvas;

    public Transform t_SelfCamera;
    public Transform t_SelfCameraLocation;
    public float f_MoveCamSpeed;
    public Transform t_PlayerLocation;
    private Transform t_Player;

    void OnTriggerEnter(Collider c)
	{
		if(c.CompareTag("Player"))
		{
            c.transform.GetComponent<MovementController>().enabled = false;
            Camera.main.transform.GetComponent<CameraController>().enabled = false;
            t_SelfCamera.position = Camera.main.transform.position;
            Camera.main.enabled = false;
            t_SelfCamera.gameObject.SetActive(true);
            t_Player = c.transform;
            StartCoroutine(CO_MoveCam());
        }
	}

	IEnumerator CO_MoveCam()
	{
        float i = 0.0f;
        Vector3 currentPos = t_SelfCamera.position;
        Quaternion currentRot = t_SelfCamera.rotation;

        Vector3 targetPos = t_SelfCameraLocation.position;
        Quaternion targetRot = t_SelfCameraLocation.rotation;

        Vector3 currentPlayerPos = t_Player.position;
        Quaternion currentPlayerRot = t_Player.rotation;


        while(i < 1.0f)
		{
            i += Time.deltaTime * f_MoveCamSpeed;
            t_SelfCamera.position = Vector3.Lerp(currentPos, targetPos, i);
            t_SelfCamera.rotation = Quaternion.Slerp(currentRot, targetRot, i);
            t_Player.position = Vector3.Lerp(currentPlayerPos, t_PlayerLocation.position, i);
            t_Player.rotation = Quaternion.Slerp(currentPlayerRot, t_PlayerLocation.rotation, i);
            yield return null;
        }
			
		// INITIATE FAYT SEQUENS
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		combatCanvas.SetActive (true);
    }
}
