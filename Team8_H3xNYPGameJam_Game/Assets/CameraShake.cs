using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Vector3 pos;

    [Range(0.0F, 10.0F)]
    public float strenght;

    [Range(0.0F, 10.0F)]
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        pos= transform.position;
        transform.DOShakePosition(timer, strenght, 10, 90, false, false).OnComplete(() =>
        {
            transform.DOMove(pos, 1);
        });
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            transform.DOShakePosition(timer, strenght, 10, 90, false, false).OnComplete(() =>
            {
                transform.DOMove(pos, 1);
            });
        }
      
    }
}
