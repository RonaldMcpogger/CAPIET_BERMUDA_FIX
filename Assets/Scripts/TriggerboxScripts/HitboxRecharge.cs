using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HitboxRecharge : MonoBehaviour, HitboxScript
{

    [SerializeField] GameObject player;

    [SerializeField] private TMP_Text ShipIntegrityDisplay;
    
    // Start is called before the first frame update
    void Start()
    {
        ShipIntegrityDisplay = GameObject.Find("ShipIntegrityDisplay").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        ShipIntegrityDisplay.text = "Ship Integrity: " + ItemManager.Instance.getShipIntegrity() + "%";

    }

    public void entered()
    {
        if(ItemManager.Instance.checkHand())
        {
            player.GetComponentInChildren<HitboxUI>().setUIActive("Fix ship");

        }
        else
        {
            player.GetComponentInChildren<HitboxUI>().setUIActive("Recharge Systems");

        }
    }

    public void exited()
    {
        player.GetComponentInChildren<HitboxUI>().setUIInactive();
    }

    public void interact()
    {
       if(ItemManager.Instance.checkHand()) // checks if hand has resources
        {
           
            int leftId = ItemManager.Instance.depositItems(0); //check lefty
           
            
                switch (leftId)////////////////////////////// case1: rocks, case2: metal, case3: components
                {
                    case 1:
                       ItemManager.Instance.updateShipIntegrity(5);
                    break;
                    case 2:
                    ItemManager.Instance.updateShipIntegrity(15);
                    break;
                    case 3:
                    ItemManager.Instance.updateShipIntegrity(25);
                    break;
                }
            int rightId = ItemManager.Instance.depositItems(1); //check righty
            switch (rightId)
                {
                case 1:
                    ItemManager.Instance.updateShipIntegrity(5);
                    break;
                case 2:
                    ItemManager.Instance.updateShipIntegrity(15);
                    break;
                case 3:
                    ItemManager.Instance.updateShipIntegrity(25);
                    break;
            }
            
        }
       else
        {
            if(ItemManager.Instance.getShipIntegrity() >=77.5f)
            {
               ItemManager.Instance.rechargeAll(100);
                ItemManager.Instance.updateShipIntegrity(-33);

            }
            else if(ItemManager.Instance.getShipIntegrity() >= 44f)
            {
                ItemManager.Instance.rechargeAll(55);
                ItemManager.Instance.updateShipIntegrity(-33);

            }
            else if(ItemManager.Instance.getShipIntegrity() >= 11f)
            {
                ItemManager.Instance.rechargeAll(25);
                ItemManager.Instance.updateShipIntegrity(-33);

            }
        }
    }
}
