using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;

public class GamerManager : MonoBehaviour
{
    public static GamerManager _inst;

    public SpriteRenderer baseSpriteRenderer;
    public Color[] zoneColors;
    int currentZoneIndex;
    public Light2D ScreenLight;




    private void Awake()
    {
        _inst = this;
    }
    void Start()
    {
        InvokeRepeating(nameof(ChangeEnvColor), 5f, 30f);
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    void ChangeEnvColor()
    {
        currentZoneIndex++;
        if (currentZoneIndex == zoneColors.Length) currentZoneIndex = 0;
        baseSpriteRenderer.DOColor(zoneColors[currentZoneIndex], 0.7f);
        var lightColor = zoneColors[currentZoneIndex];
        lightColor.r *= 2f;
        lightColor.g *= 2f;
        lightColor.b *= 2f;
        ScreenLight.color = lightColor;

    }

   public void GameOver()
    {
        Invoke(nameof(RestartLevel), 1f);
    }
    void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
}
