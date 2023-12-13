using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Spell> inventory;
    [SerializeField] private int maxItemsCount = 5;
    private GameState gameState;
    private Transform[] inventorySlot;
    private SpellBarButton selectedItem;
    [SerializeField] private GameObject inventoryPacket;

    // Start is called before the first frame update
    void Start()
    {
        gameState = FindObjectOfType<GameState>();
        inventorySlot = new Transform[maxItemsCount];
        inventory = new List<Spell>();

        int i = 0;
        foreach(Transform child in transform)
        {
            inventorySlot[i] = child;
            DrawInventoryPacket(child.transform);
            i++;
            if(i==maxItemsCount) { break; }
        }
        
        //TODO add spell at start
        //this.AddItem(1);
        //this.AddItem(2);
    }

    public void AddItem(int spell_id)
    {
        if(inventory.Count == maxItemsCount)
        {
            Debug.LogWarning("Not enought space in inventory");
            return;
        }
        inventory.Add( gameState.GetSpell(spell_id) );
        DrawInventory();
    }

    public Spell GetItem(int number)
    {
        Spell spell;

        if (inventory.Count == 0)
            return null;

        if ( number < inventory.Count && inventory[number] != null)
        {
            return inventory[number];
        } 
        else
        {
            return null;
        }
    }
    public void DrawInventory()
    {
        for(int i=0; i<maxItemsCount; i++)
        {
            Spell spell = GetItem(i);
             
            if ( spell != null )
            {
                inventorySlot[i].GetComponent<SpriteRenderer>().sprite = spell.icon;
                inventorySlot[i].GetComponent<SpellBarButton>().SetSpellId(spell.id);
                inventorySlot[i].gameObject.SetActive(true);                
            } 
            else
            {
                inventorySlot[i].GetComponent<SpriteRenderer>().sprite = null;
                inventorySlot[i].gameObject.SetActive(false);
            }
        }
    }

    private void DrawInventoryPacket(Transform slotTransform)
    {
        var go = Instantiate(inventoryPacket, slotTransform.position, Quaternion.identity);
        go.transform.parent = slotTransform.root;
        go.gameObject.SetActive(true);
    }

    public void SetSelectedItem(SpellBarButton item)
    {
        selectedItem = item;
    }

    public void DeleteSelectedItem()
    {
        if(selectedItem)
        {
            inventory.RemoveAt(selectedItem.GetItemIndex());
            DrawInventory();
        }
    }

    public bool IsInventoryFull()
    {
        return inventory.Count == maxItemsCount;
    }

    public Vector3 GetFreeSlotPosition()
    {
        int i = 0;
        Spell spell;

        do
        {
            spell = GetItem(i);

            if ( spell == null )
            {
                Vector3 _pos = inventorySlot[i].position;

                return _pos;
            }
            i++;
        } while (i < maxItemsCount) ;

        return gameObject.transform.position;                
    }
}
