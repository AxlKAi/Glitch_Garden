using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionSwitchContentUnit : MonoBehaviour
{
    private RectTransform rectTransform;
    private float whidth;
    private Coroutine currentAnimationCoroutine;
    private bool isAnimationActive = false;
    [SerializeField] private float slideSpeed = .5f;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        whidth = rectTransform.rect.size.x;
    }

    private void SetPropirtiesForAnimationStart()
    {
        if (currentAnimationCoroutine != null)
        {
            StopCoroutine(currentAnimationCoroutine);
        }
        isAnimationActive = true;
    }

    public void MoveFromCentorToLeft()
    {
        SetPropirtiesForAnimationStart();        
        currentAnimationCoroutine = StartCoroutine(MoveFromCentorToLeftAnimation());     
    }

    IEnumerator MoveFromCentorToLeftAnimation()
    {
        rectTransform.anchoredPosition = new Vector2(0f, 0f);
        while (isAnimationActive)
        {
            yield return null;
            rectTransform.anchoredPosition += new Vector2(slideSpeed, 0f);
            float currPosition = rectTransform.anchoredPosition.x;
            if (currPosition > whidth)
            {
                isAnimationActive = false;
            }
               
        }
    }

    public void MoveFromCentorToRight()
    {
        SetPropirtiesForAnimationStart();
        currentAnimationCoroutine = StartCoroutine(MoveFromCentorToRightAnimation());
    }

    IEnumerator MoveFromCentorToRightAnimation()
    {
        rectTransform.anchoredPosition = new Vector2(0f, 0f);
        while (isAnimationActive)
        {
            yield return null;
            rectTransform.anchoredPosition -= new Vector2(slideSpeed, 0f);
            float currPosition = rectTransform.anchoredPosition.x;
            if (currPosition > whidth)
            {
                isAnimationActive = false;
            }
        }
    }

    public void MoveFromLeftBorderToCenter()
    {
        SetPropirtiesForAnimationStart();
        currentAnimationCoroutine = StartCoroutine(MoveFromLeftBorderToCenterAnimation());
    }

    IEnumerator MoveFromLeftBorderToCenterAnimation()
    {
        rectTransform.anchoredPosition = new Vector2(whidth, 0f);
        while (isAnimationActive)
        {
            yield return null;
            rectTransform.anchoredPosition -= new Vector2(slideSpeed, 0f);
            float currPosition = rectTransform.anchoredPosition.x;
            if (currPosition <= 0)
            {
                isAnimationActive = false;
            }
        }
    }
    public void MoveFromRightBorderToCenter()
    {
        SetPropirtiesForAnimationStart();
        currentAnimationCoroutine = StartCoroutine(MoveFromRightBorderToCenterAnimation());
    }

    IEnumerator MoveFromRightBorderToCenterAnimation()
    {
        rectTransform.anchoredPosition = new Vector2(-whidth, 0f);
        while (isAnimationActive)
        {
            yield return null;
            rectTransform.anchoredPosition += new Vector2(slideSpeed, 0f);
            float currPosition = rectTransform.anchoredPosition.x;
            if (currPosition >= 0)
            {
                isAnimationActive = false;
                Debug.Log("arrived");
            }
        }
    }

    public void HideToRightBorder()
    {
        rectTransform.anchoredPosition = new Vector2(-whidth, 0f);
    }

    public void HideToLeftBorder()
    {
        rectTransform.anchoredPosition = new Vector2(whidth, 0f);
    }

    public void SetCenterPosition()
    {
        rectTransform.anchoredPosition = new Vector2(0f , 0f);
    }
}
