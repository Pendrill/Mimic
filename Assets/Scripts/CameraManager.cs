using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraManager : MonoBehaviour
{

    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckForMouseClick();
    }

    void CheckForMouseClick()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
             
                if (hit.transform.name.Trim().Equals("PersonImage".Trim()))
                {
                    gameManager.CharacterWasClicked(hit.transform.parent.gameObject);
                }
            }
        }
    }
}
