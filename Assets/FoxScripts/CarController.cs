using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class CarController : MonoBehaviour
{
    public float speed;
    //Rigidbody thisRigid;
    Transform thisTransform;

    public bool isPlayer = false;

    float slowDownPeriodLength;
    float slowSpeed;
    float stalkSpeed;
    public bool isSlowDown = false;
    public bool isStalking = false;
    public float recoveryPeriod = 1f;
    float speedStep;

    public bool isBraking = false;


    public void toggleBrakes()
    {
        if(isBraking)
        {
            isBraking = false;
        }
        else
        {
            isBraking = true;
        }
    }

    Animator anim;
    //Skidmarks skidder;


    // Start is called before the first frame update
    void Start()
    {
        //thisRigid = GetComponent<Rigidbody>();
        thisTransform = GetComponent<Transform>();
        anim= GetComponentInChildren<Animator>();
        //skidder = GameObject.FindGameObjectWithTag("Skids").GetComponent<Skidmarks>();
    }

    //Öndeki arabayý tespit etmeyle ýsýrma arasýnda geçen takip boyunca öndeki arabanýn hýzýnda ilerliyoruz.
    public void SetStalkSpeed(float stalk)
    {
        stalkSpeed = stalk;
        isStalking = true;
    }

    //Bunu nadiren kullanacaðýz
    public void SetSpeed(float targetSpeed)
    {
        speed = targetSpeed;
    }

    //Çiðneme esnasýndaki geçici yavaþlama, bu yavaþlama ile indiðimiz hýzda hiç kalmýyoruz, yavaþça hýzlanýyoruz.
    public void SlowDownForChew(float slowdownRatio,float recoveryDuration, int hapticStrengt)
    {
        slowSpeed = speed/ slowdownRatio;
        speedStep = speed / slowSpeed / recoveryDuration;
        isSlowDown = true;
        if(hapticStrengt ==1)
        {
            MMVibrationManager.Haptic(HapticTypes.LightImpact);
        }
        if (hapticStrengt == 2)
        {
            MMVibrationManager.Haptic(HapticTypes.MediumImpact);
        }
        if (hapticStrengt == 3)
        {
            MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
        }
        if (hapticStrengt == 4)
        {
            MMVibrationManager.Haptic(HapticTypes.Warning);
        }


    }

    //yaða basýnca kaymak
    public void Skid()
    {
        SlowDownForChew(2f, 2f,4);
        anim.Play("MonsterSkid");

    }

    public void ReturnToCruise()
    {
        isStalking = false;
        isSlowDown = false;
        SetSpeed(speed);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("FoodCar"))
        {
            SlowDownForChew(3f, 0.1f,1);
        }
    }



    // Update is called once per frame
    void Update()
    {
        if(!isBraking)
        {
            if (!isSlowDown && !isStalking)
            {
                thisTransform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
            if (isStalking && !isSlowDown)
            {
                thisTransform.Translate(Vector3.forward * stalkSpeed * Time.deltaTime);
            }
            if (isSlowDown)
            {
                thisTransform.Translate(Vector3.forward * slowSpeed * Time.deltaTime);
                slowSpeed = slowSpeed + speedStep * Time.deltaTime;
                //Debug.Log(slowSpeed);
                if (slowSpeed >= speed)
                {
                    isSlowDown = false;
                    isStalking = false;
                }

            }
        }

    }
}
