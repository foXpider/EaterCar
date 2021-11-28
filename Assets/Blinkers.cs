using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Blinkers : MonoBehaviour
{
    
    Text blinkingText;
    public float blinkDuration = 0.5f;
    float currentTimer = 0f;
    bool isOn = true;

    // Start is called before the first frame update
    void Start()
    {
        blinkingText = this.GetComponent<Text>();
        currentTimer = blinkDuration;
    }

    // Update is called once per frame
    void Update()
    {
        currentTimer -= Time.deltaTime;
        if(currentTimer<=0 && isOn)
        {
            blinkingText.enabled = false;
            isOn = false;
            currentTimer = blinkDuration;
        }
        if (currentTimer <= 0 && !isOn)
        {
            blinkingText.enabled = true;
            isOn = true;
            currentTimer = blinkDuration;
        }
    }
}
