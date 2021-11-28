using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MoreMountains.NiceVibrations;


public class EaterCarManager : MonoBehaviour
{
    public int totalLevelCount;
    public int maxRandomLevel;
    public int minRandomLevel = 0;
    public int currentSceneIndex;
    public int lastActualLevel;
    public int levelToLoad;

    GameObject[] allFoodCars;
    CarController playerCar;


    public bool failed = false;
    public bool succeeded = false;


    public bool isTutorial = false;
    public int tutStep = 0;


    private static EaterCarManager _instance;
    public static EaterCarManager Instance
    {
        get
        {
            if (_instance == null)
            {

            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        totalLevelCount = SceneManager.sceneCountInBuildSettings;
        maxRandomLevel = SceneManager.sceneCountInBuildSettings;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;


        if (currentSceneIndex == 0)
        {
            SceneManager.LoadScene(1);
        }

        if (currentSceneIndex == 1)
        {
            //PlayerPrefs.SetInt("PlayerLevel",0);
            //PlayerPrefs.SetInt("LastActualLevel", 0);
            PlayerPrefs.SetInt("DisplayStartScreen", 1);

            if (PlayerPrefs.GetInt("LastActualLevel") != 0)
            {
                lastActualLevel = PlayerPrefs.GetInt("LastActualLevel");
                if (lastActualLevel < totalLevelCount)
                {
                    levelToLoad = lastActualLevel;
                }
                else
                {
                    levelToLoad = Mathf.FloorToInt(Random.Range(minRandomLevel, maxRandomLevel - 0.01f));
                }
                SceneManager.LoadScene(levelToLoad);
            }
            else
            {
                SceneManager.LoadScene(2);
            }

        }
        if (PlayerPrefs.GetInt("DisplayStartScreen") == 0)
        {
            if (GameObject.FindGameObjectWithTag("PreCanvas") != null)
            {
                GameObject.FindGameObjectWithTag("PreCanvas").gameObject.SetActive(false);
            }
        }
        if (PlayerPrefs.GetInt("DisplayStartScreen") == 1)
        {
            if (GameObject.FindGameObjectWithTag("PreCanvas") != null)
            {
                GameObject.FindGameObjectWithTag("PreCanvas").gameObject.SetActive(true);
            }
        }
    }
    public void StartLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1 && SceneManager.GetActiveScene().buildIndex != 0)
        {
            GameObject.FindGameObjectWithTag("PreCanvas").gameObject.SetActive(false);
        }
        foreach (GameObject g in allFoodCars)
        {
            g.SetActive(true);
        }
        playerCar.toggleBrakes();

    }

    public void NextLevel()
    {
        PlayerPrefs.SetInt("DisplayStartScreen", 0);
        PlayerPrefs.SetInt("LastActualLevel", currentSceneIndex + 1);
        int targetLevel = currentSceneIndex + 1;
        if (targetLevel + 1 > SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(Mathf.FloorToInt(Random.Range(3, maxRandomLevel - 0.01f)));
        }
        else
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }

    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void StartRace()
    {
        foreach (GameObject g in allFoodCars)
        {
            g.SetActive(true);
        }
        playerCar.toggleBrakes();
        GameObject.FindGameObjectWithTag("LevelStartScreen").SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        allFoodCars = GameObject.FindGameObjectsWithTag("DestructibleCar");
        playerCar = GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>();
        foreach (GameObject g in allFoodCars)
        {
            g.SetActive(false);
        }
        playerCar.toggleBrakes();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
