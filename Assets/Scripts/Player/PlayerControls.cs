using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float movementSpeed = 3f;
    public float jumpForce = 200f;

    [SerializeField]
    private LayerMask groundLayer;
    private Rigidbody rbody;
    private Animator anim;
    private Camera cam;
    private float dirX;
    private bool isGrounded = false;

    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        cam = Camera.main;
    }

    void Update()
    {
        Jump();

        Debug.Log(rbody.velocity.x);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Flip()
    {
        // TODO: Smooth rotate player to movement direction

        if (dirX < 0)
        {
            transform.rotation = Quaternion.Euler(0f, -90f, 0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        }
    }

    private void CheckGround()
    {
        // TODO: Cast box 
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 3.8f, groundLayer);

        if (isGrounded)
        {
            anim.SetBool("Jump", false);
        }
    }

    private void Move()
    {
        dirX = Input.GetAxisRaw("Horizontal");

        if (dirX != 0)
        {
            anim.SetBool("Movement", true);
            rbody.velocity = new Vector3(dirX * movementSpeed * Time.fixedDeltaTime, rbody.velocity.y);

            Flip(); 
        }
        else
        {
            anim.SetBool("Movement", false);
            rbody.velocity = new Vector3(0f, rbody.velocity.y);
        }

        CheckGround();
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            anim.SetBool("Jump", true);
            rbody.AddForce(Vector3.up * jumpForce);
            isGrounded = false;
        }
    }

    private void Shoot()
    {
        // TODO: Shoot to mouse direction
    }
}
