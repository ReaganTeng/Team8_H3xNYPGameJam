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

    public TextMeshProUGUI textComponent; // Reference to the TextMeshProUGUI component

    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        //ReplaceDigitsWithSprites();
        SetNumber(12);
    }



    public void SetNumber(int number)
    {
        string numberString = number.ToString();

        // Ensure the number of digits doesn't exceed the maximum
        if (numberString.Length > maxDigits)
            numberString = new string('9', maxDigits);

        for (int i = 0; i < maxDigits; i++)
        {
            // Get the individual digit
            int digit = i < numberString.Length ? int.Parse(numberString[i].ToString()) : 0;

            // Access the digit GameObject or sprite and set the appropriate sprite
            GameObject digitObject = background.transform.GetChild(i).gameObject;
            Image digitImage = digitObject.GetComponent<Image>();
            digitImage.sprite = characterSprites[digit];
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
