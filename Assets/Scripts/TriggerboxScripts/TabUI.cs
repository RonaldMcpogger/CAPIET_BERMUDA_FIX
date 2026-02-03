using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TabUI : MonoBehaviour
{

    ScreenUI screen;
    int y;
    bool pressed;
    bool tabEnabled;

    [SerializeField] GameObject top;
    [SerializeField] GameObject mid;
    [SerializeField] GameObject bot;
    [SerializeField] GameObject quit;

    [SerializeField] GameObject missionUI;
    [SerializeField] GameObject dexUI;

    [SerializeField] float fade = 0.2f;

    // Start is called before the first frame update
    void OnEnable()
    {
        screen = FindFirstObjectByType<ScreenUI>();
        fade = 0.2f;
        missionUI.SetActive(false);
        dexUI.SetActive(false);
        tabEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (screen.getIgnoreInputs() && tabEnabled) // if navigation screen is currently ignoring inputs, free to show and tab around to others
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
                if (y < 3)
                {
                    y++;
                    pressed = true;
                }

            }

            if (ControllerScan.Instance.rightAction.WasPressedThisFrame() == true) //do other stuff not on tabs
            {
                //check which tab and return input there, tab 0 is navi, tab 1 is mission screen (should have no available inputs), tab 2 is endemic life info tab
                
                switch (y)
                {
                    case 0:
                        FindFirstObjectByType<ScreenUI>().startAcceptingInputs();
                        break;
                    case 2:
                        Debug.Log("passing");
                        FindFirstObjectByType<DexUI>().enableDex();
                        tabEnabled = false;
                        break;
                    default:
                        Gamepad.current.SetMotorSpeeds(0.4f, 0.9f); //motor rumble on unavailable input
                        break;
                }

            }

            if (pressed)
            {
                resetCol();
                switch (y)
                {
                    case 0: //screenui
                        changeColor(top.gameObject.GetComponent<Button>(), fade);
                        missionUI.SetActive(false);
                        dexUI.SetActive(false);
                        break;
                    case 1: //mission
                        changeColor(mid.gameObject.GetComponent<Button>(), fade);
                        missionUI.SetActive(true);
                        dexUI.SetActive(false);
                        break;
                    case 2: // dex
                        changeColor(bot.gameObject.GetComponent<Button>(), fade);
                        missionUI.SetActive(false);
                        dexUI.SetActive(true);
                        dexUI.GetComponent<DexUI>().onShow();
                        break;
                    case 3:
                        changeColor(quit.gameObject.GetComponent<Button>(), fade);
                        break;

                }
                pressed = false;
            }

            checkInput();

        } // exit getignoreinputs
    }

    public void enableTabs()
    {
        tabEnabled = true;
    }
    public void resetNums()
    {
        y = 0;
        changeColor(top.gameObject.GetComponent<Button>(), fade);
    }

    void resetCol()
    {
        changeColor(top.gameObject.GetComponent<Button>(), 1f);
        changeColor(mid.gameObject.GetComponent<Button>(), 1f);
        changeColor(bot.gameObject.GetComponent<Button>(), 1f);
        changeColor(quit.gameObject.GetComponent<Button>(), 1f);
    }
    void changeColor(Button button, float a)
    {
        ColorBlock cb = button.colors;
        Color c = cb.normalColor;
        c.a = a;
        cb.normalColor = c;
        button.colors = cb;
    }

    void checkInput()
    {
        if (ControllerScan.Instance.interactAction.WasPressedThisFrame())
        {

            switch (y) //so far only quit button needs an input check, but this function is here should that functionality change
            {
                case 3:
                    Gamepad.current.SetMotorSpeeds(0.4f, 0.9f);
                    FindAnyObjectByType<OpenScreen>().GetComponent<OpenScreen>().setScreenActive(false);
                    y = 0;
                    resetCol();
                    screen.toggleScreenOff();
                    missionUI.SetActive(false);
                    dexUI.SetActive(false);
                    break;
            }
        }
    }
}
