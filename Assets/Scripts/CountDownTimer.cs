using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownTimer : MonoBehaviour
{
    public float MaxTime = 1;
    public bool HasGameEnded;
    public static CountDownTimer Instance;

    public float MaxTimeInSeconds { get => MaxTime * 60.0f; set => MaxTime = value / 60.0f; }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        if(Instance != null)
        {
            var text = ScoreManager.Instance.ScoreShower.GetComponent<TextMesh>();
            ScoreManager.Instance.ScoreShower.SetActive(true);

            text.text = "Parabéns, Você conseguiu " + ScoreManager.Instance.score + " pontos!";
        }
        Instance = this;
    }
    void Update()
    {
        MaxTimeInSeconds -= Time.deltaTime;
        if (MaxTime <= 0)
            HasGameEnded = true;
    }
}
