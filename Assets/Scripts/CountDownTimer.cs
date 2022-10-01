using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CountDownTimer : MonoBehaviour
{
    public float MaxTime = 1;
    public bool IsOn;
    public bool HasGameEnded;
    public bool HasReachedGoal = false;
    public static CountDownTimer Instance;
    [SerializeField] private int _lastGameScore = -1;

    public float MaxTimeInSeconds { get => MaxTime * 60.0f; set => MaxTime = value / 60.0f; }

    private bool _canPlayAudio = true;
    private GameObject _audioSource;
    private void Start()
    {
        if (Instance != this)
        {
            DontDestroyOnLoad(gameObject);
            if (Instance != null)
            {
                var text = ScoreManager.Instance.ScoreShower.GetComponent<TextMesh>();
                if (Instance.HasReachedGoal)
                {
                    text.text = @"Parabéns, você conseguiu chegar a ilha cagarra! " + '\n' +" E fez " + Instance._lastGameScore + " pontos!";
                    ScoreManager.Instance.ScoreShower.SetActive(true);
                }
                else
                {
                    text.text = "Você não chegou na ilha Cagarra a tempo...";
                    ScoreManager.Instance.ScoreShower.SetActive(true);
                }
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
                HasGameEnded = true;
            }
        }

        if (HasGameEnded)
        {
            if (_canPlayAudio)
            {
                _audioSource = AudioManager.PlaySound(AudioManager.Sound.CompleteTask, 1);
                _lastGameScore = ScoreManager.Instance.score;
                _canPlayAudio = false;
            }
            else if(_audioSource == null)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                IsOn = false;
                HasGameEnded = false;
            }
        }
    }
}
