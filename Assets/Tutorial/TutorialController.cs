using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private TutorialElement[] _tutorialElements;
    [SerializeField] private GameObject _tutorialUIroot;
    [SerializeField] private UnityEvent _globalUIwindowActive;
    [SerializeField] private UnityEvent _globalUIwindowDeactivate;

    private int _elementsCount;
    private int _currentTutorialElement;
    private bool _startNexTutorial = false;

    private float difficultyCoefficient = 1f;
    private GameState gameState;

    private bool _isTutorialActive = true; 

    // Tuturial елементы появляются по след. принципу, если появился модальный элемент, то следующий спавнится только после его уничтожения, а если нет, то по таймеру

    // Start is called before the first frame update
    void Start()
    {
        gameState = FindObjectOfType<GameState>();
        if (GameState.Difficulty_level == 0)
            difficultyCoefficient = 1.3f;
        else if (GameState.Difficulty_level == 1)
            difficultyCoefficient = 1f;
        else
            difficultyCoefficient = .9f;

        _globalUIwindowActive.AddListener(OnActivateModalWindow);
        _globalUIwindowDeactivate.AddListener(OnDeactivateModalWindow);

        _elementsCount = _tutorialElements.Length;
        _currentTutorialElement = 0;
        CreateTuturialElement();

        LoadIsTutorialActiveFromPrefs();
    }

    private void LoadIsTutorialActiveFromPrefs() 
    {
        _isTutorialActive = PlayerPrefsController.GetIsTutorialActive();
    }

    private void CreateTuturialElement()
    {
        _startNexTutorial = false;

        if (_currentTutorialElement < _elementsCount)
        {
            if (_tutorialElements[_currentTutorialElement] != null)
            {
                StartCoroutine(ShowUIelement(_tutorialElements[_currentTutorialElement]));
                _currentTutorialElement++;
            }
        }
    }

    public void FixedUpdate()
    {
        if (_startNexTutorial)
        {
            _startNexTutorial = false; 
            CreateTuturialElement();
        }
    }

    IEnumerator ShowUIelement(TutorialElement element)
    {
                
        yield return new WaitForSeconds(element.Timer*difficultyCoefficient);

        if (element.IsModal)
            if (_globalUIwindowActive != null)
                _globalUIwindowActive.Invoke();

        var elementUI = Instantiate(element.TutorialUIelement, element.TutorialUIelement.transform.position, Quaternion.identity) as GameObject;
        elementUI.transform.SetParent(_tutorialUIroot.transform);
        if (!PlayerPrefsController.GetIsTutorialActive() && !element.IsModal)
            elementUI.SetActive(false);
        RectTransform rectTransform; 
        elementUI.transform.TryGetComponent<RectTransform>(out rectTransform);
        rectTransform.localPosition = new Vector3(0, 0, 0);
        rectTransform.localScale = new Vector3(1, 1, 1);

        if (!element.IsModal)
        {
            _startNexTutorial = true;
        }

        if (element.DestroyDelay != 0)
        {
            StartCoroutine(DestroyTutorialElementAfterDelay(element, elementUI));
        }
    }

    private void OnActivateModalWindow()
    {

    }
    private void OnDeactivateModalWindow()
    {

    }

    public void GlobalUIwindowDeactivateEvent()
    {
        _globalUIwindowDeactivate.Invoke();
        _startNexTutorial = true;
    }

    private IEnumerator DestroyTutorialElementAfterDelay(TutorialElement tutorialElement, GameObject sceneElement)
    {
        yield return new WaitForSeconds(tutorialElement.DestroyDelay);
        if(tutorialElement.IsModal)
        {
            _globalUIwindowDeactivate.Invoke();
            _startNexTutorial = true;
        }
        Destroy(sceneElement);
    }
}
