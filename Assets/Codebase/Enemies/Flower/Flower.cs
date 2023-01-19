using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Flower : MonoBehaviour, IEnemy
{
    public int health;
    public Transform[] bulletSpawnPoints;
    public float lookRadius;
    public float shootingRate = 1f;
    public GameObject bulletPrefab;
    public LayerMask enemyLayer;

    private float shootingTime;
    private Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        FlowerAnimations.shooting += SpawnBullet;
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
            anim.SetTrigger("Shooting");

            shootingTime = shootingRate;
        }

        shootingTime -= Time.fixedDeltaTime;
    }

    private void SpawnBullet()
    {
        for (int i = 0; i < bulletSpawnPoints.Length; i++)
        {
            Instantiate(bulletPrefab, bulletSpawnPoints[i].position, bulletSpawnPoints[i].rotation);
        }
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

    private void OnDestroy()
    {
        FlowerAnimations.shooting -= SpawnBullet;
    }
}
