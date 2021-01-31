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

    public float RotationInterpSpeed = 10.0f;

    public float attackRange = 5f;

    public bool UseForce = true;

    Vector3 MovementDirection;

    Rigidbody m_rb;
    CapsuleCollider m_capsuleCollider;
    MeshFilter m_meshFilter;
    MeshRenderer m_meshRenderer;
    Hitbox m_hitBox;

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

        m_hitBox = GetComponentInChildren<Hitbox>();
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

        // Do Rotation
        // MovementDirection = m_rb.velocity; // We could use the velocity, but when you slide against walls, i think it's better to face into them
        MovementDirection.y = 0;
        if (MovementDirection.sqrMagnitude < 0.1) 
        {
            MovementDirection = transform.forward;
        }
        Quaternion TargetRotation = Quaternion.LookRotation(MovementDirection, Vector3.up);
        Quaternion IntermediateRotation = Quaternion.Slerp(transform.rotation, TargetRotation, RotationInterpSpeed * Time.deltaTime);
        transform.rotation = IntermediateRotation;

        if (Input.GetKeyDown(KeyCode.Space))
        {
           // GameManager.Instance.SpawnEnemy();
        }

        if (Input.GetButtonDown("Rummage") && canAttack)
        {
            m_hitBox.Trigger();

            //Vector3 diff = GetClosestBreakable().transform.position - transform.position;
            //
            //if(diff.magnitude<attackRange)
            //{
            //    StartCoroutine(attack());
            //    GetClosestBreakable().GetComponent<Breakable>().breakMe();
            //}
            
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
        //hitBox.GetComponent<HitBox>().attacking = true;
        yield return new WaitForSeconds(0.5f);
        attacking = false;
        //hitBox.GetComponent<HitBox>().attacking = false;
    }

    public GameObject GetClosestBreakable()
    {
        GameObject[] breakables;
        breakables = GameObject.FindGameObjectsWithTag("breakable"); // gets all trees
        GameObject closest = null;
        float distance = Mathf.Infinity; // placeholders
        Vector3 position = transform.position;
        foreach (GameObject breakable in breakables)
        {
            Vector3 diff = breakable.transform.position - position;
            float curDistance = diff.sqrMagnitude; // gets distance as whole number
            if (curDistance < distance)
            {
                closest = breakable; // assigns closest breakable
                distance = curDistance;
            }
        }
        return closest;
    }

    //private void OnCollisionEnter(Collision col)
    //{
    //   Debug.Log("Player is trying to break!");

    //   if(col.collider.tag == "breakable")
    //   {
    //      if (attacking || col.collider.GetComponent<Breakable>().fragile)
    //       {
    //          col.collider.GetComponent<Breakable>().breakMe();
    //      }
    //   }

    // }


    void DoMovement_Force()
    {
        float RightInput = Input.GetAxisRaw("Horizontal");
        float ForwardInput = Input.GetAxisRaw("Vertical");

        Quaternion MovementQuat = GetMovementFrame(Camera.main.transform);

        MovementDirection = (MovementQuat * Vector3.forward * ForwardInput) + (MovementQuat * Vector3.right * RightInput).normalized;

        float InputStrength = new Vector2(RightInput, ForwardInput).magnitude;

        m_rb.AddForce(MovementDirection * Force * InputStrength * Time.deltaTime);

    }
}

