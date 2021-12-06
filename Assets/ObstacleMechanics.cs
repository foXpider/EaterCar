using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArionDigital;


public class ObstacleMechanics : MonoBehaviour
{
    public float slowdownRatio;
    public float slowdownDuration;
    public Rigidbody rb;
    public bool hasHit = false;
    public GameObject puff;
    public GameObject body;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("biteScanner"))
        {
            if (other.CompareTag("Player"))
            {
                if(!hasHit)
                {
                    if(!other.GetComponent<FeverMechanics>().isFever)
                    {
                        other.GetComponent<CarController>().SlowDownForChew(other.GetComponent<CarController>().speed / slowdownDuration, slowdownDuration, 2);
                        other.GetComponent<FeverMechanics>().RemoveFever(10f);
                        if (rb != null)
                        {
                            rb.isKinematic = false;
                            rb.AddForce(new Vector3(Random.Range(-10f, 10f), Random.Range(10f, 20f), Random.Range(10f, 20f)), ForceMode.Impulse);
                            rb.AddTorque(new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f)), ForceMode.Impulse);
                        }
                        hasHit = true;
                    }
                    else
                    {
                        Debug.Log(other.GetComponent<FeverMechanics>().isFever);
                        hasHit = true;
                        //body.SetActive(false);
                        GetComponent<Renderer>().enabled = false;
                        GetComponent<Collider>().enabled = false;
                        puff.SetActive(true);
                        if(TryGetComponent(out CrashCrate cc))
                        {
                            cc.enabled = false;
                        }
                        //other.GetComponent<BiteMechanics>().StartGrowth();
                    }

                }

            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        this.TryGetComponent<Rigidbody>(out rb);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
