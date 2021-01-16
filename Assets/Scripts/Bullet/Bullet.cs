using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public float power;
    public Rigidbody rbody;
    void Start()
    {
        rbody.AddForce(transform.right * power, ForceMode.VelocityChange);
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            collision.collider.GetComponent<EnemyController>().TakeDamage(damage);
        }

        Destroy(gameObject, 0.1f);
    }
}
