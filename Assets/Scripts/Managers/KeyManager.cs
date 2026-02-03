using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public static KeyManager Instance { get; private set; }
    List<bool> keys = new List<bool>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }


        
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            Debug.Log("made key " + i);
            keys.Add(true); //remember to set this to false when done testing
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setKeyEnabled(int loc) // call this function to allow the dex to show appropriate information
    {
        if (loc < keys.Count)
        {
            keys[loc] = true;
        }
    }

    public bool getKey(int loc)
    {
        if (loc < keys.Count)
        {
            return keys[loc];
        }
        return false;
    }
}
