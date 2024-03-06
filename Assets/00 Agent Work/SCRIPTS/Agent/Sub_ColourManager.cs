using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sub_ColourManager : MonoBehaviour
{
  
    public Dictionary<string, Color> colors = new Dictionary<string, Color>();

    void Start()
    {
        // Define colors library
        AddColor("excitedOrange", "#FF8D00");
        AddColor("introspectiveMagenta", "#F434FF");
        AddColor("sadBlue", "#4AA8FF");

    }

    void AddColor(string name, string hex)
    {
        Color newColor;
        if (ColorUtility.TryParseHtmlString(hex, out newColor))
        {
            colors[name] = newColor;
        }
    }


}


