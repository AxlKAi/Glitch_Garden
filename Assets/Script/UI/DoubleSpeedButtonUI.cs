using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleSpeedButtonUI : MonoBehaviour
{
    public void SetDoubleSpeed()
    {
        GameState._instance.GameSpeed.SetDoubleGameSpeed();
        AudioManager.Instance.PlaySFX("UI_Click");
    }
}
