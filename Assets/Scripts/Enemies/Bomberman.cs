using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomberman : MonoBehaviour
{
    [SerializeField]
    public EnemyController enemyController;
    public Transform look;
    public GameObject explosion;
    public LayerMask groundLayer;
    private RaycastHit hit;

    void Start()
    {
        enemyController = GetComponent<EnemyController>();
    }

    void FixedUpdate()  
    {
        if (Physics.SphereCast(look.position, 1f, look.right, out hit, enemyController.lookDistance))
        {
            if ((1 << hit.collider.gameObject.layer) == groundLayer)
            {
                enemyController.Rotate();
            }

            if (hit.collider.CompareTag("Player"))
            {
                Instantiate(explosion, look.position, look.rotation);
                EnemyController.attackPlayer?.Invoke(enemyController.attackDamage);
                enemyController.Die();
            }
        }

        enemyController.Move(transform.right);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(look.position, enemyController.lookDistance);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(look.position, enemyController.attackDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(look.position, look.right * enemyController.lookDistance);
        Gizmos.DrawWireSphere(look.position + look.right * enemyController.lookDistance, 1f);
    }
}
