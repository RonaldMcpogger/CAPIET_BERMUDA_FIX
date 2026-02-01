using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DexSubText : MonoBehaviour
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
        switch (y)
        {
            case 0:
                this.gameObject.GetComponent<TextMeshProUGUI>().text = "test 0";
                break;
            case 1:
                this.gameObject.GetComponent<TextMeshProUGUI>().text = "test 1";
                break;
            case 2:
                this.gameObject.GetComponent<TextMeshProUGUI>().text = "test 2";
                break;
            case 3:
                this.gameObject.GetComponent<TextMeshProUGUI>().text = "test 3";
                break;
            case 4:
                this.gameObject.GetComponent<TextMeshProUGUI>().text = "test 4";
                break;
            case 5:
                this.gameObject.GetComponent<TextMeshProUGUI>().text = "test 5";
                break;
        }
    }
}
