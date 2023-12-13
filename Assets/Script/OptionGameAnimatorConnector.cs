using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class OptionGameAnimatorConnector : MonoBehaviour
{
    private OptionsGameScreen optionGameScreen;
    private bool isControllerOnRoot = false;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        var rootGameObject = transform.parent;
        isControllerOnRoot = rootGameObject.TryGetComponent<OptionsGameScreen>(out optionGameScreen);
        if (!isControllerOnRoot)
            Debug.LogWarning("I expect OptionScreenController on parrent component ");

        animator = GetComponent<Animator>();            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndOfShowAnimation()
    {
        if (isControllerOnRoot)
            optionGameScreen.ActivateOptionWindowInvoke();
    }

    public void StartOfCloseAnimation()
    {
        animator.SetBool("CloseAnimationState", true);

        if (isControllerOnRoot)
            optionGameScreen.SetGameSpeedNormal();
    }

    public void EndOfCloseAnimation()
    {
        if (isControllerOnRoot)
            optionGameScreen.DeactivateOptionWindowInvoke();
    }
}
