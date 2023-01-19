using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomAnimations : MonoBehaviour
{
    public static Action shooting;

    public void Shoot()
    {
        shooting?.Invoke();
    }
}
