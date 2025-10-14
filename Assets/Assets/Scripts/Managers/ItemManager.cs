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

    public void addItem(ItemScriptables item)
    {
        itemScriptables.Add(item);
    }

    public void removeItem(ItemScriptables item)
    {
        itemScriptables.Remove(item);
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

}
