using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMechanics : MonoBehaviour
{
    public float slowdownRatio;
    public float slowdownDuration;
    public Rigidbody rb;
    public bool hasHit = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("biteScanner"))
        {
            if (other.CompareTag("Player"))
            {
                if(!hasHit)
                {
                    other.GetComponent<CarController>().SlowDownForChew(other.GetComponent<CarController>().speed / slowdownDuration, slowdownDuration,2);
                    if (rb != null)
                    {
                        rb.isKinematic = false;
                        rb.AddForce(new Vector3(Random.Range(-10f, 10f), Random.Range(10f, 20f), Random.Range(10f, 20f)), ForceMode.Impulse);
                        rb.AddTorque(new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f)),ForceMode.Impulse);
                    }
                    hasHit = true;
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
