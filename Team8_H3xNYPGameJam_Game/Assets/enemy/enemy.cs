using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class enemy : MonoBehaviour
{
    float speed;
    float Weight;
    float strength;
    Sprite enemySprite;
    

    public void SetEnemyStats(float s, float w, float str,Sprite ES)
    {
        speed = s;
        Weight= w; 
        strength = str;
        GetComponent<SpriteRenderer>().sprite = ES;
    }


}


