using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilMechanics : MonoBehaviour
{



    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("biteScanner"))
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<CarController>().Skid();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
