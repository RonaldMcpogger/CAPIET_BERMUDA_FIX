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

    bool flashOn = false;

    bool displayOn = false; // for coordinates display

    public bool inSub = false; //if player is in sub

    [SerializeField] public GameObject coordinates;

    float BatteryLife = 100f;
    public float drainDelay = 2f;
    float drainRate = 5f;
    float OxygenLife = 100f;


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
                            if(BatteryLife <= 0f) // battery dead
                            {
                                return;
                            }
                            // insert state condition for flashlight
                           else if (headLight.intensity != 1.3f)
                            {
                                flashOn = true;
                                headLight.intensity = 1.3f;
                            }
                            else
                            {
                                flashOn = false;
                                headLight.intensity = 0f;
                            }

                        }
                        else if (leftHeld.itemID == 201)
                        {
                            Debug.Log("Used left item: " + leftHeld.itemName);

                        }
                       
                        else if (leftHeld.itemID == 300 ) // coordinates
                           {
                            if (displayOn == false)
                            {
                                coordinates.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "X: 345 Y: 375";
                                displayOn = true;
                            }
                            else
                            {
                                coordinates.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "";
                                displayOn = false;
                            }
                        }
                         else if(leftHeld.itemID == 301) // map
                            {
                            if (displayOn == false)
                            {
                                coordinates.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "X: 540 Y: 284";
                                displayOn = true;
                            }
                            else
                            {
                                coordinates.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "";
                                displayOn = false;
                            }
                        }
                        
                         
                    }
                    else //revamp to accept a thing inside sub
                    {
                        if (leftHeld.itemID == 100) // battery
                        {
                            Debug.Log("Used left item: " + leftHeld.itemName);
                            BatteryLife += 25f;


                        }
                        else if (leftHeld.itemID == 101) // pills
                        {
                            Debug.Log("Used left item: " + leftHeld.itemName);
                            OxygenLife += 25f;

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
                            if (BatteryLife <= 0f) // battery dead
                            {
                                return;
                            }
                            // insert state condition for flashlight
                            else if (headLight.intensity != 1.3f)
                            {
                                flashOn = true;
                                headLight.intensity = 1.3f;
                            }
                            else
                            {
                                flashOn = false;
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
                            BatteryLife += 25f;



                        }
                        else if (Rightheld.itemID == 101) // pills
                        {
                            Debug.Log("Used left item: " + Rightheld.itemName);
                            OxygenLife += 25f;

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

    public float getOxygen()
    {
        return OxygenLife;
    }


public float getBattLife()
    {
        return BatteryLife;
    }
    void Update()
    {
        if(flashOn)
        {
            BatteryLife -= drainRate * Time.deltaTime;
            if(BatteryLife <= 0f)
            {
                flashOn = false;
                headLight.intensity = 0f;
            }
        }
        if(inSub == false)
        {
            OxygenLife -= (drainRate / drainDelay) * Time.deltaTime;
        }
    }
   
   public void depositItems() // check hand if metal 
    {

    }
}
