using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System;

public class LevelManager : MonoBehaviour
{
    public static Action<bool, bool> enableTasks;
    public static Action levelPassed;

    public bool enemyChecker;
    public bool keyChecker;

    [SerializeField]
    private List<Entity> entities = new List<Entity>();

    private void Start()
    {
        if (enemyChecker)
        {
            entities.AddRange(FindObjectsOfType<Entity>().Where(o => o.type == Entity.EntityType.Enemy));
        }

        if (keyChecker)
        {
            entities.AddRange(FindObjectsOfType<Entity>().Where(o => o.type == Entity.EntityType.Key));
        }

        Entity.updateLevelManager += UpdateEntities;

        enableTasks?.Invoke(enemyChecker, keyChecker);
    }

    private void UpdateEntities(Entity entity)
    {
        if (enemyChecker && entity.type == Entity.EntityType.Enemy)
        {
            entities.Remove(entity);
        }

        if (keyChecker && entity.type == Entity.EntityType.Key)
        {
            entities.Remove(entity);
        }

        if (entities.Count == 0)
        {
            levelPassed?.Invoke();
        }
    }

    private void OnDestroy()
    {
        Entity.updateLevelManager -= UpdateEntities;
    }
}
