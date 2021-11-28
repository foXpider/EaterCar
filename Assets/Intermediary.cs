using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intermediary : MonoBehaviour
{

    BiteMechanics biter;

    public void BiteOtherCar()
    {
        biter.BiteOtherCar();
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
