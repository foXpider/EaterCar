using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleVehicle : MonoBehaviour
{

    public List<GameObject> carStages;
    int currentStage = 0;

    public GameObject oilSpray;
    public GameObject oilSpill;

    Animator anim;




    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject g in carStages)
        {
            g.gameObject.SetActive(false);
        }
        carStages[0].gameObject.SetActive(true);
        currentStage = 0;
        anim = this.GetComponentInChildren<Animator>();
    }

    public void ProceedToNextStage()
    {
        if(currentStage!=carStages.Count)
        {
            currentStage++;
            anim.Play("FoodCarBitSwivel");
            foreach (GameObject g in carStages)
            {
                g.gameObject.SetActive(false);
            }
            if(currentStage==carStages.Count)
            {
                return;
            }
            else
            {
                carStages[currentStage].gameObject.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
