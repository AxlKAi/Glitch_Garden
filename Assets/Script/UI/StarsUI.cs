using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StarsUI : MonoBehaviour
{
    TextMeshProUGUI starsUI_GUI;
    // Start is called before the first frame update
    void Start()
    {
        starsUI_GUI = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayStars(int stars)
    {
        starsUI_GUI.text = stars.ToString();
    }
}
