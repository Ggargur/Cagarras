using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWaitStart : MonoBehaviour
{
    public Flying Player;
    public Pooper Pooper;
    [SerializeField]  public GameObject Audios;
    public List<GameObject> Objectives;
    private int _counter;

    public void GameStart(bool state)
    {
        foreach (GameObject obj in Objectives)
            obj.SetActive(state);
        Player.IsGamePaused = !state;
        gameObject.SetActive(!state);
        Audios.SetActive(state);

        if(state)
            CountDownTimer.Instance.IsOn = state;
        //Timer.Instance.isTimerOn = state;
    }
    void Start()
    {
        GameStart(false);
    }

    void Update()
    {
        if (Pooper.isHoldingSelectors() || Input.GetKeyDown(KeyCode.Space))
            GameStart(true);
        //if (Player.IsFlappingWings() != Vector3.zero && _counter < 3)
        //    _counter++;
        //if (_counter >= 3 || )
        //    GameStart(true);
    }
}
