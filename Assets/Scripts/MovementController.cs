using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {
    public Vector2 v2_MoveSpeeds;
    public float f_JumpSpeed;
    public float f_Gravity;
    public Transform t_Camera;
    public float f_RotateSpeed;
    private Vector3 v3_MoveDir = Vector3.zero;
    private CharacterController cc_This;
    private Transform t_This;
    private Transform t_CameraReference;

	public Animator characterAnimator;

	public float jumpDelay;
	private bool doJump = false;

	public float sprintMultiplier;

    void Awake()
    {
        cc_This = GetComponent<CharacterController>();
        t_This  = transform;
    }

    void Update()
    {
		characterAnimator.SetBool ("IsGrounded", cc_This.isGrounded);

		if (cc_This.isGrounded) {
			float horizontal = Input.GetAxis ("Horizontal");
			float vertical = Input.GetAxis ("Vertical");
			v3_MoveDir = new Vector3 (horizontal, 0, vertical);
			v3_MoveDir.Normalize ();
			v3_MoveDir = Vector3.Scale (v3_MoveDir, new Vector3 (v2_MoveSpeeds.x, 0, v2_MoveSpeeds.y));

			t_CameraReference = t_Camera;
			t_CameraReference.eulerAngles = new Vector3 (0, t_Camera.eulerAngles.y, 0);

			characterAnimator.SetFloat ("XWalking", v3_MoveDir.x);
			characterAnimator.SetFloat ("YWalking", v3_MoveDir.z);

			v3_MoveDir = t_CameraReference.TransformDirection (v3_MoveDir);

			/*if (Input.GetKeyDown (KeyCode.Space)) {
				characterAnimator.SetTrigger ("Jump");
				StartCoroutine (Jump());
			}*/

			if (Input.GetButton ("Sprint")) {
				v3_MoveDir *= sprintMultiplier;
				characterAnimator.SetFloat ("SprintMultiplier", sprintMultiplier);
			} else {
				characterAnimator.SetFloat ("SprintMultiplier", 1);
			}

			if (doJump) {
				v3_MoveDir.y = f_JumpSpeed;
				doJump = false;
			}
		}

        v3_MoveDir.y -= Time.deltaTime * f_Gravity;
        cc_This.Move(v3_MoveDir * Time.deltaTime);

		if(B_IsMoving())
		{
            Quaternion targetRot = Quaternion.Euler(new Vector3(t_This.eulerAngles.x, t_Camera.eulerAngles.y, t_This.eulerAngles.z));
            t_This.rotation = Quaternion.Slerp(t_This.rotation, targetRot, Time.deltaTime * f_RotateSpeed);
        }
    }

	public IEnumerator Jump(){
		yield return new WaitForSeconds (jumpDelay);
		doJump = true;
	}

	public void ResetAnimation(){
		characterAnimator.SetFloat ("XWalking", 0);
		characterAnimator.SetFloat ("YWalking", 0);
	}

	bool B_IsMoving()
	{
		return cc_This.velocity.magnitude > 0.1f && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && cc_This.isGrounded;
    }
}