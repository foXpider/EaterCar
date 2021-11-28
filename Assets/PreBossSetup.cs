using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PreBossSetup : MonoBehaviour
{
    
    public GameObject levelProgressBar;
    public CarController playerCarContr;
    public CarController bossCarContr;
    public Transform bossBattlePlayerPosition;
    public CinemachineVirtualCamera bossBattleCam;
    public GameObject boss;
    public GameObject player;
    public GameObject[] allFoodCars;

    bool isStandoff = false;

    float standoffDuration = 4f;

    // Start is called before the first frame update
    void Start()
    {
        allFoodCars = GameObject.FindGameObjectsWithTag("DestructibleCar");
        playerCarContr = GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>();
        bossCarContr = GameObject.FindGameObjectWithTag("Boss").GetComponent<CarController>();
        bossBattleCam = GameObject.FindGameObjectWithTag("BossCam").GetComponent<CinemachineVirtualCamera>();
        bossBattlePlayerPosition = GameObject.FindGameObjectWithTag("BossBattlePlayerPos").transform;
        boss = GameObject.FindGameObjectWithTag("Boss");
        player = GameObject.FindGameObjectWithTag("Player");
    }


    public void ClearAllOtherCars()
    {
        foreach (GameObject g in allFoodCars)
        {
            g.SetActive(false);
        }
    }

    public void PreBoss()
    {

        levelProgressBar.SetActive(false);
        playerCarContr.SetSpeed(0);
        playerCarContr.gameObject.transform.position = bossBattlePlayerPosition.position;
        playerCarContr.gameObject.GetComponent<JoystickPlayerExample>().enabled = false;
        bossBattleCam.Priority = 11;
        boss.GetComponent<FinalMouthMechanic>().enabled = true;
        player.GetComponent<FinalMouthMechanic>().enabled = true;
        player.GetComponentInChildren<Animator>().enabled = false;
        player.GetComponent<RoadShake>().enabled = false;
        isStandoff = true;
    }

    public void LaunchBothParties()
    {
        playerCarContr.SetSpeed(10f);
        bossCarContr.SetSpeed(-10f);
    }

    // Update is called once per frame
    void Update()
    {
        if(isStandoff)
        {
            standoffDuration -= Time.deltaTime;
            if(standoffDuration<=0)
            {
                LaunchBothParties();
                isStandoff = false;
            }
        }
    }
}
