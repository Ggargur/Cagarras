using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer Instance { get; private set; }
    [SerializeField] private GameObject TimerShower;
    public float timer = 0;
    public string FormatedTime { get { return string.Format("{0,2:00}:{1,2:00}:{2}", (int)(timer / 60), (int)(timer % 60), (int)((timer % 1) * 10000)); } }
    public bool isTimerOn = false;

    void Start()
    {
        if (timer == 0)
        {
            if (Instance != null)
            {
                var text = TimerShower.GetComponent<TextMesh>();
                if (text != null)
                {
                    TimerShower.SetActive(true);
                    text.text = "Parabéns, o último percurso foi feito em " + Instance.FormatedTime;
                    isTimerOn = false;
                    timer = 0;
                }
            }
            else
            {
                TimerShower.SetActive(false);
            }
            Instance = this;
            DontDestroyOnLoad(Instance.gameObject);
            StartCoroutine(AddTimer());
        }
    }
    
    private bool GetIsTimerOn()
    {
        return isTimerOn;
    }

    IEnumerator AddTimer()
    {
        yield return new WaitUntil(GetIsTimerOn);
        while (isTimerOn)
        {
            timer += Time.deltaTime;
            yield return null;
        }
    }

}
