using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_item : MonoBehaviour
{
    Spell spell;
    [SerializeField] int spell_id;
    private GameState gameState;
    private Inventory inventory;
    private bool isPicked;
    [SerializeField] private float _inventoryPuttingSpeed = .1f;
    private Vector3 _endPointPosition;
    private PickableVFX _pickableVFX;


    [SerializeField] float selfDestroyTime = 10f;

    // Start is called before the first frame update
    void Start()
    {
        gameState = FindObjectOfType<GameState>();
        inventory = gameState.GetInventory();
        spell = gameState.GetSpell(spell_id);
        GetComponent<SpriteRenderer>().sprite = spell.icon;
        StartCoroutine(DestroySpellItem());
        _pickableVFX = GetComponent<PickableVFX>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isPicked)
        {
            MoveToInventory();
        }
    }

    public bool TryToPickUpItem()
    {
        if(!inventory.IsInventoryFull() && !isPicked)
        {            
            isPicked = true;
            _endPointPosition = inventory.GetFreeSlotPosition();
            _pickableVFX.ShowPickableVFX();
            inventory.AddItem(spell_id);
            return true;
        } 
        else
        {
            return false;
        }
    }

    public void SetSpell_id(int spell_id)
    {
        this.spell_id = spell_id;
    }

    IEnumerator DestroySpellItem()
    {
        yield return new WaitForSeconds(selfDestroyTime);
        Destroy(gameObject);
    }

    public void SetDestroySpellTime(float delay)
    {
        selfDestroyTime = delay;
    }

    private void MoveToInventory()
    {
        transform.position = Vector3.MoveTowards(transform.position, _endPointPosition, _inventoryPuttingSpeed / 100);
        float _distanceToEnd = (transform.position - _endPointPosition).magnitude;
        if( _distanceToEnd<=.1 )
        {            
            Destroy(gameObject);
        }        
    }
}
