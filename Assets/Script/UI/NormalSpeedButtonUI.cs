using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalSpeedButtonUI : MonoBehaviour
{
    public void SetNormalSpeed()
    {
        GameState._instance.GameSpeed.SetNormalGameSpeed();
        AudioManager.Instance.PlaySFX("UI_Click");
    }
}
