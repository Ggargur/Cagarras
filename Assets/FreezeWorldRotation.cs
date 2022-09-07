using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeWorldRotation : MonoBehaviour
{
    [SerializeField] bool FreezeOnX, FreezeOnY, FreezeOnZ, FreezeOnW;

    void LateUpdate()
    {
        if (!FreezeOnX && !FreezeOnY && !FreezeOnZ && !FreezeOnW)
            return;

        Quaternion newRotation = Quaternion.Euler(
            FreezeOnX ? -transform.parent.localEulerAngles.x : 0,
            FreezeOnY ? -transform.parent.localEulerAngles.y : 0,
            FreezeOnZ ? -transform.parent.localEulerAngles.z : 0);

        transform.localRotation = newRotation;
    }
}
