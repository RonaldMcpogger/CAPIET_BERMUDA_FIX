using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class HandScript : MonoBehaviour
{
    //testing
  
    CharacterController controller;
    bool validLeft;
    bool validRight;

    bool holdingLeft;
    bool holdingRight;

    bool cooldownActive = false;
    float cooldownDuration = .5f; // Cooldown duration in seconds

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
        if (cooldownActive == false)
        {
            useLeft();
            useRight();
        }
        
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
                Item temp = hit.transform.gameObject.GetComponent<Item>();

                ItemManager.Instance.grabItem(hit.transform.gameObject.GetComponent<Item>().getItemData(), 0);


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


                ItemManager.Instance.grabItem(hit.transform.gameObject.GetComponent<Item>().getItemData(), 1);

                hit.transform.gameObject.SetActive(false);

            }
        }
    }

    void useLeft()
    {
        if(ControllerScan.Instance.usedLeft == true && holdingLeft)
        {
            if (ItemManager.Instance.isConsumeable(0))
            {
                holdingLeft = false;
                idLeft = -1;
            }
            ItemManager.Instance.useItem(0);
            StartCoroutine(startCooldown());
        }

    }
    void useRight()
    {
        if (ControllerScan.Instance.usedRight == true && holdingRight)
        {
            if (ItemManager.Instance.isConsumeable(1))
            {
                Debug.Log("used right and consumedAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                holdingRight = false;
                idRight = -1;
            }

            ItemManager.Instance.useItem(1);
            StartCoroutine(startCooldown());


        }
    }

    IEnumerator startCooldown()
    {
        cooldownActive = true;
        yield return new WaitForSeconds(cooldownDuration);
        cooldownActive = false;
    }

}
