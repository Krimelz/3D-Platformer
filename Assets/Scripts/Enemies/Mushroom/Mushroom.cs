using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Mushroom : MonoBehaviour, IEnemy
{
    public int health;
    public Transform look;
    public Transform spawnBulletPoint;
    public float lookDistance;
    public float shootingRate;
    public GameObject bulletPrefab;
    public LayerMask enemyLayer;

    private RaycastHit hit;
    private float shootingTime;
    private Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        MushroomAnimations.shooting += SpawnBullet;
    }

    void FixedUpdate()
    {
        if (Physics.SphereCast(look.position, 1f, look.right, out hit, lookDistance, enemyLayer))
        {
            if (hit.collider.CompareTag("Player"))
            {
                Shoot();
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

    public void SpawnBullet()
    {
        Instantiate(bulletPrefab, spawnBulletPoint.position, spawnBulletPoint.rotation);
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
        Gizmos.color = Color.green;
        Gizmos.DrawRay(look.position, look.right * lookDistance);
        Gizmos.DrawWireSphere(look.position + look.right * lookDistance, 1f);
    }

    private void OnDestroy()
    {
        MushroomAnimations.shooting -= SpawnBullet;
    }
}
