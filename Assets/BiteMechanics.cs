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

    public AnimationCurve scalarCurve;
    float scalarTime = 0f;
    public bool shouldGrow;

    public int totalBiteScore = 0;


    //bu fonksiyon animasyon üzerindeki bir trigger ile çaðrýlýyor ve parçalanma sekansýný baþlatýyor. Transition'a dikkat.
    public void BiteOtherCar()
    {
        if(isTargetStillInFrontForBite)
        {
            targetVehicle.ProceedToNextStage();
            controller.SlowDownForChew(biteTargetSpeed,0.33f,3);
            StartGrowth();
            totalBiteScore++;
        }
        else
        {
            CancelBiteAnimation();
            controller.ReturnToCruise();
        }
    }

    //bu fonksiyonu aracýn önüne takýlý görünmez gameobject'in colliderý trigger olunca oradaki DetectBiteTarget script'i çaðýrýyor.
    public void SetTargetCar(DestructibleVehicle target, float speed)
    {
        isTargetStillInFrontForBite = true;
        targetVehicle = target;
        biteTargetSpeed = speed;
        controller.SetStalkSpeed(biteTargetSpeed);
        anim.Play("NewMonsterCarBite");
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
