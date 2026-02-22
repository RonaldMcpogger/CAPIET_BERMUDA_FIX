using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DexSubText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void changeText(int y)
    {
        if (KeyManager.Instance.getKey(y))
        {
            switch (y)
            {
                case 0: //ISOPOD
                    this.gameObject.GetComponent<TextMeshProUGUI>().text = "One of the earliest discoveries in the post-nuclear ocean. Seemingly well adapted to the fallout," +
                        " no noticable changes to available pre-fallout data on isopods. Found on the ocean floor and other areas.";
                    break;
                case 1: //SEA ANGEL
                    this.gameObject.GetComponent<TextMeshProUGUI>().text = "Glows brighter than previously known specimens of this species. Tend to group together around areas of note. " +
                        "Significantly more common than pre-fallout levels, suggesting a change in breeding rate and patterns.";
                    break;
                case 2: //BARRELEYE
                    this.gameObject.GetComponent<TextMeshProUGUI>().text = "Another instance of a deep sea creature becoming significantly more common post-fallout. " +
                        "Behavior patterns also suggest more aggression or curiousity towards human teams. Its real eyes are within its transparent head, " +
                        "which it uses to hunt creatures above it that block the light above. No reports of bioluminescense in said eyes in any accounts of pre-fallout specimen.";
                    break;
                case 3: //SPOTLIGHT
                    this.gameObject.GetComponent<TextMeshProUGUI>().text = "LOG ▓:\nThis is ▓▓▓▓▓▓▓▓▓▓, captain of submersible ▓▓▓-▓▓ reporting sightings of what seems to be an Anglerfish. " +
                        "There's something wrong with it.. It's nothing like what any of our logs have on this species! " +
                        "It's been circling us for the past ten minutes, and- shit, it's getting closer- ARGH! That light... it's coming from the" +
                        " damn thing??? AGHHHHH [Recording End]";
                    break;
                case 4:
                    this.gameObject.GetComponent<TextMeshProUGUI>().text = "LOG ▓▓:\nReporting for arrival on site ▓▓▓▓▓▓. The damn ship's been torn to shreds by that fish, " +
                        "it's hard to believe there'll be anything recoverable in this mess. Woah, are those pieces... moving? They're whole chunks of metal, how can it-" +
                        "What is that??? Full thrusters away from it! God- it's not doing anything, we're still losing speed, it's almost like we're trapped- [CRACKLING AND CRUSHING NOISES]\n[Recording End]";
                    break;
                case 5:
                    this.gameObject.GetComponent<TextMeshProUGUI>().text = "LOG ▓▓▓:\nIt's fascinating. There's these sort of lights fluttering around the area where the last crash "+
                        "happened. Makes the area look alive.. On the bright side, it feels a lot easier to get around the area, almost like they're guiding me " +
                        "to where I need to go. Ship's been running worse, though. Been rumbling like it's about to give up on me. Hang on- are the lights coming towards me? [FILE CORRUPTED]";
                    break;
            }

        } else
        {
            this.gameObject.GetComponent<TextMeshProUGUI>().text = "Insufficient clearance. File locked.";
        }
    }
}
