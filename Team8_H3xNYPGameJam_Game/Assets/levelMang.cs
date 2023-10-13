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
        width = transform.localScale.x * SR.bounds.size.x;
        height = transform.localScale.y * SR.bounds.size.y;

    }

    public float getMaxHeight()
    {
        return transform.position.y + height;
    }
    public float getMinHeight()
    {
        return transform.position.y - height;
    }
    public float getMaxWidth()
    {
        return transform.position.x + width;
    }
    public float getMinWidth()
    {
        return transform.position.x - width;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            Debug.Log(getMinHeight());
        }
    }

}
