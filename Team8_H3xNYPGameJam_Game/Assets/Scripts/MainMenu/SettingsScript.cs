
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    [SerializeField] private Sprite volumeOnImage;
    [SerializeField] private Sprite volumeOffImage;
    [SerializeField] private Button volumeButton;
    private bool isAudioMuted = false;
    [SerializeField] private Sprite vibrationOnImage;
    [SerializeField] private Sprite vibrationOffImage;
    [SerializeField] private Button vibrationButton;
    private bool isVibrationOn = false;
    //[SerializeField] private SpriteRenderer
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnVolumeButtonClicked()
    {
        if (isAudioMuted)
        {
            isAudioMuted = false;
            volumeButton.image.sprite = volumeOnImage;
            AudioListener.volume = 1;
        }
        else
        {
            isAudioMuted = true;
            volumeButton.image.sprite = volumeOffImage;
            AudioListener.volume = 0;
        }
    }
    public void OnVibrationButtonClicked()
    {
        if (isVibrationOn)
        {
            isVibrationOn = false;
            vibrationButton.image.sprite = vibrationOnImage;
            //alter vibration
        }
        else
        {
            isVibrationOn = true;
            vibrationButton.image.sprite = vibrationOffImage;
            //alter vibration
        }
    }
}
