using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlate : MonoBehaviour
{
    public LayerMask enemyLayer;

    private void OnCollisionEnter(Collision collision)
    {
        if ((1 << collision.gameObject.layer & enemyLayer) != 0)
        {
            collision.gameObject.GetComponent<IEnemy>().TakeDamage(100000);
        }
    }
}
