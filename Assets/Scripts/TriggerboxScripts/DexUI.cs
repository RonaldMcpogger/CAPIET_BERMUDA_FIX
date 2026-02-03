using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Presets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DexUI : MonoBehaviour
{
    int y;
    [SerializeField] float fade = 0.2f;
    bool pressed;
    [SerializeField] List<GameObject> boxes;
    [SerializeField] List<GameObject> models;

    bool dexEnabled;
    // Start is called before the first frame update
    void Start()
    {

        fade = 0.2f;
        dexEnabled = false;

        
    }


    // Update is called once per frame
    void Update()
    {
        Debug.Log(dexEnabled);
        if (dexEnabled)
        {
            
            if (ControllerScan.Instance.upAction.WasPressedThisFrame() == true)
            {
                if (y > 0)
                {
                    y--;
                    pressed = true;
                }

            }
            if (ControllerScan.Instance.downAction.WasPressedThisFrame() == true)
            {
                if (y < 5)
                {
                    y++;
                    pressed = true;
                }

            }

            if (ControllerScan.Instance.leftAction.WasPressedThisFrame() == true) //return to tabs
            {
                dexEnabled = false;
                resetCol();
                FindFirstObjectByType<TabUI>().enableTabs();
            }

            if (pressed)
            {
                resetCol();
                changeColor(boxes[y].gameObject.GetComponent<Button>(), fade);
                changeText();
                changeModel();
                pressed = false;
            }
        }

    }
    void resetCol()
    {
        for (int i = 0; i < boxes.Count; i++)
        {
            changeColor(boxes[i].gameObject.GetComponent<Button>(), 1f);
        }
    }
    void changeColor(Button button, float a)
    {
        ColorBlock cb = button.colors;
        Color c = cb.normalColor;
        c.a = a;
        cb.normalColor = c;
        button.colors = cb;
    }

    void changeText()
    {
        GetComponentInChildren<DexHeaderText>().changeText(y);
        GetComponentInChildren<DexSubText>().changeText(y);
    }

    void changeModel()
    {
        for (int i = 0; i < models.Count; i++)
        {
            if (i == y)
                models[i].SetActive(true);
            else
                models[i].SetActive(false);
        }
    }

    public void onShow()
    {
        for (int i = 0; i < boxes.Count; i++) //checks the keys when enabled
        {
            if (KeyManager.Instance.getKey(i) == true)
            {
                switch (i)
                {
                    case 0:
                        boxes[i].GetComponentInChildren<TextMeshProUGUI>().text = "001";
                        break;
                    case 1:
                        boxes[i].GetComponentInChildren<TextMeshProUGUI>().text = "002";
                        break;
                    case 2:
                        boxes[i].GetComponentInChildren<TextMeshProUGUI>().text = "003";
                        break;
                    case 3:
                        boxes[i].GetComponentInChildren<TextMeshProUGUI>().text = "026";
                        break;
                    case 4:
                        boxes[i].GetComponentInChildren<TextMeshProUGUI>().text = "031";
                        break;
                    case 5:
                        boxes[i].GetComponentInChildren<TextMeshProUGUI>().text = "110";
                        break;
                }
            }
            else
                boxes[i].GetComponentInChildren<TextMeshProUGUI>().text = "???"; //set to ??? to ensure no wrong text
        }

        for (int i = 0; i < models.Count; i++)
        {
            models[i].SetActive(false);
        }

        models[0].SetActive(true);
    }

    public void enableDex()
    {
        Debug.Log("Initializing Dex");
        dexEnabled = true;
        y = 0;
        changeColor(boxes[y].gameObject.GetComponent<Button>(), fade);
        changeText();
        models[y].SetActive(true);
    }
}
