using UnityEngine;
using UnityEngine.SceneManagement;

public class HasFinished : MonoBehaviour
{
    [Header("References")]
    public Way wayScript;
    public PoopObjective poopObjectiveScript;

    private GameObject audiosource;
    private bool gameOver = false;
    void Update()
    {
        if(wayScript.HasBeenFinished && poopObjectiveScript.HasBeenFinished && audiosource == null)
        {
            if (audiosource == null && gameOver)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                return;
            }
            audiosource = AudioManager.PlaySound(AudioManager.Sound.CompleteTask, 1);
            gameOver = true;
            print(Timer.Instance.FormatedTime);
            Timer.Instance.isTimerOn = false;
        }
    }
}
