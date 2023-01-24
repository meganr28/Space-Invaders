using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScoreStartUI : MonoBehaviour
{
    Global globalObj;
    TextMeshProUGUI hiScoreText;

    // Start is called before the first frame update
    void Start()
    {
        hiScoreText = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        float highScore = PlayerPrefs.GetFloat("highScore");
        hiScoreText.text = highScore.ToString();
    }
}
