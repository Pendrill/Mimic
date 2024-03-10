using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueButton : MonoBehaviour
{
    // Start is called before the first frame update

    public TextManager textManager;
    public int nextIndex = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateNextIndex(int index)
    {
        nextIndex = index;
    }

    public void OptionWasSelected()
    {
        textManager.QuestionWasSelected(nextIndex);
    }
}
