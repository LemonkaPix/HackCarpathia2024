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
    private int woodAmount;

    private void Start()
    {
        PlayerStats.Instance.PollutionLoss += state;
        spriteRenderer = GetComponent<SpriteRenderer>();
        PlayerStats.Instance.OnNewGen.AddListener(ChangeState);
    }
    [Button]
    public void ChangeState()
    {
        if (state < statesToGrow)
        {
            PlayerStats.Instance.PollutionLoss += state;
            state++;
            spriteRenderer.sprite = sprites[state - 1];
        }
    }

    public void Chop()
    {
        if (state == statesToGrow)
        {
            PlayerStats.Instance.Wood += (int)PlayerStats.Instance.WoodGain; ;
            print(woodAmount);
            PlayerStats.Instance.PollutionLoss -= 6;
            if (saplingPlace != null)
                saplingPlace.ChopTree();
            Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        Chop();
    }
}
