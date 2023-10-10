using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class enemyManeger : MonoBehaviour
{
    enemy e = null;
    EnemysStates[]AllenemyType;
    public void SpawnEnemy()
    {
        if(e != null)
        {
           Destroy(e); e = null;
        }
        GameObject SpawnEnemy = new GameObject();
        SpawnEnemy.AddComponent(typeof(enemy));
        SpawnEnemy.AddComponent(typeof(SpriteRenderer));
        SpawnEnemy.transform.position = new Vector3(0, .7f, 0);
        e = SpawnEnemy.GetComponent<enemy>();
       // Instantiate(SpawnEnemy, new Vector3(0,.7f,0),Quaternion.identity);
        setEnemyStats();
    }

    private void setEnemyStats()
    {
        int a = Random.Range(0, AllenemyType.Length);
        e.SetEnemyStats(AllenemyType[a].speed, AllenemyType[a].Weight, AllenemyType[a].strength, AllenemyType[a].enemySprite);
    }
    public void Start()
    {
        AllenemyType = Resources.LoadAll<EnemysStates>("enemy");
        SpawnEnemy();
    }

}
