using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(EnemyController))]
public class SimpleEnemy : MonoBehaviour
{
    [SerializeField]
    public EnemyController enemyController;
    private RaycastHit hit;

    void Start()
    {
        enemyController = GetComponent<EnemyController>();
    }

    void FixedUpdate()
    {
        if (Physics.SphereCast(transform.position, 1f, transform.right, out hit, enemyController.lookDistance, enemyController.enemyLayer))
        {
            if (hit.collider.CompareTag("Player"))
            {
                if (hit.distance <= enemyController.attackDistance)
                {
                    enemyController.Attack();
                }
                else if (hit.distance <= enemyController.lookDistance)
                {
                    enemyController.Move(transform.right);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyController.lookDistance);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, enemyController.attackDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.right * enemyController.lookDistance);
        Gizmos.DrawWireSphere(transform.position + transform.right * enemyController.lookDistance, 1f);
    }
}
