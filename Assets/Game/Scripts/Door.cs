using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public int doorID;
    public float doorHeight;
    public bool isOpening;
    private Vector3 defaultPos;

    private void OnDisable()
    {
        transform.DOKill();
    }

    public void Start()
    {
        isOpening = false;
        doorHeight = GetComponent<MeshRenderer>().localBounds.size.y;
        defaultPos = transform.localPosition;
    }
    [Sirenix.OdinInspector.Button]
    public void Open()
    {
        if (!isOpening)
        {
            transform.DOLocalMoveY(doorHeight, 1.0f).OnComplete(() =>
            {
                isOpening = true;
            });
           
        }
    }

    [Sirenix.OdinInspector.Button]
    public void Close()
    {
        if (isOpening)
        {
            transform.DOLocalMoveY(defaultPos.y, 1.0f).OnComplete(() =>
            {
                isOpening = false;
            });
          
        }
    }
    
}
