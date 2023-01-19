using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAnimations : MonoBehaviour
{
    public static Action shooting;

    public void Shoot()
    {
        shooting?.Invoke();
    }
}
