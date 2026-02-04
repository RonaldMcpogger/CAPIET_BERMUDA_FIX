using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    bool pressed;
    int index;
    float fade;
    [SerializeField] List<GameObject> boxes;
    // Start is called before the first frame update
    void Start()
    {
        pressed = false;
        index = 0;
        fade = 0.2f;
        changeColor(boxes[index].gameObject.GetComponent<Button>(), fade);
    }

    // Update is called once per frame
    void Update()
    {
        if (ControllerScan.Instance.leftAction.WasPressedThisFrame() == true)
        {
            if (index > 0)
            {
                index--;
                pressed = true;
            }

        }
        if (ControllerScan.Instance.rightAction.WasPressedThisFrame() == true)
        {
            if (index < 1)
            {
                index++;
                pressed = true;
            }

        }

        if (pressed)
        {
            resetCol();
            changeColor(boxes[index].gameObject.GetComponent<Button>(), fade);
            pressed = false;
        }

        checkInput();
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

    void checkInput()
    {
        if (ControllerScan.Instance.interactAction.WasPressedThisFrame())
        {
            switch (index)
            {
                case 0://start
                    SceneManager.LoadScene("Level1-Tutorial");
                    break;
                case 1: //quit
                    Application.Quit();

                    #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                    #endif
                    break;

            }
        }
    }
}
