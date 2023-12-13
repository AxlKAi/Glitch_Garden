using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="TutorialElement", menuName = "Tutorial/Tutorial Element", order = 51)]
public class TutorialElement : ScriptableObject
{
    [SerializeField] private GameObject _tutorialUIelement;
    [SerializeField] private float _timer;
    [SerializeField] private bool _isModal = false;
    [SerializeField] private float _destroyDelay;

    public float Timer => _timer;

    public GameObject TutorialUIelement => _tutorialUIelement;

    public bool IsModal => _isModal;

    public float DestroyDelay => _destroyDelay;
}
