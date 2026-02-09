using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WorldTransportManager : MonoBehaviour
{
    // ram note: make error handling where if character has no coordinate, load the current level they are in 
    public static WorldTransportManager Instance { get; private set; }

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

    int currentLevel = 1;
    Vector2 lvl1Code;
    Vector2 lvl2Code;
    List<Vector2> levels = new List<Vector2>();
    string sceneToLoad = "Level1-Tutorial";
    private void Start()
    {
        lvl1Code = new Vector2(345, 375);
        lvl2Code = new Vector2(540, 284);

        levels.Add(lvl1Code);
        levels.Add(lvl2Code);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void checkLevel(float x, float y)
    {
        //Debug.Log(x + " " + y);
        int levelCheck = 1;
        bool found = false;
        foreach (var level in levels)
        {
            if (x == level.x && y == level.y)
            {
                found = true;
                break;
            }
            else
                levelCheck++;
        }

        if (found)
        {
            switch (levelCheck)
            {
                case 1:
                    sceneToLoad = "Level1-Tutorial";
                    break;
                case 2:
                    sceneToLoad = "Level2";
                    break;
                default: break;
            }
        }

        Debug.Log(found + " " + sceneToLoad);
    }

    public string getLevelToLoad()
    {
        return sceneToLoad;
    }
}
