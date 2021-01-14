using UnityEngine;
public interface IEnemy
{
    public void Move();
    public void Attack();
    public void TakeDamage(int damage);
    public void Die();
}
