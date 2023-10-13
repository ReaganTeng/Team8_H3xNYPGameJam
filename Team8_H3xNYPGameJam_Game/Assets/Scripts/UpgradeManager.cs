using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    public GameObject player;
    //Player playerScript;

    Player2 playerScript;
    AudioSource AS;
    public AudioClip chaChingSound;
    public DigitToImageReplacer[] DTIR;
    //AS.Play();

    private void Awake()
    {
        playerScript = player.GetComponent<Player2>();

        AS = GetComponent<AudioSource>();
        AS.clip = chaChingSound;

    }
    public void UpgradeWeight()
    {
        playerScript.playerWeight += 1;
        DisableShop();
    }

    public void UpgradeStrength()
    {
        playerScript.playerStrength += 1;
        DisableShop();
    }

    public void UpgradeSpeed()
    {
        playerScript.playerSpeed += 1;
        DisableShop();
    }

    public void DisableShop()
    {
        enemyManeger.EM.SpawnEnemy();
        playerScript.playerStrengthText.text = playerScript.playerStrength.ToString();
        playerScript.playerWeightText.text = playerScript.playerWeight.ToString();
        playerScript.shopCanvas.gameObject.SetActive(false);
        playerScript.enemiesDefeated = 0;
        AS.Play();
    }
    private void OnEnable()
    {

        playerScript.playerStrengthText.text = playerScript.playerStrength.ToString();
        playerScript.playerWeightText.text = playerScript.playerWeight.ToString();
        for (int i = 0; i<DTIR.Length;i++)
        {
            DTIR[i].ChangeToImg();
        }
    }
}
