using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenScreen : MonoBehaviour, HitboxScript
{
    [SerializeField] GameObject player;
    bool active;
    float timesinceLastOpen;
    // Start is called before the first frame update
    void Start()
    {
        timesinceLastOpen = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(timesinceLastOpen < 1f)
        {
            timesinceLastOpen += Time.deltaTime;
           
        }
        Debug.Log(timesinceLastOpen);
    }
    public void entered()
    {
        player.GetComponentInChildren<HitboxUI>().setUIActive("Open Monitor");
    }

    public void exited()
    {
        player.GetComponentInChildren<HitboxUI>().setUIInactive();
    }

    public void interact()
    {
        if (!active && timesinceLastOpen >=1f)
        {
            player.GetComponentInChildren<ScreenUI>().toggleScreenOn();
            active = true;
        }
    }

    public void resetActive()
    {
        active = false;
        timesinceLastOpen = 0;
    }
    public void up()
    {
       // Debug.Log("up");
    }

    public void setScreenActive(bool a)
    {
        Debug.Log(a);
        active = a;
    }
}
