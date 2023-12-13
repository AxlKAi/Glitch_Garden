using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    Transform bar;
    bool isFirstHit = true;

    // Start is called before the first frame update
    void Start()
    {
        bar = transform.Find("Bar");
    }

    public void SetSize(float sizeNormalized)
    {
        if(isFirstHit)
        {
            ShowBar();
        }
        bar.localScale = new Vector3(sizeNormalized, 1f);
    }

    void ShowBar()
    {
        foreach(Transform t in transform)
        {
            t.gameObject.SetActive(true);
        }
        gameObject.SetActive(true);
        isFirstHit = false;
    }

    public void HideBar()
    {
        foreach (Transform t in transform)
        {
            gameObject.SetActive(false);
        }
        isFirstHit = true;
    }
}
