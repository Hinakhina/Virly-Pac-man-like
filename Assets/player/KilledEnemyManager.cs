using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KilledEnemyManager : MonoBehaviour
{
    [SerializeField] Player Player;
    public int enemyCount;

    void Start()
    {
        InitEnemyList();
    }

    private void InitEnemyList()
    {
        Enemy[] enemyList = GameObject.FindObjectsOfType<Enemy>();
        for (int i = 0; i < enemyList.Length; i++)
        {
            enemyCount += 1;
            
        }
    }

}
