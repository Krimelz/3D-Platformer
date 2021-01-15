using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public static Action<int> healthUpdate;

    public float movementSpeed = 500f;
    public float jumpForce = 200f;
    public int health = 100;
    public float shootingDelay = 1f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public LayerMask groundLayer;
    [Space]
    public AudioClip moveClip;
    public AudioClip jumpClip;
    public AudioClip landingClip;
    public AudioClip shootClip;

    private Rigidbody rbody;
    private Animator anim;
    private AudioSource playerSounds;
    private float movementDirectionX;
    private float shootingTime;
    private bool isGrounded = false;
    private bool isDead = false;

    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        playerSounds = GetComponent<AudioSource>();
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

        transform.localRotation = Quaternion.Euler(0f, 90f * movementDirectionX, 0);
    }

    private void Move()
    {
        movementDirectionX = Input.GetAxisRaw("Horizontal");

        if (movementDirectionX != 0)
        {
            anim.SetBool("Move", true);
            rbody.velocity = new Vector3(movementDirectionX * movementSpeed * Time.fixedDeltaTime, rbody.velocity.y);

            Flip();
        }
        else
        {
            anim.SetBool("Move", false);
            rbody.velocity = new Vector3(0f, rbody.velocity.y);
        }
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
                shootingTime = shootingDelay;
                anim.SetTrigger("Attack");
            }
        }

        shootingTime -= Time.deltaTime;
    }

    public void SpawnBullet()
    {
        Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        PlayShootSound();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        anim.SetTrigger("Hit");

        if (health <= 0)
        {
            health = 0;
            Die();
        }

        healthUpdate?.Invoke(health);
    }

    private void Die()
    {
        anim.SetBool("Die", true);
        isDead = true;
    }

    public void PlayMoveSound()
    {
        playerSounds.clip = moveClip;
        playerSounds.Play();
    }

    public void PlayJumpSound()
    {
        playerSounds.clip = jumpClip;
        playerSounds.Play();
    }

    public void PlayShootSound()
    {
        playerSounds.PlayOneShot(shootClip);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer) == groundLayer)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
            anim.SetBool("Jump", false);
        }
    }
}
