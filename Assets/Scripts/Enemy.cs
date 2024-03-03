using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float minX, maxX;
    public GameObject offScreenVFX;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        Destroy(gameObject, 6f);
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 0.7f);
    }

}

  