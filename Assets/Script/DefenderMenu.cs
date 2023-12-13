using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderMenu : MonoBehaviour
{
    [SerializeField] Color deSelectedButtonCollor = new Color32 (58,58,58,255);
    [SerializeField] Color selectedButtonCollor = new Color32(230, 230, 230, 255);
    [SerializeField] Defender defenderPref;
    MouseCursor buidlMouseCursor;

    DefenderBuildZone coreGameArea;
    GameState gameState;

    private bool isDisabled = false;
    public bool IsDisabled
    {
        get
        {
            return isDisabled;
        }

        set
        {
            isDisabled = value;
        }
    }

    private void Start()
    {
        coreGameArea = FindObjectOfType<DefenderBuildZone>();
        if (defenderPref==null)
        {
            Debug.LogError("Please, add defender pref to button" + name);
        }
        buidlMouseCursor = transform.root.GetComponentInChildren<MouseCursor>();
        gameState = GameObject.FindObjectOfType<GameState>();
        gameState.BuildModeOffEvent += MakeAllButtonsDeSelected;
    }

    private void Update()
    {

    }

    private void OnMouseDown()
    {
        if(!isDisabled)
            DefenderButtonClicked();
    }

    public void DefenderButtonClicked()
    {
        MakeAllButtonsDeSelected();
        GetComponent<SpriteRenderer>().color = selectedButtonCollor;
        coreGameArea.SetSpawnableDefender(defenderPref);
        gameState.BuildModeOn(defenderPref);
    }


    private void MakeAllButtonsDeSelected()
    {
        GameObject parrentObj = transform.parent.gameObject;
       
        if (parrentObj.name == null)
        {
            Debug.LogError("Somthing wrong in bottom menu structure. Buttons must be child to Button game object");
            return;
        }

        foreach (Transform child in parrentObj.transform)
        {
            child.GetComponent<SpriteRenderer>().color = deSelectedButtonCollor;
        }
    }

}
