using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{

    float score = 0;
    public TextMeshProUGUI scoreTMP , highScoreTMP;

    // Update is called once per frame
    void Update()
    {
        highScoreTMP.text = "HighScore: " + PlayerPrefs.GetFloat("HighScore",0).ToString("0");
        score += Time.deltaTime;
        scoreTMP.text = score.ToString("0.00");
    }

    private void OnDestroy()
    {
        if (score > PlayerPrefs.GetFloat("HighScore", 0)) ;
        PlayerPrefs.SetFloat("HighScore", score);
    }
}
