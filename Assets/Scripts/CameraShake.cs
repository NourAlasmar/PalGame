using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraShake : MonoBehaviour
{
    public static CameraShake _inst;

    Vector3 basePos;
    private void Awake()
    {
        _inst = this;

    }

    private void Start()
    {
        basePos = transform.position;
    }
    public void Shake(float duration=0.3f,float power=1f)
    {
        transform.DOShakePosition(duration, power).OnComplete(OnShakeComplete);
    }

    void OnShakeComplete()
    {
        transform.position= basePos;
    }
}
