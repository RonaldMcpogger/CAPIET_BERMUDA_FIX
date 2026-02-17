using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    /// <summary>
    /// RAM NOTE: if free time, implement the Interface functions so that its not cluttered in item manager

    /// </summary>

    // Start is called before the first frame update

  




    [SerializeField] private ItemScriptables leftHeld, Rightheld;


    [SerializeField] public float BatteryLife = 100f;
    [SerializeField] float OxygenLife = 100f;
    [SerializeField] private float shipIntegrity;
    // public float drainDelay = 1f;

    [Tooltip("Rate at which battery and oxygen drain when in use or outside of sub, respectively")]
   [SerializeField] private float OxydrainRate = 1f;
   [SerializeField]  private float BattDrainRate = 3f;




    //Tools
    [SerializeField] private Light headLight;

    [SerializeField] public GameObject coordinates;

    [SerializeField] private GameObject scanFunc;


    bool flashOn = false;

    bool displayOn = false; // for coordinates display

    public bool inSub = false; //if player is in sub



    /// <summary>
    /// /SINGLETON
    /// </summary>
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
        //finding the necessary items

    }

    private  void Refresh()
    {
     
        if (headLight == null)
            {
                headLight = GameObject.Find("LightArea").GetComponent<Light>();
            }
            if (coordinates == null)
            {
                coordinates = GameObject.Find("Coords Display");
            }
            if (scanFunc == null)
            {
                scanFunc = GameObject.Find("ScanArea");
        }
    }
    
 

    private void Start()
    {
        Refresh();
        scanFunc.SetActive(false);



    }
   




    public void grabItem(ItemScriptables item, int hand)
    {
       switch(hand)
        {
            case 0:
                leftHeld = item;
                break;
            case 1:
                Rightheld = item;
                break;
        }
    }



    public void dropItem(int hand)
    {
        switch(hand)
        {
            case 0:
                leftHeld = null;
                break;
            case 1:
                Rightheld = null;
                break;
        }
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
                        if (leftHeld.itemID == 200) 
                        {
                            if(BatteryLife > 0f) // battery dead
                            {
                                 if (headLight.intensity != 1.3f)
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
                         

                        }
                        else if (leftHeld.itemID == 201 &&BatteryLife>24)
                        {
                            
                            scanFunc.SetActive(true);
                            scanFunc.GetComponent<Scanner>().StartScanning();

                        }
                       //mayhaps will create a seperate script later
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
                    else 
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

                    }
                   
                }
                break;
            case 1:
                if (Rightheld != null)
                {
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

                            scanFunc.SetActive(true);
                            scanFunc.GetComponent<Scanner>().StartScanning();
                        }
                        /////////////////////////////////////////////////////////////// COOOOOOOOOOOOOOORDS //////////////////////////////////////////////////////////////////////////////////
                        else if (Rightheld.itemID == 300) // coordinates
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
                        else if (Rightheld.itemID == 301) // map
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

public float getShipIntegrity()
    {
               return shipIntegrity;
    }
 public void updateShipIntegrity(float integrity)
    {
        shipIntegrity += integrity;
        shipIntegrity = Mathf.Clamp(shipIntegrity, 0f, 100f);
    }



    public ItemScriptables getItemInHand(int hand)
    {
        if (hand == 0)
        {
            return leftHeld;
        }
        else if (hand == 1)
        {
            return Rightheld;
        }
        else
        {
            return null;
        }
    }

    void Update()
    {
        Refresh();

        if (flashOn)
        {
            BatteryLife -= BattDrainRate * Time.deltaTime;
            if(BatteryLife <= 0f)
            {
                flashOn = false;
                headLight.intensity = 0f;
            }
        }
        if(inSub == false)
        {
            OxygenLife -= (OxydrainRate) * Time.deltaTime;
            HealthManager.Instance.setO2Status(false);

        }
        if (OxygenLife <= 0f) // if oxygen meter is now 0
        {
            OxygenLife = 0f;
            HealthManager.Instance.setO2Status(true); // set o2 damage status to true
        }
        else
        {
            HealthManager.Instance.setO2Status(false);
        }
    }
   public void DamageOxygen(float damage)
    {
        OxygenLife -= damage;
    }
    public void DamageBattery(float damage)
    {
        BatteryLife -= damage;
    }



    


    public int depositItems(int hand) // check hand if metal 
    {
        int tag;
        if(hand == 0 && leftHeld != null)
        {

             tag = leftHeld.itemID;
            leftHeld = null;


        }
        else if (hand == 1 && Rightheld != null)
        {
            tag = Rightheld.itemID;
            Rightheld = null;
        }
        else
        {
            tag= -1; // no item in hand
        }
        return tag;
    }

    public bool checkHand()
    {
        if(leftHeld != null ||Rightheld !=null)
        {
            if ((leftHeld.itemID >= 0 && leftHeld.itemID < 3) || (Rightheld.itemID >= 0 && Rightheld.itemID < 43))
            {
                Debug.Log("Hand has resources");
                return true;
            }
            else return false;
        }
        else return false;
    }


    public void rechargeAll(float value)
    {
               OxygenLife = value;
        BatteryLife = value;
    }
}
