using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScreenUI : MonoBehaviour
{
    [SerializeField] GameObject screenUI;
    [SerializeField] GameObject regularUI;
    [SerializeField] TextMeshProUGUI xText;
    [SerializeField] TextMeshProUGUI yText;
    bool active;

    int x;
    int y;
    bool pressed;

    [SerializeField] GameObject topLeft;
    [SerializeField] GameObject midLeft;
    [SerializeField] GameObject botLeft;
    [SerializeField] GameObject botbotLeft;
    [SerializeField] GameObject topMid;
    [SerializeField] GameObject midMid;
    [SerializeField] GameObject botMid;
    [SerializeField] GameObject botbotMid;
    [SerializeField] GameObject topRight;
    [SerializeField] GameObject midRight;
    [SerializeField] GameObject botRight;
    [SerializeField] GameObject botbotRight;
    [SerializeField] GameObject back;
    [SerializeField] GameObject enter;

    List<string> nums = new List<string>();
    int current;

    bool frameBuffer;

    // Start is called before the first frame update
    void Start()
    {
        regularUI.GetComponent<Canvas>().enabled = true;
        screenUI.GetComponent<Canvas>().enabled = false;
        x = 0;
        y = 0;
        active = false;
        nums.Add("");
        nums.Add("");
        current = 0;
        xText.text = "X";
        xText.text = "Y";
        frameBuffer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (active && !frameBuffer)
        {
            Debug.Log(nums.Count);
            checkDpad();
            checkInput();
        }
        frameBuffer = false;
    }

    public void toggleScreenOn()
    {
        if (!active)
        {
            regularUI.GetComponent<Canvas>().enabled = false;
            screenUI.GetComponent<Canvas>().enabled = true;
            changeColor(topLeft.gameObject.GetComponent<Button>(), .5f);
            active = true;
            frameBuffer = true;
        }
    }

    public void toggleScreenOff()
    {
        regularUI.GetComponent<Canvas>().enabled = true;
        screenUI.GetComponent<Canvas>().enabled = false;
        active = false;
    }


    void checkDpad()
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
        if (ControllerScan.Instance.leftAction.WasPressedThisFrame() == true)
        {
            if (x > 0)
            {
                x--;
                pressed = true;
            }
        }
        if (ControllerScan.Instance.rightAction.WasPressedThisFrame() == true)
        {
            if (x < 4)
            {
                x++;
                pressed = true;
            }
        }

        if (pressed)
        {
            resetCol();
            switch (y)
            {
                case 0:
                    switch (x)
                    {

                        case 0: //0, 0 top left
                            changeColor(topLeft.gameObject.GetComponent<Button>(), .5f);
                            break;
                        case 1: //1, 0
                            changeColor(topMid.gameObject.GetComponent<Button>(), .5f);
                            break;
                        case 2: // 2,0
                            changeColor(topRight.gameObject.GetComponent<Button>(), .5f);
                            break;
                        case 3: // 3,0
                            changeColor(back.gameObject.GetComponent<Button>(), .5f);
                            break;
                    }

                    break;

                case 1:
                    switch (x)
                    {

                        case 0: //0, 1
                            changeColor(midLeft.gameObject.GetComponent<Button>(), .5f);
                            break;
                        case 1: //1, 1
                            changeColor(midMid.gameObject.GetComponent<Button>(), .5f);
                            break;
                        case 2: // 2,1
                            changeColor(midRight.gameObject.GetComponent<Button>(), .5f);
                            break;
                        case 3: // 3,1
                            changeColor(enter.gameObject.GetComponent<Button>(), .5f);
                            break;
                    }

                    break;

                case 2:
                    switch (x)
                    {

                        case 0: //0, 2
                            changeColor(botLeft.gameObject.GetComponent<Button>(), .5f);
                            break;
                        case 1: //1, 2
                            changeColor(botMid.gameObject.GetComponent<Button>(), .5f);
                            break;
                        case 2: // 2,2
                            changeColor(botRight.gameObject.GetComponent<Button>(), .5f);
                            break;
                        case 3: // 3,2
                            x = 2;
                            changeColor(botRight.gameObject.GetComponent<Button>(), .5f);
                            break;
                    }

                    break;

                case 3:
                    switch (x)
                    {

                        case 0: //0, 3
                            changeColor(botbotLeft.gameObject.GetComponent<Button>(), .5f);
                            break;
                        case 1: //1, 3
                            changeColor(botbotMid.gameObject.GetComponent<Button>(), .5f);
                            break;
                        case 2: // 2,3
                            changeColor(botbotRight.gameObject.GetComponent<Button>(), .5f);
                            break;
                        case 3: // 3,3
                            x = 2;
                            changeColor(botbotRight.gameObject.GetComponent<Button>(), .5f);
                            break;
                    }

                    break;
            }
            pressed = false;
        }
    }
    void resetCol()
    {
        changeColor(topLeft.gameObject.GetComponent<Button>(), 1f);
        changeColor(topMid.gameObject.GetComponent<Button>(), 1f);
        changeColor(topRight.gameObject.GetComponent<Button>(), 1f);
        changeColor(back.gameObject.GetComponent<Button>(), 1f);
        changeColor(midLeft.gameObject.GetComponent<Button>(), 1f);
        changeColor(midMid.gameObject.GetComponent<Button>(), 1f);
        changeColor(midRight.gameObject.GetComponent<Button>(), 1f);
        changeColor(enter.gameObject.GetComponent<Button>(), 1f);
        changeColor(botLeft.gameObject.GetComponent<Button>(), 1f);
        changeColor(botMid.gameObject.GetComponent<Button>(), 1f);
        changeColor(botRight.gameObject.GetComponent<Button>(), 1f);
        changeColor(botbotLeft.gameObject.GetComponent<Button>(), 1f);
        changeColor(botbotMid.gameObject.GetComponent<Button>(), 1f);
        changeColor(botbotRight.gameObject.GetComponent<Button>(), 1f);

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
            Debug.Log("number entered");
            bool numUpdated = false;
            switch (y)
            {
                case 0:
                    switch (x)
                    {

                        case 0: //0, 0 top left
                            nums[current] += 7;
                            numUpdated = true;
                            break;
                        case 1: //1, 0
                            nums[current] += 8;
                            numUpdated = true;
                            break;
                        case 2: // 2,0
                            nums[current] += 9;
                            numUpdated = true;
                            break;
                        case 3: // 3,0
                            nums[current] = nums[current].Substring(0, nums[current].Length - 1);
                            numUpdated = true;
                            break;
                    }

                    break;

                case 1:
                    switch (x)
                    {

                        case 0: //0, 1
                            nums[current] += 4;
                            numUpdated = true;
                            break;
                        case 1: //1, 1
                            nums[current] += 5;
                            numUpdated = true;
                            break;
                        case 2: // 2,1
                            nums[current] += 6;
                            numUpdated = true;
                            break;
                        case 3: // 3,1
                            if (current < 2)
                            {
                                current++;
                            } else
                            {//set teleport data here
                                toggleScreenOff();
                            }
                            break;
                    }

                    break;

                case 2:
                    switch (x)
                    {

                        case 0: //0, 2
                            nums[current] += 1;
                            numUpdated = true;
                            break;
                        case 1: //1, 2
                            nums[current] += 2;
                            numUpdated = true;
                            break;
                        case 2: // 2,2
                            nums[current] += 3;
                            numUpdated = true;
                            break;
                    }

                    break;

                case 3:
                    switch (x)
                    {
                        case 0: //0, 3
                            if (nums[current].Length < 1)
                            {
                                nums[current] += '-';
                                numUpdated = true;
                            }
                            break;
                        case 1: //1, 3
                            nums[current] += 0;
                            numUpdated = true;
                            break;
                        case 2: // 2,3
                            nums[current] += '.';
                            numUpdated = true;
                            break;
                    }

                    break;
            }

            if (numUpdated)
            {
                if(current == 0)
                {
                    xText.text = nums[current];
                }
                else
                {
                    yText.text = nums[current];
                }
            }

            numUpdated = false;
        }
    }
}
