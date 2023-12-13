using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionSwithContent : MonoBehaviour
{
    [SerializeField] private OptionSwitchContentUnit[] _optionSwitchContentUnit;
    [SerializeField] private int _startMenuPositionNumber = 0;
    [SerializeField] public UnityEngine.Events.UnityEvent onChange;

    private int _currentMenuNumber=0;
    public int CurrentMenuPosition { get { return _currentMenuNumber; } set { } }
    private int _maxMenuNumber = 0; 
    // Start is called before the first frame update
    void Start()
    {
        _maxMenuNumber = _optionSwitchContentUnit.Length - 1;
        if (_maxMenuNumber < 0)
            Debug.LogError("Add OptionSwitchContentUnit to Content");

        _currentMenuNumber = SetPositionIndex(_startMenuPositionNumber);
        ActivateNumeredChildInContent(_currentMenuNumber);
    }

    private int SetPositionIndex(int position)
    {
        if (position >= _maxMenuNumber)
            _currentMenuNumber = _maxMenuNumber;
        else if (position <= 0)
            _currentMenuNumber = 0;
        else
            _currentMenuNumber = position;

        return _currentMenuNumber;
    }

    public void SetActivePosition(int position)
    {
        ActivateNumeredChildInContent(SetPositionIndex(position));
    }

    private void ActivateNumeredChildInContent(int menuPosition)
    {
        if (_optionSwitchContentUnit.Length < 1)
            return;

        for (int i = 0; i <= _maxMenuNumber; i++)
        {
            if (i < menuPosition)
            {
                _optionSwitchContentUnit[i].HideToLeftBorder();
            }                
            else if(i==menuPosition)
            {
                _optionSwitchContentUnit[i].SetCenterPosition();
            }                
            else
            {
                _optionSwitchContentUnit[i].HideToRightBorder();
            }                
        }
    }

    public void SwitchLeft()
    {
        if (_currentMenuNumber > 0)
        {
            _optionSwitchContentUnit[_currentMenuNumber].MoveFromCentorToLeft();
            _currentMenuNumber--;            
            _optionSwitchContentUnit[_currentMenuNumber].MoveFromRightBorderToCenter();
            onChange?.Invoke();
        }
    }

    public void SwitchRight()
    {
        if (_currentMenuNumber < _maxMenuNumber)
        {
            _optionSwitchContentUnit[_currentMenuNumber].MoveFromCentorToRight();
            _currentMenuNumber++;           
            _optionSwitchContentUnit[_currentMenuNumber].MoveFromLeftBorderToCenter();
            onChange?.Invoke();
        }
    }
}
