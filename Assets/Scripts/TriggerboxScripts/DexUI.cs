using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
    List<bool> keys;
    bool dexEnabled;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 6; i++)
            keys.Add(false);
        fade = 0.2f;
        enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
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
                if (y < 6)
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

    void setKeyEnabled(int loc) // call this function to allow the dex to show appropriate information
    {
        if (loc < keys.Count)
        {
            keys[loc] = true;
        }
    }

    public void enableDex()
    {
        dexEnabled = true;
        y = 0;
        changeColor(boxes[y].gameObject.GetComponent<Button>(), fade);
        changeText();
    }
}
