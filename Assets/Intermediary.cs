using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Intermediary : MonoBehaviour
{

    BiteMechanics biter;
    public CarController playerController;
    CinemachineVirtualCamera runCam;
    public DetectBiteTarget db;

    public void BiteOtherCar()
    {
        biter.BiteOtherCar();
    }

    public void KillDaBoss()
    {
        playerController.toggleBrakes();
        db.BossDestroyed();
        
    }

    public void EndSkid()
    {
        playerController.EndSkid();
    }


    // Start is called before the first frame update
    void Start()
    {
        biter = this.transform.parent.GetComponent<BiteMechanics>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
