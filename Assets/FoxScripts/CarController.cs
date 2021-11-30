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


    public float laneSwitchRate = 0f;
    public float switchChancePerSecond;

    public float targetLaneXCoord;

    public bool leftLane = true;
    public bool isChangingLane = false;
    public float laneSwitchSpeed = 1f;

    GameObject fakeMouth;


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
        if(switchChancePerSecond!=0)
        {
            laneSwitchRate = switchChancePerSecond / 60f;
        }
        //skidder = GameObject.FindGameObjectWithTag("Skids").GetComponent<Skidmarks>();
        fakeMouth = GameObject.FindGameObjectWithTag("FakeTopMouth");
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

        fakeMouth.SetActive(false);
        SlowDownForChew(2f, 1f,4);
        anim.Play("MonsterSkid");

    }

    public void EndSkid()
    {
        fakeMouth.SetActive(true);
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

    public void ChangeLane()
    {
        if(leftLane)
        {
            targetLaneXCoord = Random.Range(1f, 3f);
        }
        else
        {
            targetLaneXCoord = Random.Range(-3f, -1f);
        }
        isChangingLane = true;
    }



    // Update is called once per frame
    void Update()
    {
        if(!isPlayer)
        {
            if(laneSwitchRate>0f)
            {
                if(!isChangingLane)
                {
                    float dice = Random.Range(0f, 100f);
                    if (dice < laneSwitchRate)
                    {
                        ChangeLane();
                    }
                }
                
            }
        }
        if(isChangingLane)
        {
            if(leftLane)
            {
                transform.position = new Vector3(transform.position.x + (laneSwitchSpeed * Time.deltaTime), transform.position.y, transform.position.z);
                if(transform.position.x>targetLaneXCoord)
                {
                    isChangingLane = false;
                    leftLane = false;
                }
            }
            else
            {
                transform.position = new Vector3(transform.position.x - (laneSwitchSpeed * Time.deltaTime), transform.position.y, transform.position.z);
                if (transform.position.x < targetLaneXCoord)
                {
                    isChangingLane = false;
                    leftLane = true;
                }
            }
        }

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
