using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenUpgrade : MonoBehaviour
{
    [SerializeField] GameObject upgradeWindow;
    private void OnMouseDown()
    {
        if (TutorialManager.instance.duringTutorial) return;
        upgradeWindow.SetActive(true);
    }   
}
