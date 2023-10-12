using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript gmInstance;
    public enum GameStates
    {
        START,
        PLAYING,
        SETTINGS,
        PAUSED,
        UPGRADE,
        LOSE
    }
    public GameStates gameState;
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
        Debug.Log(gameState);
    }
}
