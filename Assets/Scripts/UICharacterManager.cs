using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICharacterManager : MonoBehaviour
{
    public UICharacter[] uiCharacters;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateUICharacters(string name)
    {
        uiCharacters[0].gameObject.SetActive(true);
        //uiCharacters[1].gameObject.SetActive(true);
        //UpdateCharacterImage(name);
        uiCharacters[0].ActivateInitialCharacterUI();
        //uiCharacters[1].ActivateInitialCharacterUI();

    }

    public void DeActivateUICharacters()
    {
        uiCharacters[0].DeActivateInitialCharacterUI();
        uiCharacters[0].gameObject.SetActive(false);
    }

    public void UpdateCharacterImage(string name)
    {
        Debug.Log(name);
        uiCharacters[0].UpdateCharacterImage(name);
    }
}
