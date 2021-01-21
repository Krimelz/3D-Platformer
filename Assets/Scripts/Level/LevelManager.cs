using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    public string nextScene;
    public bool enemyChecker;
    public bool keyChecker;
    public bool totemChecker;
    public bool bossChecker;

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

        if (keyChecker)
        {
            entities.AddRange(FindObjectsOfType<Entity>().Where(o => o.type == Entity.EntityType.Totem));
        }

        if (bossChecker)
        {
            entities.AddRange(FindObjectsOfType<Entity>().Where(o => o.type == Entity.EntityType.Boss));
        }

        Entity.updateLevelManager += UpdateEntities;
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

        if (totemChecker && entity.type == Entity.EntityType.Totem)
        {
            entities.Remove(entity);
        }

        if (bossChecker && entity.type == Entity.EntityType.Boss)
        {
            entities.Remove(entity);
        }

        if (entities.Count == 0) // <-----
        {
            Entity.updateLevelManager -= UpdateEntities;
            SceneManager.LoadScene(nextScene);
        }
    }
}
