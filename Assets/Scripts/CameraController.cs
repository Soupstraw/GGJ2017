using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public Transform t_Target;

    public Vector2 v2_Sensitivity;
    public Vector2 v2_Smooth;
    public float f_DistanceToTarget;
    public Vector2 v2_MinAngles;
    public Vector2 v2_MaxAngles;

    private Vector2 v2_Inputs;
    private Vector2 v2_SmoothedInputs;
    private Vector2 v2_SmoothRef;
    private Transform t_This;
    private Quaternion q_This;

    public float f_DistanceInterpolationSpeed;
    public float f_MinDistance;
    public float f_MaxDistance;
    private float f_CurrentDistance;
    
    void Awake()
	{
        t_This = transform;
        q_This = t_This.rotation;

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
    }

    void Update()
    {
        f_CurrentDistance += Input.GetAxis("Mouse ScrollWheel") * f_DistanceInterpolationSpeed;
        f_CurrentDistance = Mathf.Clamp(f_CurrentDistance, f_MinDistance, f_MaxDistance);
        f_DistanceToTarget = f_CurrentDistance;

        v2_Inputs.x += Input.GetAxis("Mouse X") * v2_Sensitivity.x;
        v2_Inputs.y -= Input.GetAxis("Mouse Y") * v2_Sensitivity.y;

        v2_Inputs.x = Mathf.Clamp(v2_Inputs.x, v2_MinAngles.x, v2_MaxAngles.x);
        v2_Inputs.y = Mathf.Clamp(v2_Inputs.y, v2_MinAngles.y, v2_MaxAngles.y);

        v2_SmoothedInputs.x = Mathf.SmoothDamp(v2_SmoothedInputs.x, v2_Inputs.x, ref v2_SmoothRef.x, v2_Smooth.x);
        v2_SmoothedInputs.y = Mathf.SmoothDamp(v2_SmoothedInputs.y, v2_Inputs.y, ref v2_SmoothRef.y, v2_Smooth.y);

        Quaternion q_TargetRot = Quaternion.Euler(new Vector3(v2_SmoothedInputs.y, v2_SmoothedInputs.x, 0.0f));
        t_This.rotation = q_TargetRot;
        t_This.position = t_Target.position +  q_TargetRot * new Vector3(0.0f, 0.0f, -f_DistanceToTarget);
    }
}
