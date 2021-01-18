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
    public enum Direction
    {
        Right,
        Up
    }

    public new Tag tag;
    public ForceMode forceMode;
    public Direction direction;
    public int damage;
    public float power;
    public float lifeTime = 2f;
    public LayerMask bulletLayer;

    private Rigidbody rbody;

    void Start()
    {
        rbody = GetComponent<Rigidbody>();

        if (direction == Direction.Right)
        {
            rbody.AddForce(transform.right * power, forceMode);
        }
        else
        {
            rbody.AddForce(transform.up * power, forceMode);
        }

        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(tag.ToString()))
        {
            collision.collider.GetComponent<IEnemy>().TakeDamage(damage);
        }

        if ((1 << collision.collider.gameObject.layer) != bulletLayer)
        {
            Destroy(gameObject);
        }
    }
}
