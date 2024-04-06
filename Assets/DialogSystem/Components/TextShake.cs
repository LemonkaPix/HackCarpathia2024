using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class TextShake : MonoBehaviour
{

    TMP_Text textComponent;
    

    [Range(0f, 25f)]
    public float timeMultiplier = 2f;
    [Range(0f, 10f)]
    public float xAxisMultiplier = 0.01f;
    [Range(0f, 100f)]
    public float yAxisMultiplier = 10f;

    bool start = false;
    string shakeText;
    int startShakeIndex;

    public void Shake(bool state, string startText, int startIndex)
    {
        start = state;
        shakeText = startText;
        startShakeIndex = startIndex;
    }

    string RemoveMarkers(string text)
    {
        Regex rich = new Regex(@"<[^>]*>");

        if (rich.IsMatch(text))
        {
            return text = rich.Replace(text, string.Empty);
        }
        return text;
    }

    void Update()
    {
        if (!start) return;

        textComponent.ForceMeshUpdate();
        TMP_TextInfo textInfo = textComponent.textInfo;

        if (startShakeIndex < 0) return;

        shakeText = RemoveMarkers(shakeText);
        

        Debug.Log(shakeText);

        int endShakeIndex = startShakeIndex + shakeText.Length > textInfo.characterCount ? textInfo.characterCount : startShakeIndex + shakeText.Length;

        

        for (int i = startShakeIndex; i < endShakeIndex; ++i)
        {
            TMP_CharacterInfo charInfo = textInfo.characterInfo[i];

            if(!charInfo.isVisible) continue;


            Vector3[] verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

            for(int j = 0; j < 4; ++j)
            {
                Vector3 og = verts[charInfo.vertexIndex + j];
                verts[charInfo.vertexIndex + j] = og + new Vector3(0, Mathf.Sin(Time.time * timeMultiplier + og.x * xAxisMultiplier) * yAxisMultiplier, 0);

            }
        }

        for(int i = 0; i < textInfo.meshInfo.Length; ++i)
        {
            TMP_MeshInfo meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            textComponent.UpdateGeometry(meshInfo.mesh, i);
        }
    }

    void Start()
    {
        textComponent = GetComponent<TMP_Text>();
    }
}
