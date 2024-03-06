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

    public TextAsset fileData;
    private string[] fileDataInfo;
    private List <string[]> dialogueSections = new List<string[]>(); // NOT USED


    private List<string> tempNames = new List<string>();
    private List<string> tempDialogue = new List<string>();

    private int currentTextIndex = 0; // TODO add in state manager + finsih setup
    //public string fileData = System.IO.File.ReadAllText("/assets/CSV Files/Mimic Dialogue - Sheet 1.csv");

    // Start is called before the first frame update
    void Start()
    {
        string[] section;
        fileDataInfo = fileData.text.Split("{STOP}");
        for (int i = 0; i < fileDataInfo.Length; i++)
        {
            //section = fileDataInfo[i].Split(",");
            //dialogueSections.Add(section);
            //Debug.Log(fileDataInfo[i]);
        }

       /* for (int i = 0; i < fileDataInfo.Length; i++)
        {
            Debug.Log(dialogueSections[i]);
        }
*/
        


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

    public void SetupNewText(GameObject personClicked)
    {
        Character currentCharacter = personClicked.GetComponent<Character>();
        int characterDialogueIndex = currentCharacter.currentDialogueIndex;
        string currentDialogueInfo = fileDataInfo[characterDialogueIndex - 1];
        string[] currentInfoSplit = currentDialogueInfo.Split(',');

        orguanizeText(currentInfoSplit, currentCharacter);
    }

    private void orguanizeText(string[] currentInfo, Character personClicked)
    {
        for(var i = 0; i < currentInfo.Length; i++)
        {
            string current = currentInfo[i];


            if (current.Trim().Equals("{NAME}".Trim()))
            {
                tempNames.Add(currentInfo[i + 1]);
            }
            else if (current.Trim().Equals("{DIALOGUE}".Trim()))
            {
                tempNames.Add(currentInfo[i + 1]);
            }
            else if (current.Trim().Equals("{STOP}".Trim()))
            {
                personClicked.updateDialogueIndex(int.Parse(currentInfo[i - 1]));
            }
        }    
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
