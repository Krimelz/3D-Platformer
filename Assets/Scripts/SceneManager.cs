using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    private List<GameObject> enemies = new List<GameObject>();

    private void Start()
    {
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
    }


}
