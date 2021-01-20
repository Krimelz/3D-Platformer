using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public static Action<Enemy> checkEnemy;

    private void OnDestroy()
    {
        checkEnemy?.Invoke(this);
    }
}
