using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Image LeftUI;
    [SerializeField] Image RightUI;
    [SerializeField] public ItemScriptables heldItem = null;
    [Tooltip("Number of metals collected")]
    [SerializeField] private int subMetals;
    [Tooltip("Number of components collected")]
    [SerializeField] private int subComponents;
    [Tooltip("Number of Rocks collected")]
    [SerializeField] private int subRocks;

    //Tools
    [SerializeField] private Light headLight;

    private ItemScriptables leftHeld, Rightheld;
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


    public void grabItem(ItemScriptables item, int hand)
    {
        if (hand == 0) // left
        {
            LeftUI.sprite = item.itemIcon;
            leftHeld = item;
        }
        else if (hand == 1) // right
        {
            RightUI.sprite = item.itemIcon;
            Rightheld = item;
        }
    }



    public void dropItems()
    {
        LeftUI.sprite = null;
        RightUI.sprite = null;
        leftHeld = null;
        Rightheld = null;
    }
    public void useItem(int hand)
    {
        switch (hand)
        {
            case 0:
                if (leftHeld != null)
                {
                    Debug.Log("Used left item: " + leftHeld.itemName);
                    // Example logic for using an item
                    if (!leftHeld.isConsumable)
                    {
                        if (leftHeld.itemID == 200) // note try to make a meter for battery life 
                        {
                            // insert state condition for flashlight
                            if (headLight.intensity != 1.3f)
                            {
                                headLight.intensity = 1.3f;
                            }
                            else
                            {
                                headLight.intensity = 0f;
                            }

                        }
                        else if (leftHeld.itemID == 201)
                        {
                            Debug.Log("Used left item: " + leftHeld.itemName);

                        }
                    }
                    else
                    {
                        if (leftHeld.itemID == 100) // battery
                        {
                            Debug.Log("Used left item: " + leftHeld.itemName);


                        }
                        else if (leftHeld.itemID == 101) // pills
                        {
                            Debug.Log("Used left item: " + leftHeld.itemName);

                        }
                        leftHeld = null;
                        LeftUI.sprite = null;

                    }
                }
                break;
            case 1:
                if (Rightheld != null)
                {
                    Debug.Log("Used left item: " + Rightheld.itemName);
                    // Example logic for using an item
                    if (!Rightheld.isConsumable)
                    {
                        if (Rightheld.itemID == 200) // note try to make a meter for battery life 
                        {
                            if (headLight.intensity != 1.3f)
                            {
                                headLight.intensity = 1.3f;
                            }
                            else
                            {
                                headLight.intensity = 0f;
                            }
                        }
                        else if (Rightheld.itemID == 201)
                        {
                            Debug.Log("Used left item: " + Rightheld.itemName);

                        }
                    }
                    else
                    {
                        if (Rightheld.itemID == 100) // battery
                        {
                            Debug.Log("Used left item: " + Rightheld.itemName);



                        }
                        else if (Rightheld.itemID == 101) // pills
                        {
                            Debug.Log("Used left item: " + Rightheld.itemName);

                        }
                        Rightheld = null;
                        RightUI.sprite = null;
                    }
                }
                break;
        }




    }

    public bool isConsumeable(int hand)
    {
        if(hand == 0 && leftHeld != null)
        {
            return leftHeld.isConsumable;
        }
        else if (hand == 1 && Rightheld != null)
        {
            return Rightheld.isConsumable;
        }
        else
        {
            return false;
        }
    }
}
