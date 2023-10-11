using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    private RectTransform arrowRectTransform;
    public enum arrowDirection
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
        //SetRandomDirection(getRandomDirection());
    }
    public arrowDirection getRandomDirection()
    {
        int random = Random.Range(0, 4);
        switch (random)
        {
            case 0:
                return arrowDirection.UP;
            case 1:
                return arrowDirection.DOWN;
            case 2:
                return arrowDirection.LEFT;
            case 3:
                return arrowDirection.RIGHT;
            default:
                return arrowDirection.UP;
        }
    }
    public arrowDirection getDir()
    {
        return direction;
    }
    public void SetRandomDirection(arrowDirection dir)
    {
        direction = dir;
        Vector3 rotVector = new Vector3(0, 0, 0);
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
