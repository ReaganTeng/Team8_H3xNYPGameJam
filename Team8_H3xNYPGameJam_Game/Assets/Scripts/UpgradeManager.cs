using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    public GameObject player;
    Player playerScript;

    AudioSource AS;
    public AudioClip chaChingSound;
    //AS.Play();

    private void Awake()
    {
        playerScript = player.GetComponent<Player>();

        AS = GetComponent<AudioSource>();
        AS.clip = chaChingSound;

    }

    public void UpgradeWeight()
    {
        playerScript.playerWeight += 1;
        playerScript.playerWeightText.text = playerScript.playerWeight.ToString();
        DisableShop();
    }

    public void UpgradeStrength()
    {
        playerScript.playerStrength += 1;
        playerScript.playerStrengthText.text = playerScript.playerStrength.ToString();
        DisableShop();
    }

    public void UpgradeSpeed()
    {
        playerScript.playerSpeed += 1;
        playerScript.playerSpeedText.text = playerScript.playerSpeed.ToString();
        DisableShop();
    }

    public void DisableShop()
    {
        playerScript.shopCanvas.enabled = false;
        playerScript.enemiesDefeated = 0;
        AS.Play();
    }
}
