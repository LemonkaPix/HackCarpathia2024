using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextShakeMarkFinder : MonoBehaviour
{
    public static string FindMarker(TMP_Text textComponent)
    {
        if(!textComponent.text.Contains("<shake>".Trim().ToLower())) return "";

        string slicedText = textComponent.text.Substring(textComponent.text.IndexOf("<shake>"));

        int markStartIndex = slicedText.IndexOf("<shake>");
        slicedText = slicedText.Remove(markStartIndex, 7);

        int markEndIndex = slicedText.IndexOf("</shake>");
        if (markEndIndex < 0) return "";

        slicedText = slicedText.Remove(markEndIndex);

        Debug.Log(slicedText);

        return slicedText;
    }

    public static string RemoveMarkers(TMP_Text textComponent)
    {
        if (!textComponent.text.Contains("<shake>".Trim().ToLower())) return "";

        string slicedText = textComponent.text;

        int markStartIndex = slicedText.IndexOf("<shake>");
        slicedText = slicedText.Remove(markStartIndex, 7);

        int markEndIndex = slicedText.IndexOf("</shake>");
        if (markEndIndex < 0) return "";
        slicedText = slicedText.Remove(markEndIndex, 8);

        return slicedText;
    }
}
