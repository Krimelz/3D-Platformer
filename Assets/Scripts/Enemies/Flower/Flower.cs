using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour, IEnemy
{
    public int health;
    public Transform[] bulletSpawnPoints;
    public float lookRadius;
    public float shootingRate = 1f;
    public GameObject bulletPrefab;
    public LayerMask enemyLayer;

    private float shootingTime;

    private void Start()
    {
        shootingTime = shootingRate;
    }

    void FixedUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, lookRadius, enemyLayer);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                Shoot();
                break;
            }
        }
    }

    private void Shoot()
    {
        if (shootingTime <= 0)
        {
            for (int i = 0; i < bulletSpawnPoints.Length; i++)
            {
                SpawnBullet(i);
            }

            shootingTime = shootingRate;
        }

        shootingTime -= Time.fixedDeltaTime;
    }

    private void SpawnBullet(int spawnPointNumber)
    {
        Instantiate(bulletPrefab, bulletSpawnPoints[spawnPointNumber].position, bulletSpawnPoints[spawnPointNumber].rotation);
    }

    public void TakeDamage(int damage)
    {
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
