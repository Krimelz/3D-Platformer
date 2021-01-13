using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gangster : MonoBehaviour, IEnemy
{
    public EnemyData enemyData;
    public int Health { get; set; }
    public float MovementSpeed { get; set; }
    public float LookRadius { get; set; }
    public float AttackDistance { get; set; }

    private Transform playerTarget;

    void Start()
    {
        Health = enemyData.health;
        MovementSpeed = enemyData.movementSpeed;
        LookRadius = enemyData.lookRadius;
        AttackDistance = enemyData.attackDistance;

        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Move();
    }

    public void Attack()
    {

    }

    public void Move()
    {
        if (Vector3.Distance(transform.position, playerTarget.position) <= LookRadius)
        {
            // Move to player
        }
    }

    public void TakeDamage(int takedDamage)
    {
        Health -= takedDamage;
        
        if (Health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
