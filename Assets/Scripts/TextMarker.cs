using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextMarker : MonoBehaviour
{

    public Sprite endImage;
    public Sprite continueImage;
    private Image markerImage;
    // Start is called before the first frame update
    void Start()
    {
        markerImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowContinueMarker()
    {
        markerImage.enabled = true;
        markerImage.sprite = continueImage;
        
    }

    public void ShowEndMarker()
    {
        markerImage.enabled = true;
        markerImage.sprite = endImage;
        
    }
    
    public void HideMarker()
    {
        markerImage.enabled = false;
    }
}
