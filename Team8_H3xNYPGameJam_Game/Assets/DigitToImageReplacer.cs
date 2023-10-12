using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
using System.Linq;

public class DigitToImageReplacer : MonoBehaviour
{
    public RawImage rawImage; // Reference to the RawImage component
    public string textToReplace = "12345"; // The text to replace
    public Texture2D[] digitImages; // An array of digit images (0-9)
    TextMeshProUGUI textComponent;  // Reference to the UI Text component

    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        rawImage = GetComponentInChildren<RawImage>();
    }

    public void Update()
    {
        ReplaceCharacters();
    }

    void ReplaceCharacters()
    {
        if (rawImage != null && textComponent != null)
        {
            string replacedText = "";

            foreach (char character in textToReplace)
            {
                if (char.IsDigit(character) && character >= '0' && character <= '9')
                {
                    int digit = (int)char.GetNumericValue(character);

                    if (digit >= 0 && digit < digitImages.Length)
                    {
                        // Replace the character with the corresponding image
                        Texture2D image = digitImages[digit];
                        replacedText += $"<texture={image.GetInstanceID()}>";
                    }
                }
                else
                    replacedText += character.ToString(); // Keep non-digits as they are
            }

            // Update the Text component with the replaced text
            textComponent.text = replacedText;
        }
    }

}
