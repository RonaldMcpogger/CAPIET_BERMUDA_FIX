using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public GameObject HandsUI;
    public GameObject InventoryUI;
    [SerializeField] public List<ItemScriptables> itemScriptables;
    [SerializeField] public ItemScriptables heldItem = null;
    [SerializeField] private Light headLight;
    public static ItemManager Instance { get; private set; }

    private void Awake()
    {

        // If an instance already exists and it's not this one, destroy this duplicate.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            // Otherwise, set this as the instance.
            Instance = this;
            // Optionally, make the object persistent across scene loads.
            DontDestroyOnLoad(this);
        }
    }

    public void addItem(ItemScriptables item) // add item to the inventory
    {
        itemScriptables.Add(item);
    }

    public void removeItem(ItemScriptables item)// remove item from inventory
    {
        itemScriptables.Remove(item);
    }


   public int FindItem(int ID)
    {
        for (int i = 0; i < itemScriptables.Count; i++)
        {
            if (itemScriptables[i].itemID == ID)
            {
               Debug.Log("Found item at index: " + i);
                return i;
            }
        }
        return -1;
    }

    public void setHeldItem(int index)
    {
        if(index < 0 || index >= itemScriptables.Count)
        {
            Debug.LogError("Index out of bounds");
            return;
        }
        else
            heldItem = itemScriptables[index];
    }

  public bool isConsumeable()
    {
        return heldItem.isConsumable;
    }

    public void useItem(int ID)
    { 
      

      if(heldItem.isUsable)
        {
            if (heldItem.isConsumable)
            {
                if(heldItem.itemID == 101)
                {
                    Debug.Log("Medkit");
                } 
                if(heldItem.itemID == 100)
                {
                    Debug.Log("Battery Tank");
                }
                //remove consumeable
            
            }
            else
            {
                if (heldItem.itemID == 200)
                {
                    headLight.intensity = 1.3f;
                    Debug.Log("Toggled Headlight");
                }
                if (heldItem.itemID == 201)
                {
                    Debug.Log("used sonar");
                }
            }
        }


    }

}
