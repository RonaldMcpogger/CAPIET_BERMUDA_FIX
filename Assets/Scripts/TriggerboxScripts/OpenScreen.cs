using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenScreen : MonoBehaviour, HitboxScript
{
    [SerializeField] GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        player.GetComponentInChildren<ScreenUI>().toggleScreenOn();
    }
    public void up()
    {
        Debug.Log("up");
    }
}
