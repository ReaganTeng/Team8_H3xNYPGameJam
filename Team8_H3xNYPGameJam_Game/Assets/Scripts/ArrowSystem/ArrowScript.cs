using System;
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
    public arrowDirection direction;
    // Start is called before the first frame update
    void Start()
    {
        
        //SetRandomDirection(getRandomDirection());
    }
    public arrowDirection getRandomDirection()
    {
        int random = UnityEngine.Random.Range(0, Enum.GetValues(typeof(arrowDirection)).Length);
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
        arrowRectTransform = GetComponent<RectTransform>();
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
