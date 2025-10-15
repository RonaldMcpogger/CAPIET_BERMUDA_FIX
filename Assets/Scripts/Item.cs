using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Item : MonoBehaviour
{
    [SerializeField] int id;
    [SerializeField] ItemScriptables ItemData;
    public SpriteRenderer spriteRenderer;


    // Start is called before the first frame update

    void awake()
    {
        if (ItemData == null)
        {
            Debug.LogError("ItemData is not assigned in the inspector for " + gameObject.name);
        }
     
    }

    void Start()
    {
        id = ItemData.itemID;
        spriteRenderer.sprite = ItemData.itemIcon;
        Debug.Log("Item ID: " + ItemData.itemID + " Name: " + ItemData.itemName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getID()
    {
        return id;
    }

    public ItemScriptables getItemData()
    {
        return ItemData;
    }
}
