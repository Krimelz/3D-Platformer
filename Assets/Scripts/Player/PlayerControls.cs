using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float movementSpeed = 3f;
    public float jumpForce = 200f;
    public GameObject shellPrefab;

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
        Shoot();
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
        if (Input.GetMouseButtonDown(0))
        {
            float enter;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            new Plane(-Vector3.forward, transform.position).Raycast(ray, out enter);
            Vector3 target = ray.GetPoint(enter);

            GameObject shell = Instantiate(shellPrefab, transform.position, Quaternion.identity);
            shell.transform.LookAt(target, shell.transform.right);
            shell.GetComponent<Rigidbody>().AddForce(shell.transform.forward * 75f, ForceMode.VelocityChange);
        }
    }
}
