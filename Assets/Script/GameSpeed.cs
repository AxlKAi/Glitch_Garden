using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSpeed : MonoBehaviour
{
    [SerializeField]
    private NormalSpeedButtonUI _normalSpeedButtonUI;
    [SerializeField]
    private DoubleSpeedButtonUI _doubleSpeedButtonUI;

    private int maxGameSpeed = 16;

    public void SetNormalGameSpeed()
    {
        Time.timeScale = 1;
        _normalSpeedButtonUI.gameObject.GetComponent<Image>().color = Color.red;
    }

    public void SetDoubleGameSpeed()
    {
        if ((int)Time.timeScale < maxGameSpeed)
            Time.timeScale = Time.timeScale * 2;
    }

    public void SetZeroGameSpeed()
    {
        Time.timeScale = 0;
    }

    private void Start()
    {
        _normalSpeedButtonUI = FindObjectOfType<NormalSpeedButtonUI>();
        _doubleSpeedButtonUI = FindObjectOfType<DoubleSpeedButtonUI>();
    }
}
