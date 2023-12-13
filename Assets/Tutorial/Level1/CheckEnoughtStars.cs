using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnoughtStars : MonoBehaviour
{
    [SerializeField] private GameObject _rootGameObject;
    private GameState gameState;

    // Start is called before the first frame update
    void Start()
    {
        gameState = FindObjectOfType<GameState>();
        if (!gameState.IsEnoughStars(100))
        {
            if (_rootGameObject != null)
                _rootGameObject.SetActive(false);
        }
    }


}
