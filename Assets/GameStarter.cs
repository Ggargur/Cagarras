using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    [SerializeField] float minForce = 1;

    [Header("References")]
    [SerializeField] InputActionReference leftControllerVelocity;
    [SerializeField] InputActionReference rightControllerVelocity;
    private bool IsFlappingWings()
    {
        var leftHandVelocity = leftControllerVelocity.action.ReadValue<Vector3>();
        var rightHandVelocity = rightControllerVelocity.action.ReadValue<Vector3>();

        Vector3 localVelocity = (leftHandVelocity + rightHandVelocity);
        if (localVelocity.sqrMagnitude > minForce * minForce && localVelocity.y < 0)
        {
            return true;
        }
        return false;
    }
    void Update()
    {
        if (IsFlappingWings())
        {
            SceneManager.LoadScene("CagarrasPlaceHolder");
        }
    }
}
