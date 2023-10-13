using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioManager : MonoBehaviour
{
    public AudioClip[] hurtsounds;
    public AudioClip[] hitsounds;


    public AudioSource AS;

    // Start is called before the first frame update
    void Start()
    {
        AS = GetComponent<AudioSource>();
    }


    //RANDOMISE A SOUND TO PLAY
    public void PlayRandomSound(int listNumber)
    {
        AudioClip[] soundList = hurtsounds;
        if (listNumber == 0)
        {
            soundList = hurtsounds;
        }
        if (listNumber == 1)
        {
            soundList = hitsounds;
        }

        int idx = Random.Range(0, soundList.Length);
        Debug.Log("Sound Played " + soundList[idx].name);
        // Play the sound at the specified index
        AS.clip = soundList[idx];
        AS.Play();
    }
    //

    // Update is called once per frame
    void Update()
    {
        
    }
}
