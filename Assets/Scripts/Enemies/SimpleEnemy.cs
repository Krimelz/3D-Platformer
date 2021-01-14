using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour, IEnemy
{
    public int health;
    public float movementSpeed;
    public float lookRadius;
    public int attackDamage;
    public float attackDistance;
    public float attackDelay;

    private GameObject player;
    private Rigidbody rbody;
    private float distanceToPlayer;
    private float attackTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rbody = GetComponent<Rigidbody>();
        attackTime = attackDelay;
    }

    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        Attack();
    }

    void FixedUpdate()
    {
        Move();
    }

    public void Attack()
    {
        if (distanceToPlayer <= attackDistance)
        {
            // Play attack animation
            if (attackTime <= 0)
            {
                OnAttacked();
                attackTime = attackDelay;
            }

            attackTime -= Time.deltaTime;
        }
    }

    // TODO: Call when attack animation end
    public void OnAttacked()
    {
        player.GetComponent<PlayerController>().TakeDamage(attackDamage);
    }

    public void Move()
    {
        if (distanceToPlayer <= lookRadius && distanceToPlayer >= attackDistance)
        {
            int dir = transform.position.x > player.transform.position.x ? -1 : 1;
            Vector3 movement = new Vector3(dir * movementSpeed * Time.fixedDeltaTime, 0f, 0f);
            rbody.velocity = movement;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}
