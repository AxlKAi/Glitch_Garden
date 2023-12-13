using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;


public class LevelMapsScroolButtons : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private bool _scroolLeft;
    [SerializeField] private GameObject _scrollViewContent;
    [SerializeField] private float _scroolNormalizedStep = 0.2f;
    [SerializeField] private float _scroolSpeed = .4f;

    private ScrollRect _scrollRect;
    private float _minNormalizedPosition = 0;
    private float _maxNormalizedPosition = 1;

    private void Start()
    {
        _scrollRect = _scrollViewContent.GetComponent<ScrollRect>();    
    }

    public void OnPointerClick(PointerEventData eventData)
    {
       if(_scroolLeft)
        {
            Slide(-_scroolNormalizedStep);
        }
        else
        {
            Slide(_scroolNormalizedStep);
        }
    }

    private void Slide(float delta)
    {
        float nextPosition = Mathf.Clamp(
                    _scrollRect.horizontalNormalizedPosition + delta,
                    _minNormalizedPosition,
                    _maxNormalizedPosition);

        _scrollRect.DOHorizontalNormalizedPos(nextPosition, _scroolSpeed);
    }
}
