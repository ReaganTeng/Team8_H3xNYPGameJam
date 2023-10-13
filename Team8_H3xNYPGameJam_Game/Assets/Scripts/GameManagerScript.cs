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
    private ArrowTimerScript timerScript;
    [HideInInspector] public enum GameStates
    {
        START,
        PLAYING,
        PAUSED,
        UPGRADE,
        FALLING,
        TUTORIAL,
        LOSE
    }
    public GameStates gameState;
    private List<GameObject> arrows = new List<GameObject>();

    private int arrowCount = 3;
    public bool canVibrate = true;
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
        timerScript = arrowPanel.GetComponent<ArrowTimerScript>();
    }

    // Update is called once per frame
    void Update()
    {
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
            ArrowInputsControlPC();
            ArrowInputsControlMobile();
        }
        else
        {
            arrowPanel.SetActive(false);
            if (arrowCount <= 15)
                arrowCount++;
            gameState = GameStates.PLAYING;
            Player2.instance.Recover();
            Player2.instance.transform.DOMove(Player2.instance.startingLocation(), 1).OnComplete(() => {
                Player2.instance.isBack = true;
                Player2.instance.currentPlayerState = playerState.IDLE;
                enemyManeger.EM.CallToMove();
            });
        }

    }


    private void ArrowInputsControlPC()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (arrowPanel.transform.GetChild(arrowPanel.transform.childCount - 1).GetComponent<ArrowScript>().getDir() == ArrowScript.arrowDirection.UP)
            {
                arrows.RemoveAt(arrows.Count - 1);
                Destroy(arrowPanel.transform.GetChild(arrowPanel.transform.childCount - 1).gameObject);
            }
            else
            {
                timerScript.setSliderValue(timerScript.getSliderValue() - 0.5f);
                CameraShake.instance.Shake();
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (arrowPanel.transform.GetChild(arrowPanel.transform.childCount - 1).GetComponent<ArrowScript>().getDir() == ArrowScript.arrowDirection.LEFT)
            {
                arrows.RemoveAt(arrows.Count - 1);
                Destroy(arrowPanel.transform.GetChild(arrowPanel.transform.childCount - 1).gameObject);
            }
            else
            {
                timerScript.setSliderValue(timerScript.getSliderValue() - 0.5f);
                CameraShake.instance.Shake();
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (arrowPanel.transform.GetChild(arrowPanel.transform.childCount - 1).GetComponent<ArrowScript>().getDir() == ArrowScript.arrowDirection.DOWN)
            {
                arrows.RemoveAt(arrows.Count - 1);
                Destroy(arrowPanel.transform.GetChild(arrowPanel.transform.childCount - 1).gameObject);
            }
            else
            {
                timerScript.setSliderValue(timerScript.getSliderValue() - 0.5f);
                CameraShake.instance.Shake();
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (arrowPanel.transform.GetChild(arrowPanel.transform.childCount - 1).GetComponent<ArrowScript>().getDir() == ArrowScript.arrowDirection.RIGHT)
            {
                arrows.RemoveAt(arrows.Count - 1);
                Destroy(arrowPanel.transform.GetChild(arrowPanel.transform.childCount - 1).gameObject);
            }
            else
            {
                timerScript.setSliderValue(timerScript.getSliderValue() - 0.5f);
                CameraShake.instance.Shake();
            }
        }
    }


    private void ArrowInputsControlMobile()
    {
       
        // Check for swipe
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 deltaPosition = touch.deltaPosition;
                //UP SWIPE
                if (deltaPosition.y > Screen.height * 0.1f)
                {
                    if (arrowPanel.transform.GetChild(arrowPanel.transform.childCount - 1).GetComponent<ArrowScript>().getDir() == ArrowScript.arrowDirection.UP)
                    {
                        arrows.RemoveAt(arrows.Count - 1);
                        Destroy(arrowPanel.transform.GetChild(arrowPanel.transform.childCount - 1).gameObject);
                    }
                    else
                    {
                        timerScript.setSliderValue(timerScript.getSliderValue() - 0.5f);
                        CameraShake.instance.Shake();
                        if(canVibrate)
                            Handheld.Vibrate();
                    }
                }
                //LEFT SWIPE
                if (deltaPosition.x < -Screen.width * 0.1f)
                {
                    if (arrowPanel.transform.GetChild(arrowPanel.transform.childCount - 1).GetComponent<ArrowScript>().getDir() == ArrowScript.arrowDirection.LEFT)
                    {
                        arrows.RemoveAt(arrows.Count - 1);
                        Destroy(arrowPanel.transform.GetChild(arrowPanel.transform.childCount - 1).gameObject);
                    }
                    else
                    {
                        timerScript.setSliderValue(timerScript.getSliderValue() - 0.5f);
                        CameraShake.instance.Shake();
                        if (canVibrate)
                            Handheld.Vibrate();
                    }
                }
                //DOWN SWIPE
                if (deltaPosition.y < -Screen.height * 0.1f)
                {
                    if (arrowPanel.transform.GetChild(arrowPanel.transform.childCount - 1).GetComponent<ArrowScript>().getDir() == ArrowScript.arrowDirection.DOWN)
                    {
                        arrows.RemoveAt(arrows.Count - 1);
                        Destroy(arrowPanel.transform.GetChild(arrowPanel.transform.childCount - 1).gameObject);
                    }
                    else
                    {
                        timerScript.setSliderValue(timerScript.getSliderValue() - 0.5f);
                        CameraShake.instance.Shake();
                        if (canVibrate)
                            Handheld.Vibrate();
                    }
                }
                //RIGHT SWIPE
                if (deltaPosition.x > Screen.width * 0.1f)
                {
                    if (arrowPanel.transform.GetChild(arrowPanel.transform.childCount - 1).GetComponent<ArrowScript>().getDir() == ArrowScript.arrowDirection.RIGHT)
                    {
                        arrows.RemoveAt(arrows.Count - 1);
                        Destroy(arrowPanel.transform.GetChild(arrowPanel.transform.childCount - 1).gameObject);
                    }
                    else
                    {
                        timerScript.setSliderValue(timerScript.getSliderValue() - 0.5f);
                        CameraShake.instance.Shake();
                        if (canVibrate)
                            Handheld.Vibrate();
                    }
                }
            }
        }
      
    }
}
