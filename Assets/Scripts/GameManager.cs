using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    public CameraManager cameraManager;
    public TextManager textManager;

    public UnityEvent activateText;
    public class StringEvent : UnityEvent<string> { }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CharacterWasClicked(GameObject character)
    {
        // we need to get info as to the convo that needs to happen here
        textManager.SetupNewText(character);
        textManager.ActivateTextUI();
    }
}
