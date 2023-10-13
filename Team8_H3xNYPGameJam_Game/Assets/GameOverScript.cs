using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    [SerializeField] private List<Sprite> numbersList = new List<Sprite>();
    [SerializeField] private Image defaultImg;
    [SerializeField] private GameObject scoreImg;
    [SerializeField] private GameObject highScoreImg;
    private void OnEnable()
    {
        string scoreLength = Player2.instance.totalenemiesDefeated.ToString();
        for(int i = 0; i < scoreLength.Length; i++)
        {
            int digit = int.Parse(scoreLength[i].ToString());
            Image scoreDigit = Instantiate(defaultImg, scoreImg.transform);
            scoreDigit.sprite = numbersList[digit];
            scoreDigit.GetComponent<RectTransform>().sizeDelta = new Vector2(88, 96);
        }
        // setting new high score
        if(Player2.instance.totalenemiesDefeated > PlayerPrefs.GetFloat("Highscore", 0))
        {
            PlayerPrefs.SetFloat("Highscore", Player2.instance.totalenemiesDefeated);
        }
        string highScoreLength = PlayerPrefs.GetFloat("Highscore", 0).ToString();
        for (int i = 0; i < highScoreLength.Length; i++)
        {
            int digit = int.Parse(highScoreLength[i].ToString());
            Image scoreDigit = Instantiate(defaultImg, highScoreImg.transform);
            scoreDigit.sprite = numbersList[digit];
            scoreDigit.GetComponent<RectTransform>().sizeDelta = new Vector2(88, 96);
        }
    }
    public void OnRestartButtonClicked()
    {
        SceneManager.LoadScene(0);
    }
}
