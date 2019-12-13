using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterDisplay : MonoBehaviour
{
    public int water = 0;
    public Text waterText;

    void Update()
    {
        waterText.text = "Water: " + ResourcesCollected.water;
    }
}
