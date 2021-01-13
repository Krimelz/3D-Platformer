using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public WeaponData weaponData;
    public Rigidbody rbody;
    void Start()
    {
        rbody.AddForce(transform.forward * weaponData.power, ForceMode.VelocityChange);
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            collision.collider.GetComponent<IEnemy>().TakeDamage(weaponData.damage);
        }

        Destroy(gameObject);
    }
}
