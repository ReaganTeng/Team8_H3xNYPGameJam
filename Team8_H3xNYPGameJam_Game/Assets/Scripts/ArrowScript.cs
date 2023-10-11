using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    private RectTransform arrowRectTransform;
    private enum arrowDirection
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }
    private arrowDirection direction;
    // Start is called before the first frame update
    void Start()
    {
        arrowRectTransform = GetComponent<RectTransform>();
        SetRandomDirection(arrowDirection.DOWN);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private arrowDirection getRandomDirection()
    {
        int random = Random.Range(0, 3);
        switch (random)
        {

        }
        return arrowDirection.UP;
    }
    private void SetRandomDirection(arrowDirection dir)
    {
        direction = dir;
        Vector3 rotVector = new Vector3(0,0,0);
        switch (direction)
        {
            case arrowDirection.UP:
                break;
            case arrowDirection.DOWN:
                rotVector = new Vector3(0, 0, 180);
                break;
            case arrowDirection.LEFT:
                rotVector = new Vector3(0, 0, 90);
                break;
            case arrowDirection.RIGHT:
                rotVector = new Vector3(0, 0, -90);
                break;
            default:
                rotVector = new Vector3(0, 0, 0);
                break;

        }
        arrowRectTransform.rotation = Quaternion.Euler(rotVector);
    }
}
