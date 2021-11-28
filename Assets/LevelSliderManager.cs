using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSliderManager : MonoBehaviour
{

    public Vector3 playerStart;
    public Vector3 bossPosition;
    public Vector3 currentPosition;
    public float currentRatio;
    public float totalDistance;
    public float currentDistance;
    public float unclampedDistance;
    Slider levelSlider;
    public PreBossSetup preBossScript;

    public GameObject bossWarningUI;
    bool bossWarningDisplayed = false;

    public GameObject bossBattleUI;
    bool bossBattleUIDispayed = false;


    Transform playerTransform;
    bool bossActivated = false;


    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerStart = playerTransform.position;
        bossPosition = GameObject.FindGameObjectWithTag("Boss").transform.position;
        totalDistance = (playerStart - bossPosition).magnitude;
        levelSlider = this.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        currentPosition = playerTransform.position;
        currentDistance = Mathf.Clamp((currentPosition - bossPosition).magnitude,0.01f,Mathf.Infinity);
        currentRatio = 1-(currentDistance / totalDistance); 

        levelSlider.value = currentRatio;

        if(!bossActivated)
        {
            if (currentDistance <= 10f)
            {
                bossActivated = true;
                preBossScript.PreBoss();
                bossWarningUI.SetActive(false);
                if(!bossBattleUIDispayed)
                {
                    bossBattleUI.SetActive(true);
                    bossBattleUIDispayed = true;
                }
            }
            if(currentDistance<=80f&&!bossWarningDisplayed)
            {
                bossWarningUI.SetActive(true);
                bossWarningDisplayed = true;
                preBossScript.ClearAllOtherCars();
            }
        }

    }
}
