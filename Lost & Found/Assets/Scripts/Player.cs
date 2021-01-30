using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Player : MonoBehaviour
{
    public bool attacking = false;
    public bool canAttack;


    public float Speed = 10f;
    public float Force = 100f;

    public bool UseForce = true;

    Rigidbody m_rb;
    CapsuleCollider m_capsuleCollider;
    MeshFilter m_meshFilter;
    MeshRenderer m_meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("started");
        IntializeComponents();

    }

    void IntializeComponents()
    {
        m_rb = GetComponent<Rigidbody>();
        m_capsuleCollider = GetComponent<CapsuleCollider>();
        m_meshFilter = GetComponent<MeshFilter>();
        m_meshRenderer = GetComponent<MeshRenderer>();

        m_rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        canAttack = !attacking;

        if (UseForce)
        {
            DoMovement_Force();
        }
        else
        {
            float RightInput = Input.GetAxisRaw("Horizontal");
            float ForwardInput = Input.GetAxisRaw("Vertical");

            Quaternion MovementQuat = GetMovementFrame(Camera.main.transform);

            Vector3 MovementInput = (MovementQuat * Vector3.forward * ForwardInput) + (MovementQuat * Vector3.right * RightInput);

            Vector3 MovementDelta = MovementInput * Speed * Time.deltaTime;

            transform.Translate(MovementDelta);
        }
        


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


    void DoMovement_Force()
    {
        float RightInput = Input.GetAxisRaw("Horizontal");
        float ForwardInput = Input.GetAxisRaw("Vertical");

        Quaternion MovementQuat = GetMovementFrame(Camera.main.transform);

        Vector3 MovementDirection = (MovementQuat * Vector3.forward * ForwardInput) + (MovementQuat * Vector3.right * RightInput);

        MovementDirection.Normalize();

        float InputStrength = new Vector2(RightInput, ForwardInput).magnitude;

        m_rb.AddForce(MovementDirection * Force * InputStrength * Time.deltaTime);

    }
}
