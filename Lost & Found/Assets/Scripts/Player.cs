using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
<<<<<<< HEAD
    public bool attacking = false;
    public bool canAttack;
    public GameObject hitBox;

    public float Speed = 10f;
    public float Force = 100f;
    public float attackRange = 5f;

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


        if (Input.GetKeyDown(KeyCode.Space))
        {
           // GameManager.Instance.SpawnEnemy();
        }

        if (Input.GetKeyDown(KeyCode.Space) && canAttack)
        {
            Vector3 diff = GetClosestBreakable().transform.position - transform.position;

            if(diff.magnitude<attackRange)
            {
                StartCoroutine(attack());
                GetClosestBreakable().GetComponent<Breakable>().breakMe();
            }
            
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
=======
    public Camera camera;
    float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //assuming we're only using the single camera:
        camera = Camera.main;
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        float facing = camera.transform.rotation.y;
        float DistanceFromNeutral = 0;
        float transformZ = 0;
        float transformX = 0;
        float finalZ = 0;
        float finalX = 0;

        if (facing > -90 && facing <= 90)
        { //facing forward
            if (facing >= 0)
            {
                DistanceFromNeutral = (90 - facing);
            }
            else
            {
                if (facing < 0)
                {
                    DistanceFromNeutral = (90 + facing);
                };
            };
>>>>>>> 079e394b255fa3d41af06b24d428b4529a436cbd

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


            transformX = (1 / 90) * (DistanceFromNeutral);
            transformZ = 90 - transformX;
        };


        finalX = (transformX * verticalAxis) + (transformZ * horizontalAxis);


        finalZ = (transformZ * verticalAxis) + (transformX * horizontalAxis);


        transform.Translate((new Vector3(finalX * 0.01f, 0f, finalZ * 0.01f)) * speed * Time.deltaTime);
    }


}
