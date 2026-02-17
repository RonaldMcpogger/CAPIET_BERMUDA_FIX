using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class HandInv : MonoBehaviour

{

    [SerializeField] private Image handImg;
    [SerializeField] private int hand;
    // Start is called before the first frame update
    void Start()
    {
        handImg = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hand == 0 && ItemManager.Instance.getItemInHand(0)!= null)
        {
           
            handImg.sprite = ItemManager.Instance.getItemInHand(0).itemIcon;
        }
        else if(hand == 1 && ItemManager.Instance.getItemInHand(1) != null)
        {
            
            handImg.sprite = ItemManager.Instance.getItemInHand(1).itemIcon;
        }
        else
        {
                       handImg.sprite = null;
        }
    }
}
