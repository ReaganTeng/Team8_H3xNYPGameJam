using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGAManager : MonoBehaviour
{
    public GameObject gamemanager;
    public GameManagerScript gms;

    public AudioClip MainmenuMusic;
    public AudioClip GameplayMusic;

    AudioSource AS;

    // Start is called before the first frame update
    void Awake()
    {
        gms = gamemanager.GetComponent<GameManagerScript>();
        AS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gms.gameState == GameManagerScript.GameStates.START && AS.clip != MainmenuMusic)
        {
            Debug.Log("MAIN MENU");
            AS.clip = MainmenuMusic;
            AS.Play();
        }
        else if (gms.gameState != GameManagerScript.GameStates.START && AS.clip != GameplayMusic)
        {
            Debug.Log("PLAYING");
            AS.clip = GameplayMusic;
            AS.Play();
        }
    }
}
