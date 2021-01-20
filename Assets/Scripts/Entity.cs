using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Entity : MonoBehaviour
{
    public enum EntityType
    {
        Enemy,
        Key,
        Totem,
        Boss
    }

    public static Action<Entity> updateLevelManager;

    public EntityType type;

    private void OnDestroy()
    {
        updateLevelManager?.Invoke(this);
    }
}
