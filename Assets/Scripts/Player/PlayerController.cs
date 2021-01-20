using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour, IEnemy
{
    public static Action<int> healthUpdate;
    public static Action<int> manaUpdate;

    [Header("Player stats")]
    public float movementSpeed = 500f;
    public float jumpForce = 400f;
    public int normalHealthAmount = 100;
    public int normalManaAmount = 100;
    public int manaRegenerateAmount = 1;
    public float manaRegenerateRate = 1f;
    public int bulletCost = 20;
    public float shootingRate = 1f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public LayerMask groundLayer;
    [Space]
    [Header("Player sounds")]
    public AudioClip moveClip;
    public AudioClip jumpClip;
    public AudioClip landingClip;
    public AudioClip shootClip;

    private Rigidbody rbody;
    private Animator anim;
    private AudioSource playerSounds;
    private int health;
    private int mana;
    private float movementDirectionX;
    private float shootingTime;
    private bool isGrounded = false;
    private bool isDead = false;

    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        playerSounds = GetComponent<AudioSource>();

        health = normalHealthAmount;
        mana = normalManaAmount;
        shootingTime = shootingRate;

        StartCoroutine(RegenerateMana());
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
        transform.localRotation = Quaternion.Euler(0f, 90f * movementDirectionX - 90f, 0);
    }

    private void Move()
    {
        // TODO: fix takeoff when run on slopes

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

    private void GroundCheck()
    {
        //
    }

    private void Shoot()
    {
        if (Input.GetKey(KeyCode.W))
        {
            bulletSpawnPoint.localRotation = Quaternion.Euler(0f, 0f, 45f);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            bulletSpawnPoint.localRotation = Quaternion.Euler(0f, 0f, -45f);
        }
        else
        {
            bulletSpawnPoint.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }

        if (Input.GetKeyDown(KeyCode.G) && shootingTime <= 0 && mana >= bulletCost)
        {
            shootingTime = shootingRate;
            anim.SetTrigger("Attack");
            CastBullet(bulletCost);
            SpawnBullet();
        }

        shootingTime -= Time.deltaTime;
    }

    public void SpawnBullet()
    {
        Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        PlayShootSound();
    }

    private IEnumerator RegenerateMana()
    {
        while (true)
        {
            if (mana < normalManaAmount) 
            {
                ReplenishMana(manaRegenerateAmount);
                yield return new WaitForSeconds(manaRegenerateRate);
            }
            else
            {
                yield return null;
            }
        }
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

    private void CastBullet(int cost)
    {
        mana -= cost;

        if (mana <= 0)
        {
            mana = 0;
        }

        manaUpdate?.Invoke(mana);
    }

    private void ReplenishMana(int manaAmount)
    {
        mana += manaAmount;

        if (mana > normalManaAmount)
        {
            mana = normalManaAmount;
        }

        manaUpdate?.Invoke(mana);
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
        if ((1 << other.gameObject.layer & groundLayer) != 0)
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
