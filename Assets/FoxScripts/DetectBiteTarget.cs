using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class DetectBiteTarget : MonoBehaviour
{
    public BiteMechanics biteScript;
    DestructibleVehicle currentTarget;
    float targetSpeed;
    public FinalMouthMechanic playerMouthScript;
    public FinalMouthMechanic bossMouthScript;
    public BiteMechanics playerBiteMechanicsScript;

    float playerMouthScore;
    float bossMouthScore;

    public GameObject playerDestroyed;
    public GameObject bossDestroyed;

    public GameObject bossToDisable;
    public Renderer[] playerToDisable;
    CinemachineVirtualCamera runCam;

    public GameObject victoryScreen;
    public GameObject loseScreen;
    public GameObject bossBattleUI;
    Animator playerAnim;

    CarController playerControl;

    public bool playerVictory = false;
    public bool finalCountDown = false;
    public float finalCount = 2f;




    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("FoodCar"))
        {

            currentTarget = other.transform.root.GetComponent<DestructibleVehicle>();
            targetSpeed = other.transform.root.GetComponent<CarController>().speed;
            ReportCurrentTarget(currentTarget, targetSpeed);

        }
        
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("FoodCar"))
        {
            biteScript.TargetAway();
        }
    }

    



    public void ReportCurrentTarget(DestructibleVehicle vehicle, float speed)
    {
        biteScript.SetTargetCar(vehicle, speed);
        biteScript.FeverBite();
    }

    private void Start()
    {
        playerMouthScript = GameObject.FindGameObjectWithTag("Player").GetComponent<FinalMouthMechanic>();
        bossMouthScript = GameObject.FindGameObjectWithTag("Boss").GetComponent<FinalMouthMechanic>();
        playerBiteMechanicsScript = GameObject.FindGameObjectWithTag("Player").GetComponent<BiteMechanics>();
        //bossDestroyed = GameObject.FindGameObjectWithTag("BossExplosion");
        //bossDestroyed.SetActive(false);
        playerDestroyed.SetActive(false);
        bossToDisable = GameObject.FindGameObjectWithTag("Boss").transform.GetChild(0).gameObject;
        playerToDisable = GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<Renderer>();
        runCam = GameObject.FindGameObjectWithTag("RunCam").GetComponent<CinemachineVirtualCamera>();
        victoryScreen = GameObject.FindGameObjectWithTag("VictoryScreen");
        //victoryScreen.SetActive(false);
        loseScreen = GameObject.FindGameObjectWithTag("LoseScreen");
        //loseScreen.SetActive(false);
        bossBattleUI = GameObject.FindGameObjectWithTag("BossBattleUI");
        //bossBattleUI.SetActive(false);
        playerAnim = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();
        playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>();

    }

    private void Update()
    {
        /*
        if(finalCountDown)
        {
            finalCount -= Time.deltaTime;
            if(finalCount<=0)
            {
                if(playerVictory)
                {
                    runCam.Priority = 12;
                    victoryScreen.SetActive(true);
                    playerControl.toggleBrakes();
                    finalCountDown = false;
                }
                else
                {
                    runCam.Priority = 12;
                    loseScreen.SetActive(true);
                    finalCountDown = false;
                }

            }
        }
        */
    }

}
