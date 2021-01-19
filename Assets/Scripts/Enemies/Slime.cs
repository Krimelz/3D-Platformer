using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour, IEnemy
{
    public int health;
    [Range(1f, 20f)]
    public float movementSpeed = 1f;
    public Transform look;
    public float lookDistanceDown = 0.5f;
    public float lookDistanceRight = 0.5f;
    public int damage;
    public LayerMask enemyLayer;
    public LayerMask groundLayer;

    private RaycastHit hitRight;

    void FixedUpdate()
    {
        if (Physics.Raycast(look.position, transform.right, out hitRight, lookDistanceRight, groundLayer))
        {
            RotateUp();
        }

        if (!Physics.Raycast(look.position, -transform.up, lookDistanceDown, groundLayer))
        {
            RotateDown();
        }

        Move();
    }

    private void Move()
    {
        transform.Translate(new Vector3(movementSpeed * Time.fixedDeltaTime, 0f, 0f));
    }

    private void RotateDown()
    {
        transform.Rotate(new Vector3(0f, 0f, -90f));
        transform.Translate(new Vector3(0.1f, 0f, 0f));
    }

    private void RotateUp()
    {
        if (hitRight.distance <= lookDistanceRight / 2f)
        {
            transform.Rotate(new Vector3(0f, 0f, 90f));
            transform.Translate(new Vector3(0.1f, -hitRight.distance, 0f));
        }
    }

    public void TakeDamage(int damage)
    {
        // TODO: Play hit animation

        health -= damage;

        if (health <= 0)
        {
            health = 0;
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<IEnemy>().TakeDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(look.position, transform.right * lookDistanceRight);
        Gizmos.DrawRay(look.position, -transform.up * lookDistanceDown);
    }
}
