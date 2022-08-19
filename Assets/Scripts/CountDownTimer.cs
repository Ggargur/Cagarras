using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CountDownTimer : MonoBehaviour
{
    public float MaxTime = 1;
    public bool IsOn;
    public bool HasGameEnded;
    public static CountDownTimer Instance;
    [SerializeField] private int _lastGameScore = -1;

    public float MaxTimeInSeconds { get => MaxTime * 60.0f; set => MaxTime = value / 60.0f; }

    private GameObject _audiosource;
    private void Start()
    {
        if (Instance != this)
        {
            DontDestroyOnLoad(gameObject);
            if (Instance != null)
            {
                var text = ScoreManager.Instance.ScoreShower.GetComponent<TextMesh>();
                text.text = "Parabéns, você conseguiu " + Instance._lastGameScore + " pontos!";
                ScoreManager.Instance.ScoreShower.SetActive(true);
            }
        }
        Instance = this;
    }
    void Update()
    {
        if (IsOn)
        {
            MaxTimeInSeconds -= Time.deltaTime;
            if (MaxTime <= 0)
            {
                if (_audiosource == null && HasGameEnded)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    IsOn = false;
                    return;
                }
                if (!HasGameEnded)
                {
                    _audiosource = AudioManager.PlaySound(AudioManager.Sound.CompleteTask, 1);
                    _lastGameScore = ScoreManager.Instance.score;
                }
                HasGameEnded = true;
            }
        }
    }
}
