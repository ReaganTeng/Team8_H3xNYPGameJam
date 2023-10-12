using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelMang : MonoBehaviour
{
    public static levelMang Instance;

    private void Awake()
    {
        Instance = this;
    }

    public float getMaxHeight()
    {
        return transform.position.y + transform.localScale.y / 2;
    }
    public float getMinHeight()
    {
        return transform.position.y - transform.localScale.y / 2;
    }
    public float getMaxWidth()
    {
        return transform.position.x + transform.localScale.x / 2;
    }
    public float getMinWidth()
    {
        return transform.position.x - transform.localScale.x / 2;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            Debug.Log(getMinHeight());
        }
    }

}
