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
        SpawnEnemy.transform.rotation = Quaternion.Euler(0, 0, 180);
        e= SpawnEnemy.AddComponent(typeof(enemy)) as enemy;
        SpawnEnemy.AddComponent(typeof(SpriteRenderer));
        SpawnEnemy.AddComponent<SpriteMan>();
        SpawnEnemy.AddComponent<BoxCollider2D>().isTrigger=true;
        SpawnEnemy.transform.localScale *= 1;
        SpawnEnemy.name = "Enemy";
       // Instantiate(SpawnEnemy, new Vector3(0,.7f,0),Quaternion.identity);
        setEnemyStats();
    }

    private void setEnemyStats()
    {
        int a = Random.Range(0, AllenemyType.Length);
        e.SetEnemyStats(AllenemyType[a]);
    }
    public void Start()
    {
        AllenemyType = Resources.LoadAll<EnemysStates>("enemy");
        SpawnEnemy();
    }

}
