using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableVFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem _pickableVFX;
    [SerializeField] private float _pickableVFXspawnDelay = 0.05f;
    [SerializeField] private float _pickableVFXLifeTime = 1f;

    [Tooltip ("Default name is LevelVFX")]
    [SerializeField] private GameObject _pickableVFXRootGO;

    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.Find("LevelVFX");
        if (go)
        {
            _pickableVFXRootGO = go;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowPickableVFX()
    {
        StartCoroutine(SpawnPickableVFX());
    }

    private IEnumerator SpawnPickableVFX()
    {
        while (true)
        {
            var vfx = Instantiate(_pickableVFX.gameObject, gameObject.transform.position, Quaternion.identity);
            vfx.gameObject.transform.parent = _pickableVFXRootGO.transform;
            vfx.gameObject.SetActive(true);
            vfx.gameObject.GetComponent<ParticleSystem>().Play();
            GameObject.Destroy(vfx, _pickableVFXLifeTime);
            yield return new WaitForSeconds(_pickableVFXspawnDelay);
        }
    }
}
