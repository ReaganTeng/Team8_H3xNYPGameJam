using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelMang : MonoBehaviour
{
    public static levelMang Instance;

    SpriteRenderer SR;
    float width;
    float height;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SR = GetComponent<SpriteRenderer>();
         width = SR.bounds.size.x;
         height = SR.bounds.size.y;

    }

    public float getMaxHeight()
    {
        return transform.position.y + height/2;
    }
    public float getMinHeight()
    {
        return transform.position.y - height / 2;
    }
    public float getMaxWidth()
    {
        return transform.position.x + width / 2;
    }
    public float getMinWidth()
    {
        return transform.position.x - width / 2;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            Debug.Log(getMinHeight());
        }
    }

}
