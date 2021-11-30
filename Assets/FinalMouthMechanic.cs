using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalMouthMechanic : MonoBehaviour
{
    public Transform mouthPiece;
    public GameObject fakeMouth;

    Quaternion mouthStartRotation;
    Quaternion mouthFullOpenRotation;
    public GameObject fullOpenMouthIndicator;
    float declineSpeed = 0.2f;
    float joltPerTouch = 10f;
    float timeSinceLastJolt = 0f;
    public float scoreReductionTimeperiod = 0.5f;


    public float baseBossMouthScore = 0;
    public float bossChanceToJolt;
    public float bossJoltInterval;
    public float currentBossJoltTimer;
    public float mouthScore;


    public bool isPlayer = false;

    public bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        mouthStartRotation = mouthPiece.rotation;
        mouthFullOpenRotation = fullOpenMouthIndicator.transform.rotation;
        if(!isPlayer)
        {
            mouthScore += baseBossMouthScore;
        }
        if(isPlayer)
        {
            fakeMouth.SetActive(false);
        }
    }

    public void MouthPieceDecline()
    {
        mouthPiece.rotation = Quaternion.RotateTowards(mouthPiece.rotation, mouthStartRotation, declineSpeed);
    }
    
    public void MouthJolt()
    {
        timeSinceLastJolt = 0;
        mouthPiece.rotation = Quaternion.RotateTowards(mouthPiece.rotation, mouthFullOpenRotation, joltPerTouch);
    }
    
    public void CloseMouth()
    {
        mouthPiece.rotation = mouthStartRotation;
        isGameOver = true;
    }


    // Update is called once per frame
    void Update()
    {
        if(!isGameOver)
        {
            MouthPieceDecline();
            timeSinceLastJolt += Time.deltaTime;
            if (timeSinceLastJolt >= scoreReductionTimeperiod)
            {
                timeSinceLastJolt = 0;
                mouthScore--;
            }
            if (isPlayer)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    MouthJolt();
                    mouthScore++;
                }
            }
            if (!isPlayer)
            {
                currentBossJoltTimer -= Time.deltaTime;
                if (currentBossJoltTimer <= 0)
                {
                    currentBossJoltTimer = bossJoltInterval;
                    float bossJoltDice = Random.Range(0f, 100f);
                    if (bossJoltDice <= bossChanceToJolt)
                    {
                        MouthJolt();
                        mouthScore++;

                    }
                }
            }
        }
    }
}
