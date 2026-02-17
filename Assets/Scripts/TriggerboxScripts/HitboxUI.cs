using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class HitboxUI : MonoBehaviour
{
    [SerializeField] GameObject button;
    [SerializeField] GameObject text;
    [SerializeField] GameObject fader;
    float alpha;
    float alphaGoal;
    string scene;
    bool introFade;
    bool outroFade;
    // Start is called before the first frame update
    void Start()
    {
        button.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
        Color c = fader.gameObject.GetComponent<Image>().color;
        c.a = 1.0f;
        fader.gameObject.GetComponent<Image>().color = c;
        alpha = 1.0f;
        introFade = true;
        outroFade = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (introFade && alpha >= 0.05f) {
            alpha = Mathf.Lerp(alpha, 0, Time.deltaTime /2f);
            //Debug.Log(alpha);
            Color c = fader.gameObject.GetComponent<Image>().color;
            c.a = alpha;
            fader.gameObject.GetComponent<Image>().color = c;
        } else if (introFade)
        {
            introFade = false;
            Color c = fader.gameObject.GetComponent<Image>().color;
            c.a = 0;
            fader.gameObject.GetComponent<Image>().color = c;
        } else if (outroFade && alpha <= 1f)
        {
            alpha = Mathf.Lerp(alpha, 1.2f, Time.deltaTime *.99f);
         //   Debug.Log(alpha);
            Color c = fader.gameObject.GetComponent<Image>().color;
            c.a = alpha;
            fader.gameObject.GetComponent<Image>().color = c;
        } 
        else if (outroFade && alpha > 0.89f) //on completion after alpha flag changes
        {
            Color c = fader.gameObject.GetComponent<Image>().color;
            c.a =1;
            fader.gameObject.GetComponent<Image>().color = c;

           
            SceneManager.LoadScene(scene);
         

        }
    }

    public void setUIActive(string str)
    {
        button.gameObject.SetActive(true);
        text.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = str;
        text.gameObject.SetActive(true);
    }

    public void setUIInactive()
    {
        button.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
    }

    public void startFade(string sceneToLoad)
    {
                
        alpha = 0;
        outroFade = true;
        scene = sceneToLoad;
    }

}
