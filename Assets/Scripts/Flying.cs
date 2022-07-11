using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Flying : MonoBehaviour
{
    [Header("Values")]
    public float wingFlapForce = 8f;
    [SerializeField] float dragForce = 0.1f;
    [SerializeField] float minForce = 1;
    [SerializeField] float minTimeBetweenFlaps = 0f;
    public float constantSpeed = 1f;
    [SerializeField] bool useGravity = true;
    //[SerializeField] private float OffsetReturnMenu = 20f;


    private Vector3 trueVelocity;
    public bool IsGamePaused = true;
    private bool CanFlap = true;

    [Header("References")]
    [SerializeField] InputActionReference leftControllerVelocity;
    [SerializeField] InputActionReference rightControllerVelocity;

    [SerializeField] Transform trackingReference;

    Rigidbody _rigidbody;


    public Vector3 IsFlappingWings()
    {
        var leftHandVelocity = leftControllerVelocity.action.ReadValue<Vector3>();
        var rightHandVelocity = rightControllerVelocity.action.ReadValue<Vector3>();

        Vector3 localVelocity = (leftHandVelocity + rightHandVelocity);
        if (localVelocity.sqrMagnitude > minForce * minForce && localVelocity.y < 0 && leftHandVelocity.magnitude > 0 && rightHandVelocity.magnitude > 0)
            return localVelocity;

        return Vector3.zero;
    }

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
        //_rigidbody.constraints = RigidbodyConstraints.FreezeRotation;   
    }

    void LateUpdate()
    {
        //print(transform.localRotation.eulerAngles);
        //if (IsGamePaused && transform.localRotation.eulerAngles.x >= 90 - OffsetReturnMenu && transform.localRotation.eulerAngles.x <= 270 - OffsetReturnMenu)
        //    transform.rotation = new Quaternion(0, 0, 0, 0);
        if (!IsGamePaused)
        {
            if (trackingReference.forward.y > 0)
                _rigidbody.AddForce(new Vector3(trackingReference.forward.x, 0, trackingReference.forward.z) * constantSpeed);
            else
                _rigidbody.AddForce(trackingReference.forward * constantSpeed);

            if (CanFlap)
            {
                StartCoroutine(UpdateCooldown());
                var _lv = IsFlappingWings();
                if (_lv != Vector3.zero)
                {
                    var worldVelocity = trackingReference.TransformDirection(_lv);
                    trueVelocity = new Vector3(0, -worldVelocity.y, 0);
                    GainAltitude();
                    AudioManager.PlayRandomSound(AudioManager.Sound.WingFlap);
                }
            }
            if (useGravity)
                _rigidbody.AddForce(new Vector3(0, -dragForce));
        }
    }

    IEnumerator UpdateCooldown()
    {
        CanFlap = false;
        yield return new WaitForSeconds(minTimeBetweenFlaps);
        CanFlap = true;
    }

    private void GainAltitude()
    {
        _rigidbody.AddForce(trueVelocity * wingFlapForce);
    }
}
