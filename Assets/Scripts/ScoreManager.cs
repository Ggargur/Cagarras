using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public GameObject ScoreShower;
    public static ScoreManager Instance;

    void Awake()
    {
        if(Instance == null)
            Instance = this;
    }
}
