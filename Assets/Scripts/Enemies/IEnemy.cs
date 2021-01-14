using UnityEngine;
public interface IEnemy
{
    public int Health { get; set; }
    public float MovementSpeed { get; set; }
    public float LookRadius { get; set; }
    public float AttackDistance { get; set; }
    public float AttackDelay { get; set; }

    public void Move();
    public void Attack();
    public void TakeDamage(int takedDamage);
    public void Die();
}
