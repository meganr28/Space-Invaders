using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeWarpUI : MonoBehaviour
{
    Global globalObj;
    TextMeshProUGUI timeWarpText;

    // Start is called before the first frame update
    void Start()
    {
        GameObject g = GameObject.Find("GlobalObject");
        globalObj = g.GetComponent<Global>();
        timeWarpText = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        timeWarpText.text = "TW:" + Global.timeWarpsGranted.ToString();
    }
}
