using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaplingPlace : MonoBehaviour
{
    public bool isTree = false;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject treePrefab;
    private float startTime = 0;
    [SerializeField] float holdTime = 1f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
         if (PlayerStats.Instance.Water > 5)
            {
                PlayerStats.Instance.Water -= 5;
                GrowTree();
            }

    }
    

    public void GrowTree()
    {
        if (!isTree)
        {
 
            isTree = true;
            Tree tree = Instantiate(treePrefab, this.gameObject.transform).GetComponent<Tree>();
            tree.saplingPlace = this;
        }
    }
    public void ChopTree()
    {
        if (isTree)
        {
            spriteRenderer.enabled = true;
            isTree = false;
        }
    }
}
