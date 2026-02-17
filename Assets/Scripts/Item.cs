using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Item : MonoBehaviour
{
    [SerializeField] int id;
    [SerializeField] ItemScriptables ItemData;
    public bool is3D = false;
    public SpriteRenderer spriteRenderer;


    // Start is called before the first frame update

    
    

    void Start()
    {
        id = ItemData.itemID;
        if ((!is3D))
        {
            spriteRenderer.sprite = ItemData.itemIcon;
        }
        
    //    Debug.Log("Item ID: " + ItemData.itemID + " Name: " + ItemData.itemName);
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
