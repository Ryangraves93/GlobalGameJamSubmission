using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    public bool attacking = false;
    public bool canAttack;
    

    public float Speed = 10f;
    public float Force = 100f;

    public float RotationInterpSpeed = 10.0f;

    public float CameraInterpSpeed = 10.0f;

    public float attackRange = 5f;

    public bool UseForce = true;

    Vector3 MovementDirection;


    Rigidbody m_rb;
    CapsuleCollider m_capsuleCollider;
    MeshFilter m_meshFilter;
    MeshRenderer m_meshRenderer;
    Hitbox m_hitBox;
    Animator m_animator;

    // Start is called before the first frame update
    void Start()
    {
        IntializeComponents();

    }

    void IntializeComponents()
    {
        m_rb = GetComponent<Rigidbody>();
        m_capsuleCollider = GetComponent<CapsuleCollider>();
        m_meshFilter = GetComponent<MeshFilter>();
        m_meshRenderer = GetComponent<MeshRenderer>();
        m_animator = GetComponent<Animator>();

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

            m_animator.SetBool("walking", false);
        }
        else
        {
            m_animator.SetBool("walking", true);
        }
        Quaternion TargetRotation = Quaternion.LookRotation(MovementDirection, Vector3.up);
        Quaternion IntermediateRotation = Quaternion.Slerp(transform.rotation, TargetRotation, RotationInterpSpeed * Time.deltaTime);
        transform.rotation = IntermediateRotation;



        if (Input.GetButtonDown("Rummage") && canAttack)
        {
            //m_hitBox.Trigger();
            m_animator.SetBool("attacking", true);
            canAttack = false;
        }
        else
        {
            m_animator.SetBool("attacking", false);


        }
        //m_animator.SetBool("attacking", m_hitBox.bAttackActive);


        UpdateCamera();

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

    void DoMovement_Force()
    {

        float RightInput = Input.GetAxis("Horizontal");
        float ForwardInput = Input.GetAxis("Vertical");

        Quaternion MovementQuat = GetMovementFrame(Camera.main.transform);
        
        MovementDirection = ((MovementQuat * Vector3.forward * ForwardInput) + (MovementQuat * Vector3.right * RightInput)).normalized;


        //float InputStrength = new Vector2(RightInput, ForwardInput).magnitude;
        float InputStrength = Mathf.Clamp(Mathf.Sqrt(RightInput*RightInput + ForwardInput * ForwardInput), 0.0f, 1.0f);


        m_rb.AddForce(MovementDirection * Force * InputStrength * Time.deltaTime);

    }

    void UpdateCamera()
    {
        // We want to calculate the desired camera location without changing its rotation
        // Then we can slowly lerp to the position

        // We get the vector from camera location to player location.
        Vector3 CamToPlayer = transform.position - Camera.main.transform.position;

        // Then we can project this vector onto the camera's forward vector.
        Vector3 CamToPlayerProjection = Vector3.Project(CamToPlayer, Camera.main.transform.forward);

        // (This will give us the exact distance from the player that we need the camera.)

        // Then we just reverse this vector from the play position to find the desired camera position
        Vector3 DesiredCameraPosition = transform.position - CamToPlayerProjection;


        // Now interpolate
        Vector3 IntermediateCameraPosition = Vector3.Lerp(Camera.main.transform.position, DesiredCameraPosition, CameraInterpSpeed * Time.deltaTime);

        Camera.main.transform.position = IntermediateCameraPosition;
    }

    void OnHitboxActive()
    {
        m_hitBox.Trigger();

        m_animator.SetBool("attacking", false);
        canAttack = true;
    }
}

