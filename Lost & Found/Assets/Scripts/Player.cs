using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Camera camera;
    public bool attacking = false;
    public bool canAttack;
    float Speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("started");
    }

    // Update is called once per frame
    void Update()
    {
        canAttack = !attacking;

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

        if (Input.GetKeyDown(KeyCode.Space) && canAttack)
        {
            StartCoroutine(attack());
        }
    }

    Quaternion GetMovementFrame(Transform Frame)
    {
        Vector3 FrameForward = Vector3.ProjectOnPlane(Frame.forward, Vector3.up).normalized;

        Quaternion MovementQuat = Quaternion.LookRotation(FrameForward);

        return MovementQuat;
    }

        
    

    IEnumerator attack()
    {
        attacking = true;
        yield return new WaitForSeconds(1);
        attacking = false;
        
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.collider.tag == "breakable")
        {
            if (attacking || col.collider.GetComponent<Breakable>().fragile)
            {
                col.collider.GetComponent<Breakable>().breakMe();
            }
        }
        
    }
}
