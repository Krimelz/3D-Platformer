using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float radius = 2f;
    public int damage = 50;
    public float duration = 1f;
    public LayerMask enemyLayer;

    private void Start()
    {
        Explode();
        Destroy(gameObject, duration);
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, enemyLayer);

        foreach (Collider collider in colliders)
        {
            if (!collider.isTrigger)
            {
                collider.GetComponent<IEnemy>().TakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
