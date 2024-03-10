using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{

    //<color=#ff000000>

    private string _tempText = "Hey this is an example of what a character might say displayed on this text box. Now you know!";

    

    public UICharacterManager characterManager;
    public GameObject textPanel;
    public TMP_Text name;
    public TMP_Text bodyText;
    public GameObject textMarker;

    [Range(0.0f, 0.5f)]
    public float textSpeed = 0.03f;

    private bool _activateText = false;

    private float _textDisplayTimer = 0.0f;
    private int _currentLetterIndex = 0;
    private string _nextText = "";
    private string _typingText = "";

    public TextAsset fileData;
    public Button[] dialogueButtons;
    private string[] fileDataInfo;
    private List <string[]> dialogueSections = new List<string[]>(); // NOT USED

    private Character currentCharacter;

    private List<string> tempNames = new List<string>();
    private List<string> tempDialogue = new List<string>();
    private List<string> tempPortrait = new List<string>();
    private List<string> tempQuestions = new List<string>();

    public enum TextState { Wait, TextDisplaying, TextFinished, QuestionDisplaying, QuestionFinished, Done };
    public TextState textCurrentState;

    private int currentTextIndex = 0; 
    private int currentQuestionIndex = 0;
    //public string fileData = System.IO.File.ReadAllText("/assets/CSV Files/Mimic Dialogue - Sheet 1.csv");

    // Start is called before the first frame update
    void Start()
    {
        string[] section;
        fileDataInfo = fileData.text.Split("{STOP}");
        textPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CheckTextStates();
    }

    void CheckTextStates()
    {
        switch(textCurrentState)
        {
            case TextState.Wait:

                break;
            case TextState.TextDisplaying:
                this.DisplayText();
                break;
            case TextState.TextFinished:
                textMarker.GetComponent<TextMarker>().ShowContinueMarker();
                WaitingOnTextFinished();
                break;
            case TextState.QuestionDisplaying:
                DisplayQuestion();
                break;
            case TextState.QuestionFinished:

                break;
            case TextState.Done:
                textMarker.GetComponent<TextMarker>().HideMarker();
                DeActivateTextUI();
                textCurrentState = TextState.Wait;
                break;
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
        ResetDialogueText();
        currentCharacter = personClicked.GetComponent<Character>();
        int characterDialogueIndex = currentCharacter.currentDialogueIndex;
        Debug.Log(fileDataInfo[characterDialogueIndex - 1]);
        string currentDialogueInfo = fileDataInfo[characterDialogueIndex - 1];
        string[] currentInfoSplit = currentDialogueInfo.Split(',');

        OrguanizeText(currentInfoSplit, currentCharacter);
        PopulateCurrentText(tempNames[currentTextIndex], tempDialogue[currentTextIndex]);
        characterManager.UpdateCharacterImage(tempPortrait[currentTextIndex]);
    }

    private void OrguanizeText(string[] currentInfo, Character personClicked)
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
                if(currentInfo[i+1][0].Equals('{')) //accounts for questions
                {
                    tempQuestions.Add(currentInfo[i + 1]);
                    tempDialogue.Add("{QUESTION}");
                }
                else
                {
                    tempDialogue.Add(currentInfo[i + 1]);
                }
                
            }
            else if(current.Trim().Equals("{IMAGE}".Trim()))
            {
                tempPortrait.Add(currentInfo[i + 1]);
            }
            /*else if (current.Trim().Equals("{STOP}".Trim()))
            {
                personClicked.updateDialogueIndex(int.Parse(currentInfo[i - 1]));
            }*/
        }
        personClicked.updateDialogueIndex(int.Parse(currentInfo[currentInfo.Length - 2]));
    }

    private void PopulateCurrentText(string newName, string newDialogue)
    {
        name.text = newName;
        _nextText = "<color=#ff000000>" + newDialogue + "</color>";
        bodyText.text = _nextText;
    }

    private void WaitingOnTextFinished()
    {
        if ((Input.GetKeyDown(KeyCode.Mouse0))) // player is progressing through the dialogue
        {
            currentTextIndex += 1;
            if(currentTextIndex < tempNames.Count)
            {
                Debug.Log(tempDialogue[currentTextIndex].Trim());
                if (tempDialogue[currentTextIndex].Trim().Equals("{QUESTION}".Trim()))
                {
                    textCurrentState = TextState.QuestionDisplaying;
                }
                else
                {
                    PopulateCurrentText(tempNames[currentTextIndex], tempDialogue[currentTextIndex]);
                    characterManager.UpdateCharacterImage(tempPortrait[currentTextIndex]);
                    textMarker.GetComponent<TextMarker>().HideMarker();
                    textCurrentState = TextState.TextDisplaying;
                }    
               

            }
            else
            {
                textCurrentState = TextState.Done;
            }
        }
    }



    public void ActivateTextUI()
    {
        textPanel.SetActive(true);
        _activateText = true;
        characterManager.ActivateUICharacters(tempPortrait[0]);
        textCurrentState = TextState.TextDisplaying;
    }
    
    public void DeActivateTextUI()
    {
        textPanel.SetActive(false);
        _activateText = false;
        characterManager.DeActivateUICharacters();
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
           

            _nextText = _nextText.Insert(_currentLetterIndex, "<color=#ff000000>"); // insert rich text alpha after new letter to hide the remaining letters
            bodyText.text = _nextText; // update text object

            if (currentLetter.Trim().Equals("<".Trim())) //if letter equals the rich text first chara, stop
            {
                _currentLetterIndex = 0;
                textCurrentState = TextState.TextFinished; // stop showing letters
                //_activateText = false; 
            }

        }

        if ((Input.GetKeyDown(KeyCode.Mouse0))) // skip text scroll
        {
            _currentLetterIndex = 0;
            bodyText.text = tempDialogue[currentTextIndex];
            textCurrentState = TextState.TextFinished;

        }
    }

    void DisplayQuestion()
    {
        DeActivateTextUI();
        Debug.Log(tempQuestions[currentQuestionIndex]);
        string[] currentQuestion = tempQuestions[currentQuestionIndex].Split('/');

        for(int i = 1; i < currentQuestion.Length; i++) // int i = 1 to account for the {question} tag
        {
            Button currentButton = dialogueButtons[i - 1];
            TMP_Text buttonText = currentButton.gameObject.transform.GetChild(0).GetComponent<TMP_Text>();
            currentButton.gameObject.SetActive(true);
            string[] questionElements = currentQuestion[i].Split('^');
            Debug.Log(questionElements[1]);
            buttonText.text = questionElements[0];
            currentButton.GetComponent<DialogueButton>().updateNextIndex(int.Parse(questionElements[1].ToString()));
        }

        
        textCurrentState = TextState.QuestionFinished;
    }

    public void QuestionWasSelected(int newIndex)
    {
        currentCharacter.updateDialogueIndex(newIndex);
        SetupNewText(currentCharacter.gameObject);
        ActivateTextUI();

        for(int i = 0; i < dialogueButtons.Length; i++)
        {
            dialogueButtons[i].gameObject.SetActive(false);
        }
    }

    void ResetDialogueText()
    {
        currentTextIndex = 0;
        tempNames = new List<string>();
        tempDialogue = new List<string>();
        tempPortrait = new List<string>();
    }
}
