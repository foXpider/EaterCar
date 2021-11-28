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
    public GameObject playerToDisable;
    CinemachineVirtualCamera runCam;

    public GameObject victoryScreen;
    public GameObject loseScreen;
    public GameObject bossBattleUI;
    Animator playerAnim;



    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("FoodCar"))
        {
            currentTarget = other.transform.root.GetComponent<DestructibleVehicle>();
            targetSpeed = other.transform.root.GetComponent<CarController>().speed;
            ReportCurrentTarget(currentTarget, targetSpeed);
        }
        if(other.CompareTag("Boss"))
        {
            FinalDuel(bossMouthScript,playerMouthScript,playerBiteMechanicsScript);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("FoodCar"))
        {
            biteScript.TargetAway();
        }
    }

    public void FinalDuel(FinalMouthMechanic bossMouth, FinalMouthMechanic playerMouth, BiteMechanics playerBiter)
    {
        if(bossMouth.mouthScore>=playerMouth.mouthScore + biteScript.totalBiteScore)
        {
            playerDestroyed.SetActive(true);
            playerToDisable.SetActive(false);
            runCam.Priority = 12;
            bossMouth.CloseMouth();
            loseScreen.SetActive(true);

        }
        else
        {
            bossDestroyed.SetActive(true);
            bossToDisable.SetActive(false);
            runCam.Priority = 12;
            playerMouth.CloseMouth();
            victoryScreen.SetActive(true);
            playerAnim.enabled = true;
            playerAnim.Play("MonsterCarBossKill");
        }
        bossBattleUI.SetActive(false);
    }

    public void ReportCurrentTarget(DestructibleVehicle vehicle, float speed)
    {
        biteScript.SetTargetCar(vehicle, speed);
    }

    private void Start()
    {
        playerMouthScript = GameObject.FindGameObjectWithTag("Player").GetComponent<FinalMouthMechanic>();
        bossMouthScript = GameObject.FindGameObjectWithTag("Boss").GetComponent<FinalMouthMechanic>();
        playerBiteMechanicsScript = GameObject.FindGameObjectWithTag("Player").GetComponent<BiteMechanics>();
        bossDestroyed = GameObject.FindGameObjectWithTag("BossExplosion");
        bossDestroyed.SetActive(false);
        playerDestroyed.SetActive(false);
        bossToDisable = GameObject.FindGameObjectWithTag("Boss").transform.GetChild(0).gameObject;
        playerToDisable = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject;
        runCam = GameObject.FindGameObjectWithTag("RunCam").GetComponent<CinemachineVirtualCamera>();
        victoryScreen = GameObject.FindGameObjectWithTag("VictoryScreen");
        victoryScreen.SetActive(false);
        loseScreen = GameObject.FindGameObjectWithTag("LoseScreen");
        loseScreen.SetActive(false);
        bossBattleUI = GameObject.FindGameObjectWithTag("BossBattleUI");
        bossBattleUI.SetActive(false);
        playerAnim = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();
    }


}
