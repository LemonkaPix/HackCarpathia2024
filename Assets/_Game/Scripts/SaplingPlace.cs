using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaplingPlace : MonoBehaviour
{
    public bool isTree = false;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject treePrefab;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        GrowTree();
    }

    public void GrowTree()
    {
        if (!isTree)
        {
            spriteRenderer.enabled = false;
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