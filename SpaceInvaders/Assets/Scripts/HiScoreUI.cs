using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HiScoreUI : MonoBehaviour
{
    Global globalObj;
    TextMeshProUGUI hiScoreText;

    // Start is called before the first frame update
    void Start()
    {
        GameObject g = GameObject.Find("GlobalObject");
        globalObj = g.GetComponent<Global>();
        hiScoreText = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        float highScore = PlayerPrefs.GetFloat("highScore");

        if (Global.isGameOver)
        {
            if (globalObj.score > highScore)
            {
                hiScoreText.text = string.Format("{0:0000}", globalObj.score);
                PlayerPrefs.SetFloat("highScore", globalObj.score);
            }
        }

        hiScoreText.text = highScore.ToString();
    }
}
