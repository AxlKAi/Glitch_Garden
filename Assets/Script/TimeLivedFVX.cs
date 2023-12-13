using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLivedFVX : MonoBehaviour
{
    [SerializeField] private float _timeLivedVFXLifeTime = 1f;

    [Tooltip("Default name is LevelVFX")]
    [SerializeField] private GameObject _timeLivedVFXRootGO;

    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.Find("LevelVFX");
        if (go)
        {
            _timeLivedVFXRootGO = go;
        }

        gameObject.transform.parent = go.transform;

        StartCoroutine(DestroyGO());
    }

    private IEnumerator DestroyGO()
    {
        yield return new WaitForSeconds(_timeLivedVFXLifeTime);

        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        GameObject.Destroy(gameObject);
    }
}
