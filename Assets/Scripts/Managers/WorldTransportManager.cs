using System.Collections.Generic;

using UnityEngine;

public class WorldTransportManager : MonoBehaviour
{
    // ram note: this is the current lvel manager, 
    public static WorldTransportManager Instance { get; private set; }
    [SerializeField] private string diedScene;

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
    Vector2 lvl3Code;
    Vector2 lvl4Code;

    List<Vector2> levels = new List<Vector2>();

    string sceneToLoad = "Level1-Tutorial";
    private void Start()
    {
        //FOR THE LEVELS
        lvl1Code = new Vector2(345, 375);
        lvl2Code = new Vector2(540, 284);
        lvl3Code = new Vector2(721, 186);
        lvl4Code = new Vector2(890, 086);

        levels.Add(lvl1Code);
        levels.Add(lvl2Code);
        levels.Add(lvl3Code);
        levels.Add(lvl4Code);
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
                case 3:
                    sceneToLoad = "Level3";
                    break;
                case 4:
                    sceneToLoad = "LevelFinal";
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

    public void setDiedScene(string died)
    {
        this.diedScene = died;
    }

    public string getDiedScene()
    {
        return this.diedScene;
    }
}
