using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWaitStart : MonoBehaviour
{
    public Flying Player;
    [SerializeField]  public GameObject Audios;
    public List<GameObject> Objectives;

    public void GameStart(bool state)
    {
        foreach (GameObject obj in Objectives)
            obj.SetActive(state);
        Player.IsGamePaused = !state;
        this.gameObject.SetActive(!state);
        Audios.SetActive(state);
        Timer.Instance.isTimerOn = state;
    }
    void Start()
    {
        GameStart(false);
    }

    void Update()
    {
        if (Player.IsFlappingWings() != Vector3.zero || Input.GetKeyDown(KeyCode.Space))
            GameStart(true);

    }
}
