using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NextLevelUI : MonoBehaviour
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
        if (Global.levelWon)
        {
            gameOverText.text = "LEVEL WON \n\n Press 'X' to continue.";
        }
        else
        {
            gameOverText.text = "";
        }
    }
}
