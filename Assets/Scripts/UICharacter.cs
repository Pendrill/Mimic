using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharacter : MonoBehaviour
{

    // Start is called before the first frame update
    public GameObject character;
    public RectTransform characterRect;
    public Image characterImage;

    //slerp variables for alpha
    public enum AlphaState { Wait, Slerping, Done};
    public AlphaState alphaCurrentState; 
    public float startAlpha = 0.0f;
    public float endAlpha = 0.0f;
    public float alphaJourneySpeed = 0.1f;
    private float _alphaLerpCompletion = 0.0f;

    //slerp variables for position
    public enum PosState { Wait, Slerping, Done };
    public PosState posCurrentState;
    public Vector3 startPosition;
    public Vector3 endPosition;
    public float positionJourneySpeed = 1.0f;
    private float _positionLerpCompletion = 0.0f;

    public bool flipDirection = false;

    
    void Start()
    {
        CheckFlip();
        HideImage(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowImage(true);
            EnterAnimation();
        }
        CheckAlpha();
        CheckPos();
    }

    void CheckFlip()
    {
        if(flipDirection)
        {
            characterRect.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            characterRect.localScale = new Vector3(1, 1, 1);
        }
    }

    void CheckAlpha()
    {
        switch(alphaCurrentState)
        {
            case AlphaState.Wait:
                break;
            case AlphaState.Slerping:
                //lerp the alpha of the image;
                characterImage.color = new Color(characterImage.color.r, characterImage.color.b, characterImage.color.g, Mathf.Lerp(characterImage.color.a, endAlpha, _alphaLerpCompletion / alphaJourneySpeed));
                _alphaLerpCompletion += Time.deltaTime;
                if (_alphaLerpCompletion >= 1.0f)
                {
                    alphaCurrentState = AlphaState.Done;
                }

                break;
            case AlphaState.Done:
                _alphaLerpCompletion = 0.0f;
                alphaCurrentState = AlphaState.Wait;
                break;
        }
    }

    void HideImage(bool lerp)
    { 
        if(lerp)
        {
            _alphaLerpCompletion = 0.0f;
            endAlpha = 0f;
            alphaCurrentState = AlphaState.Slerping;
        }
        else
        {
            characterImage.color = new Color(characterImage.color.r, characterImage.color.b, characterImage.color.g, 0); // set alpha of image to 0
        }    
    }

    void ShowImage(bool lerp)
    {
        if (lerp)
        {
            _alphaLerpCompletion = 0.0f;
            endAlpha = 1f;
            alphaCurrentState = AlphaState.Slerping;
        }
        else
        {
            characterImage.color = new Color(characterImage.color.r, characterImage.color.b, characterImage.color.g, 1); // set alpha of image to 0
        }
    }

    void CheckPos()
    {
        switch (posCurrentState)
        {
            case PosState.Wait:
                break;
            case PosState.Slerping:
                //lerp the alpha of the image;
                characterRect.anchoredPosition3D = Vector3.Lerp(startPosition, endPosition, _positionLerpCompletion / positionJourneySpeed);//new Color(characterImage.color.r, characterImage.color.b, characterImage.color.g, Mathf.Lerp(characterImage.color.a, endAlpha, _alphaLerpCompletion));
                _positionLerpCompletion += Time.deltaTime;
                if (_positionLerpCompletion >= 1.0f)
                {
                    posCurrentState = PosState.Done;
                }
                break;
            case PosState.Done:
                _positionLerpCompletion = 0.0f;
                posCurrentState = PosState.Wait;
                break;
        }
    }

    void EnterAnimation()
    {
        posCurrentState = PosState.Slerping;
    }

    public void ActivateInitialCharacterUI()
    {
        _positionLerpCompletion = 0.0f;
        _alphaLerpCompletion = 0.0f;
        ShowImage(true);
        EnterAnimation();
    }

    public void DeActivateInitialCharacterUI()
    {
        _positionLerpCompletion = 0.0f;
        _alphaLerpCompletion = 0.0f;
        HideImage(false);
    }
}
