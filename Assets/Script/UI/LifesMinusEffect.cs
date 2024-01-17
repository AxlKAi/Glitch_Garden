using UnityEngine;
using DG.Tweening;


public class LifesMinusEffect : MonoBehaviour
{
    [SerializeField]
    private Transform[] _flyingCurve;

    private Vector3[] _flyingCurvePoints;
    private Vector3 _startPositoin;


    // Start is called before the first frame update
    void Start()
    {
        _startPositoin = transform.position;
        // transform.localScale = new Vector3(.1f,.1f,.1f);

        _flyingCurvePoints = new Vector3[_flyingCurve.Length];

        for (int i = 0; i < _flyingCurve.Length; i++)
            _flyingCurvePoints[i] = _flyingCurve[i].position;

        var sequence = DOTween.Sequence()
       .Append(transform.DOPath(_flyingCurvePoints, 5f, PathType.CatmullRom, PathMode.Sidescroller2D, 10, Color.red))
       .Join(transform.DOScale(new Vector3(5f, 5f, 5f), 1f));
        sequence.SetLoops(-1, LoopType.Yoyo);

        //transform.DOPath(_flyingCurvePoints, 5f, PathType.CatmullRom, PathMode.Sidescroller2D, 10, Color.red);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
