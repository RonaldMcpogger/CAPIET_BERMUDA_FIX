using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public GameObject HandsUI;
    public GameObject InventoryUI;

    public static ItemManager Instance { get; private set; }

    private void Awake()
    {
        // If an instance already exists and it's not this one, destroy this duplicate.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            // Otherwise, set this as the instance.
            Instance = this;
            // Optionally, make the object persistent across scene loads.
            DontDestroyOnLoad(this);
        }
    }

}
