using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    Global globalObj;
    TextMeshProUGUI gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        GameObject g = GameObject.Find("GlobalObject");
        globalObj = g.GetComponent<Global>();
        gameOverText = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Global.isGameOver)
        {
            gameOverText.text = "GAME OVER";
        }
        else
        {
            gameOverText.text = "";
        }
    }
}
