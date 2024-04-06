using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenUpgrade : MonoBehaviour
{
    [SerializeField] GameObject upgradeWindow;
    private void OnMouseDown()
    {
        upgradeWindow.SetActive(true);
    }
}
