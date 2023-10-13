using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
using System.Linq;

public class DigitToImageReplacer : MonoBehaviour
{

    public Sprite[] characterSprites; // An array of sprites to use for replacement
    public int maxDigits = 5;
    public Image img;

    TextMeshProUGUI textComponent; // Reference to the TextMeshProUGUI component

    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();

        characterSprites = Resources.LoadAll<Sprite>("Digits");
    }

    private void Update()
    {
        
    }
  

    private void OnEnable()
    {

    }

    public void ChangeToImg()
    {
        SetNumber(transform.GetComponent<TextMeshProUGUI>().text);
        transform.GetComponent<TextMeshProUGUI>().text = "";
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
        //if (int.TryParse(number, out int a))
        {
            string numberString = number;
            // Ensure the number of digits doesn't exceed the maximum
            if (numberString.Length > maxDigits)
                numberString = new string('9', maxDigits);

            for (int i = 0; i < numberString.Length; i++)
            {
                // Get the individual digit
                int digit = int.Parse(numberString[i].ToString());

                Debug.Log(numberString[i]);
                // Access the digit GameObject or sprite and set the appropriate sprite
                Image Digit = Instantiate(img, transform) as Image;
                Digit.sprite = characterSprites[digit];

            }
        }
    }







    //public string originalText; // The text you want to replace
    //public string digitFolderName = "Digits"; // The name of the folder containing digit sprites

    //private Dictionary<char, Sprite> digitSprites = new Dictionary<char, Sprite>();

    //private void Start()
    //{
    //    // Load digit sprites from the "digit" folder
    //    LoadDigitSprites();

    //    // Replace text with sprites
    //    ReplaceTextWithDigitSprites();
    //}

    //private void LoadDigitSprites()
    //{
    //    // Load all sprites from the specified folder
    //    Sprite[] sprites = Resources.LoadAll<Sprite>(digitFolderName);

    //    // Store digit sprites in a dictionary for easy lookup
    //    foreach (var sprite in sprites)
    //    {
    //        char digit = sprite.name[0]; // Assumes sprite names are single characters
    //        digitSprites[digit] = sprite;
    //    }
    //}


    //private void ReplaceTextWithDigitSprites()
    //{
    //    if (textComponent != null)
    //    {
    //        string replacedText = "";

    //        foreach (char character in "22")
    //        {
    //            if (digitSprites.ContainsKey(character))
    //            {
    //                Sprite sprite = digitSprites[character];
    //                // Use the digitFolderName in the sprite name
    //                replacedText += $"<sprite name={digitFolderName}/{sprite.name}>";
    //            }
    //            else
    //            {
    //                replacedText += character.ToString(); // Keep non-replaceable characters as they are
    //            }
    //        }

    //        // Update the TextMeshProUGUI component with the replaced text
    //        textComponent.text = replacedText;
    //    }
    //}

    //private void ReplaceTextWithDigitSprites()
    //{
    //    if (textComponent != null)
    //    {
    //        string replacedText = "";

    //        foreach (char character in "444")
    //        {
    //            if (digitSprites.ContainsKey(character))
    //            {
    //                Sprite sprite = digitSprites[character];
    //                replacedText += $"<sprite name={sprite.name}>";
    //            }
    //            else
    //            {
    //                replacedText += character.ToString(); 
    //            }
    //        }
    //        textComponent.text = replacedText;
    //    }
    //}

}
