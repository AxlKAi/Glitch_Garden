using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarItem : MonoBehaviour
{
    [SerializeField] private int starsAmmount = 100;
    private GameState gameState;
    
    [SerializeField]
    private float starTTL = 9f;

    private bool isPicked;
    [SerializeField] private float _starsPuttingSpeed = 7f;

    [Tooltip ("position of stars UI")]
    [SerializeField]
    private Vector3 _endPointPosition;
    private PickableVFX _pickableVFX;

    [SerializeField]
    private string _catchSFX = "catch_stars";

    // Start is called before the first frame update
    void Start()
    {
        gameState = FindObjectOfType<GameState>();
        StartCoroutine(DestroyStar(starTTL));

        GameObject go = GameObject.Find("StarsUI");
        if (go)
        {
            _endPointPosition = go.gameObject.transform.position;
        }

        _pickableVFX = GetComponent<PickableVFX>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPicked)
        {
            MoveToPacket();
        }        
    }

    public void CatchStar()
    {
        if(!isPicked)
        {
            PickStar();
        }
    }

    public void SetStarsAmmount(int stars)
    {
        starsAmmount = stars;
    }

    public void SetStarTTL(float time)
    {
        starTTL = time;
    }

    private IEnumerator DestroyStar(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    private void PickStar()
    {
        StopAllCoroutines();
        gameState.AddStars(starsAmmount);
        isPicked = true;
        AudioManager.Instance.PlaySFX(_catchSFX);
        _pickableVFX.ShowPickableVFX();
    }

    private void MoveToPacket()
    {
        transform.position = Vector3.MoveTowards(transform.position, _endPointPosition, _starsPuttingSpeed / 100);
        float _distanceToEnd = (transform.position - _endPointPosition).magnitude;
        if (_distanceToEnd <= .1)
        {
            Destroy(gameObject);
        }
    }


}
