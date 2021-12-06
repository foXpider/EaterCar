using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class FeverMechanics : MonoBehaviour
{

    public float currentFever;
    public bool isFever = false;
    GameObject speedParticles;
    GameObject carFlames;
    CarController carCont;
    Slider feverBar;
    float feverDuration = 5f;
    float feverDecay;
    BiteMechanics biteScript;
    GameObject feverText;
    PostProcessVolume ppv;
    public Vignette vignetteLayer;


    public void AddFever(float feverAmount)
    {
        if(!isFever)
        {
            currentFever += feverAmount;
            currentFever = Mathf.Clamp(currentFever, 0, 100);
            feverBar.value = currentFever;
            //Debug.Log("New fever " + currentFever);
        }

    }
    public void RemoveFever(float feverAmount)
    {
        currentFever -= feverAmount;
        currentFever = Mathf.Clamp(currentFever, 0, 100);
        feverBar.value = currentFever;
    }

    public void EnterFever()
    {
        isFever = true;
        feverText.SetActive(true);
        speedParticles.SetActive(true);
        carFlames.SetActive(true);
        carCont.SetFeverModeOn();
        biteScript.SetFeverOn();
        vignetteLayer.enabled.value = true;
        vignetteLayer.active = true;

    }

    public void ResetFever()
    {
        if(!isFever)
        {
            currentFever = 0;
            currentFever = Mathf.Clamp(currentFever, 0, 100);
            feverBar.value = currentFever;
        }

    }
    public void HideFeverUI()
    {
        feverBar.gameObject.SetActive(false);
    }

    public void EndFever()
    {
        isFever = false;
        speedParticles.SetActive(false);
        carFlames.SetActive(false);
        carCont.SetFeverModeOff();
        biteScript.SetFeverOff();
        feverText.SetActive(false);
        vignetteLayer.enabled.value = false;
        vignetteLayer.active = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        speedParticles = GameObject.FindGameObjectWithTag("SpeedParticles");
        speedParticles.SetActive(false);
        carFlames = GameObject.FindGameObjectWithTag("CarFlames");
        carCont = GetComponent<CarController>();
        feverBar = GameObject.FindGameObjectWithTag("FeverBar").GetComponent<Slider>();
        feverDecay = 100 / feverDuration;
        carFlames.SetActive(false);
        biteScript = GetComponent<BiteMechanics>();
        feverText = GameObject.FindGameObjectWithTag("FeverText");
        feverText.SetActive(false);
        ppv = GameObject.FindGameObjectWithTag("PPV").GetComponent<PostProcessVolume>();
        ppv.profile.TryGetSettings(out vignetteLayer);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isFever)
        {
            if (currentFever >= 100)
            {
                EnterFever();
            }
        }

        if(isFever)
        {
            currentFever -= feverDecay * Time.deltaTime;
            feverBar.value = currentFever;
            if(currentFever<=0)
            {
                EndFever();
            }
        }
    }
}
