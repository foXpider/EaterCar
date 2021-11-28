using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTargetCarFollow : MonoBehaviour
{

    public Transform followTarget;

    // Start is called before the first frame update
    void Start()
    {
        followTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, followTarget.position.y, followTarget.position.z);
    }
}
