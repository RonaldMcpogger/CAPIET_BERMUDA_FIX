using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(HitboxScript))]
public class PlayerScanner : MonoBehaviour
{

    bool isInside;
    // Start is called before the first frame update
    void Start()
    {
        isInside = false;
    }

    // Update is called once per frame
    void Update()
    {
        interact();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            isInside = true;
            Debug.Log("Enter");
            this.gameObject.GetComponent<HitboxScript>().entered();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInside = false;
            Debug.Log("Exit");
            this.gameObject.GetComponent<HitboxScript>().exited();
        }
    }

    public void interact()
    {
        if (ControllerScan.Instance.interactAction.WasPressedThisFrame() == true && isInside == true)
        {
            
            this.gameObject.GetComponent<HitboxScript>().interact();
        }
    }
}
