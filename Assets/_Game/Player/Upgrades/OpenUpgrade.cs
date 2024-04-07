using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenUpgrade : MonoBehaviour
{
    [SerializeField] GameObject upgradeWindow;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private string name;
    private void OnMouseDown()
    {
        playerStats.upgradeOpen = true;
        upgradeWindow.SetActive(true);
    }   
}
