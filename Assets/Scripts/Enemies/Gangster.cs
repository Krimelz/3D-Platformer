using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gangster : MonoBehaviour, IEnemy
{
    public EnemyData enemy;
    public int Health { get; set; }
    public float MovementSpeed { get; set; }
    public float LookRadius { get; set; }
    public float AttackDistance { get; set; }
    public float AttackDelay { get; set; }

    private GameObject player;
    private Rigidbody rbody;
    private float distanceToPlayer;
    private float attackTime;

    void Start()
    {
        Health = enemy.health;
        MovementSpeed = enemy.movementSpeed;
        LookRadius = enemy.lookRadius;
        AttackDistance = enemy.attackDistance;
        AttackDelay = enemy.attackDelay;

        player = GameObject.FindGameObjectWithTag("Player");
        rbody = GetComponent<Rigidbody>();
        attackTime = AttackDelay;
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
        if (distanceToPlayer <= AttackDistance)
        {
            // Play attack animation
            if (attackTime <= 0)
            {
                OnAttacked();
                attackTime = AttackDelay;
            }
            else
            {
                attackTime -= Time.deltaTime;
            }
        }
    }

    // TODO: Call when attack animation end
    public void OnAttacked()
    {
        player.GetComponent<PlayerControls>().TakeDamage(enemy.damage);
    }

    public void Move()
    {
        if (distanceToPlayer <= LookRadius && distanceToPlayer >= AttackDistance)
        {
            int dir = transform.position.x > player.transform.position.x ? -1 : 1;
            Vector3 movement = new Vector3(dir * MovementSpeed * Time.fixedDeltaTime, 0f, 0f);
            rbody.velocity = movement;
        }
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        
        if (Health <= 0)
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
        Gizmos.DrawWireSphere(transform.position, enemy.lookRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, enemy.attackDistance);
    }
}
