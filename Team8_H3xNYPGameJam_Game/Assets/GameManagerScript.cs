using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript gmInstance;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private GameObject arrowPanel;
    public enum GameStates
    {
        START,
        PLAYING,
        PAUSED,
        UPGRADE,
        FALLING,
        LOSE
    }
    public GameStates gameState;
    private List<RectTransform> test = new List<RectTransform>();
    private void Awake()
    {
        if (gmInstance == null)
            gmInstance = this;
        else Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        gameState = GameStates.START;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            gameState = GameStates.FALLING;
            createArrows();
        }
        if(gameState == GameStates.FALLING)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {

            }
            if (Input.GetKeyDown(KeyCode.A))
            {

            }
            if (Input.GetKeyDown(KeyCode.S))
            {

            }
            if (Input.GetKeyDown(KeyCode.D))
            {

            }
            //arrowPanel.GetComponentInChildren<>
        }
    }
    private void createArrows()
    {
        for(int i = 0; i < 5; i++)
        {
            Instantiate(arrowPrefab, arrowPanel.transform);
            ArrowScript arrow = arrowPrefab.GetComponent<ArrowScript>();
            arrow.SetRandomDirection(arrow.getRandomDirection());
            Debug.Log(arrow.getDir());
        }
        
    }
}
