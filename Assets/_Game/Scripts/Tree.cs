using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public SaplingPlace saplingPlace;
    [SerializeField] private Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    [SerializeField] int state = 1;
    [SerializeField] int statesToGrow = 3;
    [SerializeField] private int woodAmount;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    [Button]
    public void ChangeState()
    {
        if (state < statesToGrow)
        {
            state++;
            spriteRenderer.sprite = sprites[state - 1];
        }
    }

    public void Chop()
    {
        if (state == statesToGrow)
        {
            PlayerStats.Instance.Wood += woodAmount;
            Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        Chop();
    }
}
