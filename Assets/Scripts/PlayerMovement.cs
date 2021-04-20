using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rBody;
    float movement;
    float rotation;
    [SerializeField]
    float movementMult = 1f;
    [SerializeField]
    float rotationMult = 1f;
    [SerializeField]
    float jumpForce = 100f;
    Animator animator;

    public bool Ground { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }



    void Update()
    {
        movement = Input.GetAxis("Vertical")* movementMult;
        rotation = Input.GetAxis("Horizontal") * rotationMult;
        Debug.DrawLine(transform.position, transform.position + Vector3.down);

        if (Ground && Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (Ground && Input.GetButtonDown("Fire1"))
        {
            Punch();
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int layerMask = 1 << 8;
        Ground = (Physics.Raycast(transform.position + Vector3.up , Vector3.down, 1.1f, ~layerMask));
        
        animator.SetBool("Ground", Ground);

        if (Ground)
        {
            if (movement != 0f)
            {
                rBody.MovePosition(transform.position + transform.forward * movement * Time.fixedDeltaTime);
                animator.SetBool("Run", true);
            }
            else
            {
                animator.SetBool("Run", false);
            }      
        }

        if (rotation != 0f)
        {
            rBody.MoveRotation(transform.rotation * Quaternion.Euler(0f, rotation * Time.fixedDeltaTime, 0f));
            animator.SetFloat("Rotate", rotation);
        }
        else
        {
            animator.SetFloat("Rotate", 0f);
        }
    }

    void Jump()
    {
        rBody.AddForce(new Vector3(0, jumpForce, 0) + transform.forward * movement, ForceMode.VelocityChange);
        
    }
    void Punch()
    {
        animator.SetTrigger("Punch");
    }


    void MeleeAttackEnd()
    {

    }
    void MeleeAttackStart()
    {

    }
}

