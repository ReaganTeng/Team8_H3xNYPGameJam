using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "enemy")]
public class EnemysStates : ScriptableObject
{
    public float speed;
    public float Weight;
    public float strength;
    public Sprite enemySprite;
    public string Enemy_name;
    public Sprite[] AttackAnimation;

}