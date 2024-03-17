using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class LifesUI : MonoBehaviour
{
    [SerializeField]
    private LifesMinusEffect _decreaseEffect;

    [SerializeField]
    private string _decreasePointSFX = "lose_point";

    TextMeshProUGUI lifesUI_GUI;

    void Start()
    {
        lifesUI_GUI = GetComponent<TextMeshProUGUI>();
    }

    public void DisplayLifes(int lifes)
    {
        lifesUI_GUI.text = lifes.ToString();
    }

    public void SpawnDecreaseEffect()
    {
        LifesMinusEffect effect = Instantiate(_decreaseEffect, transform.position, Quaternion.identity) as LifesMinusEffect;
        effect.transform.parent = transform;
        StartCoroutine(DestroyEffect(effect));

        ShowScaleupEffect();
        AudioManager.Instance.PlaySFX(_decreasePointSFX);
    }

    private IEnumerator DestroyEffect(LifesMinusEffect effect)
    {
        yield return new WaitForSeconds(effect.Duration);
        Destroy(effect.gameObject);
    }

    private void ShowScaleupEffect()
    {
        var sequence = DOTween.Sequence()
            .Append(transform.DOScale(1.2f, .1f))
            .Append(transform.DOScale(1.1f, .1f))
            .Append(transform.DOScale(1.2f, .1f))
            .Append(transform.DOScale(1.1f, .1f))
            .Append(transform.DOScale(1f, .1f));
    }
}
