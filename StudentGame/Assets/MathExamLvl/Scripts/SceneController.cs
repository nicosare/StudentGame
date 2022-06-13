using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class SceneController : MonoBehaviour
{
    private static GameObject[] endings = new GameObject[7];
    private static GameObject[] awards = new GameObject[7];
    public GameObject award;
    public GameObject progress;
    private static bool[] isEndingsCompleted = new bool[7];
    private static string saveEndings = "";
    private static string loadEndings = "";
    public GameObject finalHint;
    private static Text finalHintText;
    private static bool isSetup;
    public static bool haveEnd;

    private void Start()
    {
        finalHintText = finalHint.GetComponent<Text>();
        haveEnd = false;
        isSetup = false;


        for (var i = 0; i < progress.transform.childCount; i++)
        {
            endings[i] = progress.transform.GetChild(i).gameObject;
            awards[i] = award.transform.GetChild(i).gameObject;
        }

        for (var i = 0; i < isEndingsCompleted.Length; i++)
            isEndingsCompleted[i] = false;

        if (PlayerPrefs.HasKey("SavedEndings"))
        {
            saveEndings = "";
            loadEndings = PlayerPrefs.GetString("SavedEndings");
            SetupEndings();
        }
    }

    public static void RestartScene()
    {
        SaveLevel();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void AddEnding(int indexEnding)
    {
        if (isEndingsCompleted[indexEnding] == false)
        {
            isEndingsCompleted[indexEnding] = true;
            endings[indexEnding].GetComponent<Toggle>().isOn = true;
            awards[indexEnding].SetActive(true);
            saveEndings += indexEnding + " ";

            if (indexEnding == 3)
                finalHintText.text = "Í";

            if (!isSetup)
                haveEnd = true;
        }
        else haveEnd = true;
    }

    private static void SaveLevel()
    {
        PlayerPrefs.SetString("SavedEndings", saveEndings);
        PlayerPrefs.Save();
    }

    private static void SetupEndings()
    {
        var indexesEnding = loadEndings.Split(new char[1] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        isSetup = true;

        foreach (var i in indexesEnding)
            AddEnding(int.Parse(i));
        isSetup = false;

    }
}
