using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadShake : MonoBehaviour
{
    public Transform roadShakeTransform;

    public bool skipUpAndDown = true;
    public bool leanRightAndLeft = true;
    public float skipTarget;
    public bool isSkippingUp = false;
    public float shakeMinValue = 0.005f;
    public float shakeMaxValue = 0.02f;
    public bool alsoSwivel = false;
    public float skipSpeed = 0.3f;


    // Start is called before the first frame update
    void Start()
    {
        Vector3 roadshakecurrent = roadShakeTransform.localPosition;
        Debug.Log(roadShakeTransform.localPosition);
        skipTarget = Random.Range(roadshakecurrent.y + shakeMinValue, roadshakecurrent.y + shakeMaxValue);
        Debug.Log(skipTarget);
        isSkippingUp = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(skipUpAndDown)
        {
            Vector3 roadshakecurrent = roadShakeTransform.localPosition;
            if (isSkippingUp)
            {
                roadShakeTransform.localPosition = new Vector3(roadshakecurrent.x, roadshakecurrent.y + skipSpeed * Time.deltaTime, roadshakecurrent.z);
                if(roadshakecurrent.y > skipTarget)
                {
                    skipTarget = Random.Range(roadshakecurrent.y - shakeMaxValue, roadshakecurrent.y - shakeMinValue);
                    isSkippingUp = false;
                }
            }
            else
            {
                roadShakeTransform.localPosition = new Vector3(roadshakecurrent.x, roadshakecurrent.y - skipSpeed * Time.deltaTime, roadshakecurrent.z);
                if (roadshakecurrent.y < skipTarget)
                {
                    skipTarget = Random.Range(roadshakecurrent.y + shakeMinValue, roadshakecurrent.y + shakeMaxValue);
                    isSkippingUp = true;
                }
            }
        }
    }
}
