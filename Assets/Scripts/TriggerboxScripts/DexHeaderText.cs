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
        string text;
        if (KeyManager.Instance.getKey(y))
        {
            text = "NAME: ";
            switch (y)
            {
                case 0:
                    text += "ISOPOD";
                    break;
                case 1:
                    text += "SEA ANGEL";
                    break;
                case 2:
                    text += "BARRELEYE";
                    break;
                case 3:
                    text += "SPOTLIGHT";
                    break;
                case 4:
                    text += "SEAPOUCH";
                    break;
                case 5:
                    text += "FIREWORK";
                    break;

            }
        }
        else
        {
            text = "FILE ENCRYPTED";
        }

        this.gameObject.GetComponent<TextMeshProUGUI>().text = text;
    }
}
