using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    public float rotationSpeed = 10f;
    public int damage = 1;
    public LayerMask enemyLayer;

    private void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerStay(Collider other)
    {
        if ((1 << other.gameObject.layer) == enemyLayer)
        {
            other.GetComponent<IEnemy>().TakeDamage(damage);
        }
    }
}
