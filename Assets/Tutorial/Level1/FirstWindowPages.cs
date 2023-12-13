using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstWindowPages : MonoBehaviour
{
    [SerializeField] private GameObject[] _pages;
    private int _pagesCount;
    private int _currentPage = 1;

    // Start is called before the first frame update
    void Start()
    {
        _pagesCount = _pages.Length;
        DisableAllPages();
        ShowPage(_currentPage);
    }

    public void NextPage()
    {
        if(_currentPage < _pagesCount)
        {
            _currentPage++;
        } 
        else if (_currentPage == _pagesCount)
        {
            _currentPage = 1;
        }
        DisableAllPages();
        ShowPage(_currentPage);
    }

    private void DisableAllPages()
    {
        foreach (var page in _pages)
        {
            page.gameObject.SetActive(false);
        }
    }

    private void ShowPage(int number)
    {
        if(_pages[number-1] != null)
        {
            _pages[number - 1].gameObject.SetActive(true);
        }
    }
}
