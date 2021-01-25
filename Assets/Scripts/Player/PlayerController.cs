using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour, IEnemy
{
    #region UI Actions

    public static Action<int> healthUpdate;
    public static Action<int> manaUpdate;   
    public static Action death;

    #endregion

    #region Player stats

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
    public Transform groundCheckPoint;
    public float groundCheckRayLength;
    public float groundCheckSphereRadius;

    #endregion

    private Rigidbody rbody;
    private Animator anim;
    private int health;
    private int mana;
    private float movementDirectionX;
    private float shootingTime;
    private bool isDead = false;

    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();

        PlayerAnimations.shooting += SpawnBullet;

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
        if (Input.GetKeyDown(KeyCode.Space) && GroundCheck())
        {
            rbody.AddForce(Vector3.up * jumpForce);
        }
    }

    private bool GroundCheck()
    {
        Ray ray = new Ray(groundCheckPoint.position, -transform.up);
        
        return Physics.SphereCast(ray, groundCheckSphereRadius, groundCheckRayLength, groundLayer);
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
            //CastBullet(bulletCost);
            //SpawnBullet();
        }

        shootingTime -= Time.deltaTime;
    }

    private void SpawnBullet()
    {
        CastBullet(bulletCost);
        Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
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
        death?.Invoke();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(groundCheckPoint.position, groundCheckPoint.position - transform.up * groundCheckRayLength);
        Gizmos.DrawWireSphere(groundCheckPoint.position - transform.up * groundCheckRayLength, groundCheckSphereRadius);
    }

    private void OnDestroy()
    {
        PlayerAnimations.shooting -= SpawnBullet;
    }
}
