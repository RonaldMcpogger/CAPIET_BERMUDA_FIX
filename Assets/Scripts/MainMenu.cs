using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    bool pressed;
    int index;
    float fade;
    bool outroFade;
    [SerializeField] List<GameObject> boxes;
    [SerializeField] GameObject fader;
    float alpha = 0;
    // Start is called before the first frame update
    void Start()
    {
        pressed = false;
        index = 0;
        fade = 0.2f;
        changeColor(boxes[index].gameObject.GetComponent<Button>(), fade);
        alpha = 0;
        outroFade = false;

        Color c = fader.gameObject.GetComponent<Image>().color;
        c.a = alpha;
        fader.gameObject.GetComponent<Image>().color = c;
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

        if (outroFade && alpha <= 1f)
        {
            alpha = Mathf.Lerp(alpha, 1.2f, Time.deltaTime * .99f);
            Color c = fader.gameObject.GetComponent<Image>().color;
            c.a = alpha;
            fader.gameObject.GetComponent<Image>().color = c;
        }
        else if (outroFade && alpha > 0.99f) //on completion after alpha flag changes
        {
            Color c = fader.gameObject.GetComponent<Image>().color;
            c.a = 1;
            fader.gameObject.GetComponent<Image>().color = c;
            SceneManager.LoadScene("Level1-Tutorial");
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

    void checkInput()
    {
        if (ControllerScan.Instance.interactAction.WasPressedThisFrame())
        {
            switch (index)
            {
                case 0://start
                    outroFade = true;
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
