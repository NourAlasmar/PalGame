using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraManager : MonoBehaviour
{

    public static CameraManager _inst;
    public Camera mainCam;
    Vector3 targetRotation;
    public float CurrentCameraAngle;
    float baseOthSize = 6;

    private void Awake()
    {
        _inst = this;
    }




    // Start is called before the first frame update
    void Start()
    {
        baseOthSize = mainCam.orthographicSize;
        InvokeRepeating(nameof(ChangeCameraRotation), 10f, 10f);
    }

    void ChangeCameraRotation()
    {
        targetRotation = new Vector3(0, 0, Random.Range(-45f, 45f));
        transform.DOLocalRotate(targetRotation, 2f,RotateMode.Fast).OnUpdate(onDotweenUpdate).OnComplete(onDotweenComplete);
        
    }

    void onDotweenUpdate()
    {
        var angle = transform.rotation.eulerAngles.z;
        CurrentCameraAngle= Mathf.Repeat(180f + angle, 360f) - 180f;
        mainCam.orthographicSize = baseOthSize + Mathf.Abs(CurrentCameraAngle * 0.15f);
        
        
    }

    void onDotweenComplete()
    {
        mainCam.orthographicSize = baseOthSize + Mathf.Abs(targetRotation.z * 0.15f);
    }

}
