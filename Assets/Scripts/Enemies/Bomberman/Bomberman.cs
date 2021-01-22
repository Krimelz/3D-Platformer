using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bomberman : MonoBehaviour, IEnemy
{
    public enum Direction
    {
        Left = -1,
        Right = 1
    }

    public int health;
    public float movementSpeed;
    public Transform look;
    public float lookDistance;
    public Direction direction;
    public GameObject explosionPrefab;
    public LayerMask enemyLayer;
    public LayerMask groundLayer;

    private RaycastHit hit;
    private Rigidbody rbody;

    void Start()
    {
        rbody = GetComponent<Rigidbody>();

        if (direction == Direction.Left)
        {
            Flip();
        }
    }

    void FixedUpdate()  
    {
        if (Physics.SphereCast(look.position, 1f, look.right, out hit, lookDistance))
        {
            if ((1 << hit.collider.gameObject.layer & groundLayer) != 0)
            {
                Flip();
            }
        }

        Move();
    }

    private void Move()
    {
        Vector3 movement = new Vector3((int)direction * movementSpeed * Time.fixedDeltaTime, rbody.velocity.y, 0f);
        rbody.velocity = movement;
    }

    private void Flip()
    {
        direction = direction == Direction.Left ? Direction.Right : Direction.Left;
        transform.rotation = Quaternion.Euler(0f, (-1f * transform.rotation.eulerAngles.y) - 180f, 0f);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            health = 0;
            Explode();
        }
    }

    private void Explode()
    {
        GetComponent<CapsuleCollider>().enabled = false;
        Instantiate(explosionPrefab, look.position, look.rotation);
        Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Explode();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(look.position, look.right * lookDistance);
        Gizmos.DrawWireSphere(look.position + look.right * lookDistance, 1f);
    }
}
