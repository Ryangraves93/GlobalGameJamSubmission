using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementWithFrameOfReference : MonoBehaviour
{
    public float Speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float RightInput = Input.GetAxisRaw("Horizontal");
        float ForwardInput = Input.GetAxisRaw("Vertical");


        Debug.Log("F" + ForwardInput);
        Debug.Log("R" + RightInput);

        Quaternion MovementQuat = GetMovementFrame(Camera.main.transform);

        Vector3 MovementInput = (MovementQuat * Vector3.forward * ForwardInput) + (MovementQuat * Vector3.right * RightInput);

        Vector3 MovementDelta = MovementInput * Speed * Time.deltaTime;

        transform.Translate(MovementDelta);

        Vector3 start = transform.position;
        Vector3 end = start + MovementInput * (float)200.0;

        Debug.DrawLine(start, end, Color.white, 0.1f, true);
    }

    Quaternion GetMovementFrame(Transform Frame)
    {
        Vector3 FrameForward = Vector3.ProjectOnPlane(Frame.forward, Vector3.up).normalized;

        Quaternion MovementQuat = Quaternion.LookRotation(FrameForward);

        return MovementQuat;
    }

};
