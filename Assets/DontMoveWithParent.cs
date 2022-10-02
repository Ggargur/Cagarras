using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontMoveWithParent : MonoBehaviour
{
    Vector3 savedPosition;

    public bool dontMoveWithParent = true;
    bool lastDontMoveWithParent = true;

    Vector3 parentLastPos;

    private void Update()
    {
        if (transform.hasChanged && !transform.parent.hasChanged && savedPosition != transform.position)
        {
            savedPosition = transform.position;
        }

        if (!lastDontMoveWithParent && dontMoveWithParent)
            savedPosition = transform.position;

        lastDontMoveWithParent = dontMoveWithParent;
    }

    private void LateUpdate()
    {
        if (dontMoveWithParent)
        {
            if (savedPosition == Vector3.zero)
            {
                savedPosition = transform.position;
            }

            if (transform.parent.hasChanged)
            {
                transform.position = savedPosition;
            }
        }
    }
}