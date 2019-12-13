using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoneDisplay : MonoBehaviour
{
    public int stone = 0;
    public Text stoneText;

    void Update()
    {
        stoneText.text = "Stone: " + ResourcesCollected.stone;
    }
}
