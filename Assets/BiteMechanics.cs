using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class BiteMechanics : MonoBehaviour
{
    DestructibleVehicle targetVehicle;
    Animator anim;
    CarController controller;
    float biteTargetSpeed;
    bool isTargetStillInFrontForBite = true;
    Vector3 lastGoodScale;
    FeverMechanics feverScript;

    public AnimationCurve scalarCurve;
    float scalarTime = 0f;
    public bool shouldGrow;

    public int totalBiteScore = 0;

    bool isFever = false;

    public void SetFeverOn()
    {
        isFever = true;
    }
    public void SetFeverOff()
    {
        isFever = false;
    }

    public void FeverBite()
    {
        if(isFever)
        {
            targetVehicle.EatenInFever();
            StartGrowth();

        }
    }


    //bu fonksiyon animasyon ?zerindeki bir trigger ile ?a?r?l?yor ve par?alanma sekans?n? ba?lat?yor. Transition'a dikkat.
    public void BiteOtherCar()
    {
        if(!isFever)
        {
            if (isTargetStillInFrontForBite)
            {
                targetVehicle.ProceedToNextStage();
                controller.SlowDownForChew(biteTargetSpeed, 0.33f, 3);
                StartGrowth();
                feverScript.AddFever(20);
                totalBiteScore++;
            }
            else
            {
                CancelBiteAnimation();
                controller.ReturnToCruise();
            }
        }

    }

    //bu fonksiyonu arac?n ?n?ne tak?l? g?r?nmez gameobject'in collider? trigger olunca oradaki DetectBiteTarget script'i ?a??r?yor.
    public void SetTargetCar(DestructibleVehicle target, float speed)
    {
        isTargetStillInFrontForBite = true;
        targetVehicle = target;
        biteTargetSpeed = speed;
        controller.SetStalkSpeed(biteTargetSpeed);
        anim.Play("NewMonsterCarBite");
    }

    public void CheckForStay(DestructibleVehicle stayTarget)
    {
        if(targetVehicle!=null)
        {
            if(stayTarget == targetVehicle)
            {
                isTargetStillInFrontForBite = true;
                //controller.SetStalkSpeed(biteTargetSpeed);
                anim.Play("NewMonsterCarBite");
            }
        }
    }

    public void StartGrowth()
    {
        lastGoodScale = transform.localScale;
        shouldGrow = true;
    }

    public void TargetAway()
    {
        isTargetStillInFrontForBite = false;
    }

    public void CancelBiteAnimation()
    {
        targetVehicle = null;
        anim.Play("Neutral");
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        controller = GetComponent<CarController>();
        feverScript = GetComponent<FeverMechanics>();
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldGrow)
        {
            transform.localScale = new Vector3(lastGoodScale.x*scalarCurve.Evaluate(scalarTime), lastGoodScale.y * scalarCurve.Evaluate(scalarTime), lastGoodScale.z * scalarCurve.Evaluate(scalarTime));
            scalarTime += Time.deltaTime;
            if(scalarTime>=1f)
            {
                shouldGrow = false;
                scalarTime = 0;
                lastGoodScale = transform.localScale;
            }
        }
    }
}
