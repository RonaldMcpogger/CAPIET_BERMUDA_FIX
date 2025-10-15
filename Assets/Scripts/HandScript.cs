using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class HandScript : MonoBehaviour
{
    //testing
    [SerializeField] Image LeftUI;
    [SerializeField] Image RightUI;
    CharacterController controller;
    bool validLeft;
    bool validRight;

    bool holdingLeft;
    bool holdingRight;

    int idLeft;
    int idRight;
    // Start is called before the first frame update
    void Start()
    {
        validLeft = false; validRight = false;
        holdingLeft = false; holdingRight = false;
        idLeft = -1;
        idRight = -1;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        //checks if hands are empty when looking at an item for ui updates
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2.0f, LayerMask.GetMask("Item")))
        {
            //Debug.Log(hit.transform.gameObject.name);
            if(!holdingLeft)
            {
                validLeft=true; 
            }
            if(!holdingRight)
            {
                validRight=true;
            }
        } else
        {
            validLeft = false;
            validRight = false;
        }
        //update ui based on previous
        UIUpdate();

        grabLeft(hit);
        grabRight(hit);
        useLeft();
        useRight();
    }

    void UIUpdate()
    {
        if (this.gameObject.GetComponentInChildren<HandUI>() != null)
        {
            this.gameObject.GetComponentInChildren<HandUI>().toggleLeft(validLeft);
            this.gameObject.GetComponentInChildren<HandUI>().toggleRight(validRight);
        }
    }
    void grabLeft(RaycastHit hit)
    {
        if (ControllerScan.Instance.grabbedLeft == true)
        {
            if (!holdingLeft)
            {
                if (hit.transform == null) return;
                holdingLeft = true;
                idLeft = hit.transform.gameObject.GetComponent<Item>().getID();
                Debug.Log("grabbed left");
                LeftUI.sprite = hit.transform.gameObject.GetComponent<Item>().getItemData().itemIcon;

                ItemManager.Instance.addItem(hit.transform.gameObject.GetComponent<Item>().getItemData());

                hit.transform.gameObject.SetActive(false);
            }
        }
    }

    void grabRight(RaycastHit hit)
    {
        if(ControllerScan.Instance.grabbedRight == true)
        {

            if (!holdingRight)
            {
                if (hit.transform == null) return;
                holdingRight = true;
                idRight = hit.transform.gameObject.GetComponent<Item>().getID();
                Debug.Log("grabbed right");
                RightUI.sprite = hit.transform.gameObject.GetComponent<Item>().getItemData().itemIcon;

                ItemManager.Instance.addItem(hit.transform.gameObject.GetComponent<Item>().getItemData());

                hit.transform.gameObject.SetActive(false);

            }
        }
    }

    void useLeft()
    {
        if(ControllerScan.Instance.usedLeft == true && holdingLeft)
        {
            Debug.Log("used left: " + idLeft);

            ItemManager.Instance.setHeldItem(ItemManager.Instance.FindItem(idLeft));

            ItemManager.Instance.useItem(idLeft);


            if (ItemManager.Instance.isConsumeable())
            {
                holdingLeft = false;
                idLeft = -1;
                LeftUI.sprite = null;

                ItemManager.Instance.removeItem(ItemManager.Instance.heldItem);
                if (ItemManager.Instance.itemScriptables.Count > 0)
                {
                    /// set to the last object in the list will change later
                    int tempID = ItemManager.Instance.itemScriptables.Count - 1;
                    ItemManager.Instance.setHeldItem(tempID);
                }
                else
                {
                    ItemManager.Instance.heldItem = null;
                }

            }





        }

    }
    void useRight()
    {
        if (ControllerScan.Instance.usedRight == true && holdingRight)
        {
            ItemManager.Instance.setHeldItem(ItemManager.Instance.FindItem(idRight));
            ItemManager.Instance.useItem(idRight);

            if (ItemManager.Instance.isConsumeable())
            {
                holdingLeft = false;
                idRight = -1;
                RightUI.sprite = null;

                ItemManager.Instance.removeItem(ItemManager.Instance.heldItem);
                if(ItemManager.Instance.itemScriptables.Count > 0)
                {
                    /// set to the last object in the list will change later
                    int tempID = ItemManager.Instance.itemScriptables.Count - 1;
                    ItemManager.Instance.setHeldItem(tempID);
                }
                else
                {
                    ItemManager.Instance.heldItem = null;
                }
               
            }
          

           


        }
    }
}
