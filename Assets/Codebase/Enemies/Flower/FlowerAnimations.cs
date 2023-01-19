using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FlowerAnimations : MonoBehaviour
{
    public static Action shooting;

    public void Shoot()
    {
        shooting?.Invoke();
    }
}
