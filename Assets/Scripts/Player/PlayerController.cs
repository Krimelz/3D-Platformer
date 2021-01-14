using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 500f;
    public float jumpForce = 200f;
    public int health = 100;
    public float shootingDelay = 1f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public Vector3 groundCheckSize;

    [SerializeField]
    private LayerMask groundLayer;
    private Rigidbody rbody;
    private Animator anim;
    private float dirX;
    private float shootingTime;
    private bool isGrounded = false;
    private bool isDead = false;

    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        shootingTime = shootingDelay;
    }

    void Update()
    {
        if (isDead)
        { 
            return;
        }

        Jump();
        Shoot();
    }

    private void FixedUpdate()
    {
        if (isDead)
        {
            return;
        }

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
        isGrounded = Physics.BoxCast(transform.position, groundCheckSize, -transform.up, Quaternion.identity, 4f, groundLayer);

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
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (Input.GetKey(KeyCode.W))
            {
                bulletSpawnPoint.localRotation = Quaternion.Euler(-45f, 0f, 0f);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                bulletSpawnPoint.localRotation = Quaternion.Euler(45f, 0f, 0f);
            }
            else
            {
                bulletSpawnPoint.localRotation = Quaternion.Euler(0f, 0f, 0f);
            }

            if (shootingTime <= 0)
            {
                Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                shootingTime = shootingDelay;
            }
        }

        shootingTime -= Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
    }
}