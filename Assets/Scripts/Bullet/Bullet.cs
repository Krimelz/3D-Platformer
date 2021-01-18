using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum Tag
    {
        Enemy,
        Player
    }

    public new Tag tag;
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
        if (collision.collider.CompareTag(tag.ToString()))
        {
            Debug.Log(collision);
            collision.collider.GetComponent<IEnemy>().TakeDamage(damage);
        }

        Destroy(gameObject, 0.1f);
    }
}
