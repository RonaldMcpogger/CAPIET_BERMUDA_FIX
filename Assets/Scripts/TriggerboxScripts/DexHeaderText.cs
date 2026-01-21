using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DexHeaderText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeText(int y)
    {
        string text = "NAME: ";
        switch (y)
        {
            case 0:
                text += "TEST 0";
                break;
            case 1:
                text += "TEST 1";
                break;
            case 2:
                text += "TEST 2";
                break;
            case 3:
                text += "TEST 3";
                break;
            case 4:
                text += "TEST 4";
                break;
            case 5:
                text += "TEST 5";
                break;

        }

        this.gameObject.GetComponent<TextMeshProUGUI>().text = text;
    }
}
