using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class EnemyData : ScriptableObject
{
    public int health;
    public float movementSpeed;
    public float lookRadius;
    public float attackDistance;
    public int damage;
}
