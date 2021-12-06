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
    GameObject fakeMouth;
    GameObject realMouth;

    FeverMechanics feverScript;

    bool isStandoff = false;

    float standoffDuration = 4f;

    private void Awake()
    {
        allFoodCars = GameObject.FindGameObjectsWithTag("DestructibleCar");
    }

    // Start is called before the first frame update
    void Start()
    {

        playerCarContr = GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>();
        bossCarContr = GameObject.FindGameObjectWithTag("Boss").GetComponent<CarController>();
        bossBattleCam = GameObject.FindGameObjectWithTag("BossCam").GetComponent<CinemachineVirtualCamera>();
        bossBattlePlayerPosition = GameObject.FindGameObjectWithTag("BossBattlePlayerPos").transform;
        boss = GameObject.FindGameObjectWithTag("Boss");
        player = GameObject.FindGameObjectWithTag("Player");
        fakeMouth = GameObject.FindGameObjectWithTag("FakeTopMouth");
        realMouth = GameObject.FindGameObjectWithTag("TopMouth");
        feverScript = player.GetComponent<FeverMechanics>();

    }


    public void ClearAllOtherCars()
    {
        foreach (GameObject g in allFoodCars)
        {
            g.SetActive(false);
        }
        feverScript.EndFever();
        feverScript.HideFeverUI();
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
        player.GetComponentInChildren<Animator>().enabled = true;
        player.GetComponentInChildren<Animator>().Play("MonsterCarRoar");
        player.GetComponent<RoadShake>().enabled = false;
        playerCarContr.toggleBrakes();
        realMouth.SetActive(false);
        fakeMouth.SetActive(true);
        feverScript.EndFever();
        feverScript.HideFeverUI();
        isStandoff = true;

    }

    public void LaunchBothParties()
    {
        playerCarContr.SetSpeed(10f);
        playerCarContr.toggleBrakes();
        bossCarContr.SetSpeed(-10f);
    }

    // Update is called once per frame
    void Update()
    {
        if(isStandoff)
        {
            if(feverScript.isFever)
            {
                feverScript.EndFever();
                feverScript.HideFeverUI();
            }
            standoffDuration -= Time.deltaTime;
            if(standoffDuration<=0)
            {
                LaunchBothParties();
                isStandoff = false;
                Debug.Log("attaaaacckk!");
            }
        }
    }
}
