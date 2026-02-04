using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    bool paused;
    bool canPause;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] List<GameObject> boxes;
    int index = 0;
    bool pressed;
    float fade;
    // Start is called before the first frame update
    void Start()
    {
        paused = false;
        canPause = true;
        pauseMenu.SetActive(false);
        pressed = false;
        fade = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (ControllerScan.Instance.startAction.WasPressedThisFrame() && canPause) {
            paused = !paused;

            if (paused)
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
                AudioListener.pause = true; //in case audio is added
                index = 0;
                resetCol();
                changeColor(boxes[index].gameObject.GetComponent<Button>(), fade);
            } else
            {
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
                AudioListener.pause = false;
            }
        }

        if (paused)
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
    }

    public void setCanPause(bool b)
    {
        canPause = b;
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
                    Time.timeScale = 1;
                    pauseMenu.SetActive(false);
                    AudioListener.pause = false;
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
