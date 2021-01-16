using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyController : MonoBehaviour
{
    public static Action<int> attackPlayer;

    public int health;
    public float movementSpeed;
    public float lookDistance;
    public float attackDistance;
    public int attackDamage;
    public float attackRate;
    public LayerMask enemyLayer;

    private Rigidbody rbody;
    private float attackTime;

    private void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }

    public void Attack()
    {
        // TODO: Play attack animation

        if (attackTime <= 0)
        {
            attackPlayer?.Invoke(attackDamage);
            attackTime = attackRate;
        }

        attackTime -= Time.deltaTime;
    }

    public void Move(Vector3 direction)
    {
        // TODO: Play move animation
        if (rbody != null)
        {
            Vector3 movement = new Vector3(direction.x * movementSpeed * Time.fixedDeltaTime, rbody.velocity.y, 0f);

            rbody.velocity = movement;
        }
    }

    public void Rotate()
    {
        transform.rotation = Quaternion.Euler(0f, (-1f * transform.rotation.eulerAngles.y) - 180f, 0f);
    }

    public void TakeDamage(int damage)
    {
        // TODO: Play hit animation

        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        // TODO: Play death animation

        Destroy(gameObject);
    }
}
