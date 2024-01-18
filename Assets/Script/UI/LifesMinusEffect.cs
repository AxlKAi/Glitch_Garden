using UnityEngine;
using DG.Tweening;

public class LifesMinusEffect : MonoBehaviour
{
    [SerializeField]
    private Transform[] _flyingCurve;
    [SerializeField]
    private float _duration = 1f;

    private Vector3[] _flyingCurvePoints;

    public float Duration { get { return _duration; } }

    void Start()
    {
        _flyingCurvePoints = new Vector3[_flyingCurve.Length];

        for (int i = 0; i < _flyingCurve.Length; i++) {
            _flyingCurvePoints[i] = _flyingCurve[i].position;
        }

        var sequence = DOTween.Sequence()
            .Append(transform.DOLocalPath(_flyingCurvePoints, _duration, PathType.CatmullRom, PathMode.Sidescroller2D, 5, Color.red))
            .Join(transform.DOScale(new Vector3(2f, 2f, 2f), _duration));
    }
}