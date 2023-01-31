using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissilesUI : MonoBehaviour
{
    Global globalObj;
    TextMeshProUGUI missilesText;

    // Start is called before the first frame update
    void Start()
    {
        GameObject g = GameObject.Find("GlobalObject");
        globalObj = g.GetComponent<Global>();
        missilesText = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        missilesText.text = "M:" + Global.missilesRemaining.ToString();

        if (globalObj.infiniteMissiles)
        {
            missilesText.color = Color.gray;
        }
        else
        {
            missilesText.color = Color.magenta;
        }
    }
}
