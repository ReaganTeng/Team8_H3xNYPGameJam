using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
public class enemyManeger : MonoBehaviour
{
    enemy e = null;
    EnemysStates[]AllenemyType;
    [SerializeField] EnemysStates ES;
    public static enemyManeger EM;
    private void Awake()
    {
        if (EM == null)
        {
            EM = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SpawnEnemy()
    {
        if(e != null)
        {
           Destroy(e.gameObject);
            e = null;
        }
        GameObject SpawnEnemy = new GameObject();
        e= SpawnEnemy.AddComponent(typeof(enemy)) as enemy;
        SpawnEnemy.AddComponent(typeof(SpriteRenderer));
        SpawnEnemy.AddComponent<SpriteMan>();
        SpawnEnemy.AddComponent<BoxCollider2D>().isTrigger=true;

        //ADD ENEMY AUDIO MANAGER
        SpawnEnemy.AddComponent<AudioSource>();


        SpawnEnemy.transform.position *= 3;

        SpawnEnemy.transform.localScale *= 3;
        SpawnEnemy.name = "Enemy";


        setEnemyStats();

        // Instantiate(SpawnEnemy, new Vector3(0,.7f,0),Quaternion.identity);
       
    }

    private void setEnemyStats()
    {
        int a = Random.Range(0, AllenemyType.Length);
        e.SetEnemyStats(AllenemyType[a]);
    }
    public void Start()
    {
        AllenemyType = Resources.LoadAll<EnemysStates>("enemy");
       // SpawnEnemy();
    }

    public bool SendHIt()
    {
       return e.PlayerHit();
    }
    public void CallToMove()
    {
         e.Ready();
    }

}
