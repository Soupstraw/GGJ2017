using UnityEngine;

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

    void Awake()
    {
        cc_This = GetComponent<CharacterController>();
        t_This  = transform;
    }

    void Update()
    {
        if (cc_This.isGrounded)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical   = Input.GetAxis("Vertical");
            v3_MoveDir = new Vector3(horizontal, 0, vertical);
			v3_MoveDir.Normalize ();
			v3_MoveDir = Vector3.Scale (v3_MoveDir, new Vector3(v2_MoveSpeeds.x, 0, v2_MoveSpeeds.y));

            t_CameraReference = t_Camera;
            t_CameraReference.eulerAngles = new Vector3(t_Camera.eulerAngles.x, t_Camera.eulerAngles.y, t_This.eulerAngles.z);

            v3_MoveDir = t_CameraReference.TransformDirection(v3_MoveDir);

            if (Input.GetKeyDown(KeyCode.Space))
                v3_MoveDir.y = f_JumpSpeed;
        }

        v3_MoveDir.y -= Time.deltaTime * f_Gravity;
        cc_This.Move(v3_MoveDir * Time.deltaTime);

		if(B_IsMoving())
		{
            Debug.Log("s");
            Quaternion targetRot = Quaternion.Euler(new Vector3(t_This.eulerAngles.x, t_Camera.eulerAngles.y, t_This.eulerAngles.z));
            t_This.rotation = Quaternion.Slerp(t_This.rotation, targetRot, Time.deltaTime * f_RotateSpeed);
        }
    }

	bool B_IsMoving()
	{
		return cc_This.velocity.magnitude > 0.1f && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && cc_This.isGrounded;
    }
}