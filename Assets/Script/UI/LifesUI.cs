using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LifesUI : MonoBehaviour
{
    [SerializeField]
    private LifesMinusEffect _decreaseEffect;

    TextMeshProUGUI lifesUI_GUI;

    void Start()
    {
        lifesUI_GUI = GetComponent<TextMeshProUGUI>();
        StartCoroutine(SpawnRepeat());
    }

    public void DisplayLifes(int lifes)
    {
        lifesUI_GUI.text = lifes.ToString();
    }

    private void SpawnDecreaseEffect()
    {
        LifesMinusEffect effect = Instantiate(_decreaseEffect, transform.position, Quaternion.identity) as LifesMinusEffect;
        effect.transform.parent = transform;
        StartCoroutine(DestroyEffect(effect));
    }

    private IEnumerator DestroyEffect(LifesMinusEffect effect)
    {
        yield return new WaitForSeconds(effect.Duration);
        Destroy(effect.gameObject);
    }

    //TODO delet it
    private IEnumerator SpawnRepeat()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            SpawnDecreaseEffect();
        }
    }
}
