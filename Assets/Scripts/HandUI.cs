using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandUI : MonoBehaviour
{
    [SerializeField] GameObject leftHand;
    [SerializeField] GameObject rightHand;

    // Start is called before the first frame update
    void Start()
    {
        leftHand.SetActive(false);
        rightHand.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleLeft(bool b)
    {
        leftHand.SetActive(b);
    }

    public void toggleRight(bool b)
    {
        rightHand.SetActive(b);
    }
}
