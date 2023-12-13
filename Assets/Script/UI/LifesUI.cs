using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LifesUI : MonoBehaviour
{
    TextMeshProUGUI lifesUI_GUI;
    // Start is called before the first frame update
    void Start()
    {
        lifesUI_GUI = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayLifes(int lifes)
    {
        lifesUI_GUI.text = lifes.ToString();
    }
}
