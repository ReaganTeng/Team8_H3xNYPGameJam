using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript gmInstance;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private GameObject arrowPanel;
    [HideInInspector] public enum GameStates
    {
        START,
        PLAYING,
        PAUSED,
        UPGRADE,
        FALLING,
        LOSE
    }
    public GameStates gameState;
    private List<GameObject> arrows = new List<GameObject>();

    private int baseArrowCount = 5;
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

            createArrows();
        }
        if (gameState == GameStates.FALLING)
        {
            if (arrows.Count > 0)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    if (arrowPanel.transform.GetChild(arrowPanel.transform.childCount - 1).GetComponent<ArrowScript>().getDir() == ArrowScript.arrowDirection.UP)
                    {
                        arrows.RemoveAt(0);
                        Destroy(arrowPanel.transform.GetChild(arrowPanel.transform.childCount - 1).gameObject);
                    }
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    if (arrowPanel.transform.GetChild(arrowPanel.transform.childCount - 1).GetComponent<ArrowScript>().getDir() == ArrowScript.arrowDirection.LEFT)
                    {
                        arrows.RemoveAt(0);
                        Destroy(arrowPanel.transform.GetChild(arrowPanel.transform.childCount - 1).gameObject);
                    }
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    if (arrowPanel.transform.GetChild(arrowPanel.transform.childCount - 1).GetComponent<ArrowScript>().getDir() == ArrowScript.arrowDirection.DOWN)
                    {
                        arrows.RemoveAt(0);
                        Destroy(arrowPanel.transform.GetChild(arrowPanel.transform.childCount - 1).gameObject);
                    }
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    if (arrowPanel.transform.GetChild(arrowPanel.transform.childCount - 1).GetComponent<ArrowScript>().getDir() == ArrowScript.arrowDirection.RIGHT)
                    {
                        arrows.RemoveAt(0);
                        Destroy(arrowPanel.transform.GetChild(arrowPanel.transform.childCount - 1).gameObject);
                    }
                }
            }
            else 
            {
                arrowPanel.SetActive(false);
                gameState = GameStates.PLAYING; 
            }

        }
    }
    private void createArrows()
    {
        for(int i = 0; i < baseArrowCount; i++)
        {
            Instantiate(arrowPrefab, arrowPanel.transform);
            ArrowScript arrow = arrowPrefab.GetComponent<ArrowScript>();
            arrow.SetRandomDirection(arrow.getRandomDirection());
            arrows.Add(arrowPrefab);
        }
        gameState = GameStates.FALLING;
        arrowPanel.SetActive(true);
    }
}
