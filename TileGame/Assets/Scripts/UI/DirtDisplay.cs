using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirtDisplay : MonoBehaviour
{
    public int dirt = 0;
    public Text dirtText;


    void Update()
    {
        dirtText.text = "Dirt: " + ResourcesCollected.dirt;
    }
}
