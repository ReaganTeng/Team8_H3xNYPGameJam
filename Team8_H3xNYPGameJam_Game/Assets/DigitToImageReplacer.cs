using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
using System.Linq;

public class DigitToImageReplacer : MonoBehaviour
{
    public GameObject background; // Reference to the background sprite or GameObject

    public Sprite[] characterSprites; // An array of sprites to use for replacement
    public int maxDigits = 5;
    public Image img;

    public TextMeshProUGUI textComponent; // Reference to the TextMeshProUGUI component

    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();

        characterSprites = Resources.LoadAll<Sprite>("Digits");
    }

    private void Update()
    {
        //ReplaceDigitsWithSprites();
    }
    private void Start()
    {
    }

    private void OnEnable()
    {
        SetNumber(transform.GetComponent<TextMeshProUGUI>().text);
    }

    void Clear()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
    }

    public void SetNumber(string number)
    {
        Clear();
        if (int.TryParse(number, out int a))
        {
            string numberString = number;
            transform.GetComponent<TextMeshProUGUI>().text = "";
            // Ensure the number of digits doesn't exceed the maximum
            if (numberString.Length > maxDigits)
                numberString = new string('9', maxDigits);

            for (int i = 0; i < numberString.Length; i++)
            {
                // Get the individual digit
                int digit = int.Parse(numberString[i].ToString());
               
                // Access the digit GameObject or sprite and set the appropriate sprite
                Image Digit = Instantiate(img, background.transform) as Image;
                Digit.sprite = characterSprites[digit];

            }
        }
    }

    public void ReplaceDigitsWithSprites()
    {
        //string originalText = textComponent.text;
        string originalText = "12345";

        for (int i = 0; i < originalText.Length; i++)
        {
            char character = originalText[i];

            if (char.IsDigit(character))
            {
                int digit = character - '0';

                if (digit >= 0 && digit < characterSprites.Length)
                {
                    // Replace the character with a sprite
                    string spriteTag = $"<sprite={digit}>";
                    originalText = originalText.Remove(i, 1).Insert(i, spriteTag);
                    i += spriteTag.Length - 1; // Adjust the index based on the length of the added tag
                }
            }
        }

        // Update the TextMeshProUGUI component with the replaced text
        textComponent.text = originalText;
    }

}
