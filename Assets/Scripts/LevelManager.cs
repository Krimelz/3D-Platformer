using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string nextScene;
    public bool enemyChecker;
    //public bool keyChecker;
    //public bool totemChecker;
    //public bool bossChecker;

    private List<Enemy> enemies = new List<Enemy>();
    //private List<GameObject> keys = new List<GameObject>();
    //private List<GameObject> totems = new List<GameObject>();
    //private List<GameObject> bosses = new List<GameObject>();

    private void Start()
    {
        if (enemyChecker)
        {
            enemies.AddRange(FindObjectsOfType<Enemy>());
        }

        Enemy.checkEnemy += CheckEnemies;
    }

    private void CheckEnemies(Enemy enemy)
    {
        enemies.Remove(enemy);

        if (enemies.Count == 0)
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
