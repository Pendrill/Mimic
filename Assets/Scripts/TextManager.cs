using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{

    //<color=#ff000000>

    private string _tempText = "Hey this is an example of what a character might say displayed on this text box. Now you know!";

    public UICharacterManager characterManager;
    public GameObject textPanel;
    public TMP_Text name;
    public TMP_Text bodyText;

    [Range(0.0f, 0.5f)]
    public float textSpeed = 0.03f;

    private bool _activateText = false;

    private float _textDisplayTimer = 0.0f;
    private int _currentLetterIndex = 0;
    private string _nextText = "";
    private string _typingText = "";

    // Start is called before the first frame update
    void Start()
    {
        SetupInitialText();
    }

    // Update is called once per frame
    void Update()
    {
        if(_activateText)
        {
            this.DisplayText();
        }
    }

    void SetupInitialText()
    {
        name.text = "Eric";
        _nextText = "<color=#ff000000>" + _tempText + "</color>";
        bodyText.text = _nextText;
        
    }

    public void ActivateTextUI()
    {
        textPanel.SetActive(true);
        _activateText = true;
        characterManager.ActivateUICharacters();
    }

    void DisplayText()
    {
        _textDisplayTimer += Time.deltaTime;
        if(_textDisplayTimer >= textSpeed) // rate at which we display a new letter
        {
            _textDisplayTimer = 0f; // reset timer

            _nextText = _nextText.Remove(_currentLetterIndex, 17); //remove the rich text alpha
            _currentLetterIndex += 1;
            string currentLetter = _nextText[_currentLetterIndex].ToString(); //check next letter
            if (currentLetter.Trim().Equals("<".Trim())) //if letter equals the rich text first chara, stop
            {
                _activateText = false; // stop showing letters
            }

            _nextText = _nextText.Insert(_currentLetterIndex, "<color=#ff000000>"); // insert rich text alpha after new letter to hide the remaining letters
            bodyText.text = _nextText; // update text object

        }
    }
}
