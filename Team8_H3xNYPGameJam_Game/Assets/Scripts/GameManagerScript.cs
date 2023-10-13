using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

    private int arrowCount = 3;
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
            CheckArrowInputs();
        }
    }
    public void createArrows()
    {
        for(int i = 0; i < arrowCount; i++)
        {
            Instantiate(arrowPrefab, arrowPanel.transform);
            ArrowScript arrow = arrowPrefab.GetComponent<ArrowScript>();
            arrow.SetRandomDirection(arrow.getRandomDirection());
            arrows.Add(arrowPrefab);
        }
        gameState = GameStates.FALLING;
        arrowPanel.SetActive(true);
    }
    private void CheckArrowInputs()
    {
        if (arrows.Count > 0)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (arrowPanel.transform.GetChild(arrowPanel.transform.childCount - 1).GetComponent<ArrowScript>().getDir() == ArrowScript.arrowDirection.UP)
                {
                    arrows.RemoveAt(arrows.Count - 1);
                    Destroy(arrowPanel.transform.GetChild(arrowPanel.transform.childCount - 1).gameObject);
                }
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (arrowPanel.transform.GetChild(arrowPanel.transform.childCount - 1).GetComponent<ArrowScript>().getDir() == ArrowScript.arrowDirection.LEFT)
                {
                    arrows.RemoveAt(arrows.Count - 1);
                    Destroy(arrowPanel.transform.GetChild(arrowPanel.transform.childCount - 1).gameObject);
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (arrowPanel.transform.GetChild(arrowPanel.transform.childCount - 1).GetComponent<ArrowScript>().getDir() == ArrowScript.arrowDirection.DOWN)
                {
                    arrows.RemoveAt(arrows.Count - 1);
                    Destroy(arrowPanel.transform.GetChild(arrowPanel.transform.childCount - 1).gameObject);
                }
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (arrowPanel.transform.GetChild(arrowPanel.transform.childCount - 1).GetComponent<ArrowScript>().getDir() == ArrowScript.arrowDirection.RIGHT)
                {
                    arrows.RemoveAt(arrows.Count - 1);
                    Destroy(arrowPanel.transform.GetChild(arrowPanel.transform.childCount - 1).gameObject);
                }
            }
        }
        else
        {
            arrowPanel.SetActive(false);
            if (arrowCount <= 15)
                arrowCount++;
            gameState = GameStates.PLAYING; 
            Player2.instance.transform.DOMove(Player2.instance.startingLocation(), 1).OnComplete(() => {
                Player2.instance.isBack = true;
                Player2.instance.currentPlayerState = playerState.IDLE;
                enemyManeger.EM.CallToMove();
            });
        }

    }
}
