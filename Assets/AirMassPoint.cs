using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class AirMassPoint : MonoBehaviour
{
    [Header("Points")]
    public bool IsChained = false;
    public AirMassPoint Next;
    public Vector3 FirstTangent, SecondTangent, ThirdTangent;

    [Header("Values")]
    [SerializeField] bool BackAndForth = true;
    [SerializeField] float Interval = 1f;
    [SerializeField] float Frequency = 1f;

    private TrailRenderer trailRenderer;
    private Vector3 trailOriginalPos;

    [Header("Gizmos")]
    [SerializeField] float Radius = 1f;
    [SerializeField] float Width = 1f;

    [SerializeField] Color Color;
    [SerializeField] Texture2D Texture;

    private List<Transform> _children = new List<Transform>();

    void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        trailOriginalPos = transform.position;
        StartCoroutine(MoveTrail());
    }
    private void Update()
    {
        //if (Interval <= trailRenderer.time)
        //    trailRenderer.time = Interval;
    }

    private void OnDrawGizmosSelected()
    {
        var ccount = transform.childCount;
        if (ccount < 3)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
            _children.Clear();
            var firstchild = new GameObject("First Tangent", typeof(DontMoveWithParent)).transform;
            firstchild.parent = transform;
            firstchild.localPosition = Vector3.zero;
            _children.Add(firstchild);
            var secondchild = new GameObject("Second Tangent", typeof(DontMoveWithParent)).transform;
            secondchild.parent = transform;
            secondchild.localPosition = Vector3.zero;
            _children.Add(secondchild);

            if (IsChained) return;

            var thirdchild = new GameObject("Third Tangent", typeof(DontMoveWithParent)).transform;
            thirdchild.parent = transform;
            thirdchild.localPosition = Vector3.zero;
            _children.Add(thirdchild);
        }
    }
    void OnDrawGizmos()
    {
        if (_children.Count < 2)
        {
            var count = transform.childCount;
            if (count < 3) return;

            for (int i = 0; i < count; i++)
                _children.Add(transform.GetChild(i));
        }

        FirstTangent = _children[0].transform.position;
        SecondTangent = _children[1].transform.position;
        ThirdTangent = _children[2].transform.position;
        Gizmos.color = Color;
        Gizmos.DrawSphere(FirstTangent, Radius);
        Gizmos.DrawSphere(SecondTangent, Radius);

        if (!IsChained)
            Gizmos.DrawSphere(ThirdTangent, Radius);

        if (Next == null && IsChained) return;

        if (IsChained)
        {
            if (trailOriginalPos != Vector3.zero)
                Handles.DrawBezier(trailOriginalPos, Next.transform.position, FirstTangent, SecondTangent, Color, Texture, Width);
            else
                Handles.DrawBezier(transform.position, Next.transform.position, FirstTangent, SecondTangent, Color, Texture, Width);
        }
        else
        {
            if (trailOriginalPos != Vector3.zero)
                Handles.DrawBezier(trailOriginalPos, ThirdTangent, FirstTangent, SecondTangent, Color, Texture, Width);
            else
                Handles.DrawBezier(transform.position, ThirdTangent, FirstTangent, SecondTangent, Color, Texture, Width);
        }
    }

    IEnumerator MoveTrail()
    {
        if (Next == null && IsChained) yield break;
        bool directionpositive = true;
        float t = 0;
        while (true)
        {
            if (BackAndForth)
            {
                while (t <= 1f && t >= 0)
                {
                    if (directionpositive) t += Time.fixedDeltaTime * Frequency;
                    else t -= Time.fixedDeltaTime * Frequency;
                    transform.position = GiveBazierPath(t);
                    //transform.position = Vector3.Lerp(trailOriginalPos, Next.transform.position, t); // Linear Interpolation
                    yield return null;
                }
                yield return new WaitForSeconds(Interval);
                if (t > 1) t = 1;
                else if (t < 0) t = 0;
                directionpositive = !directionpositive;
            }
            else
            {
                t += Time.fixedDeltaTime * Frequency;
                transform.position = GiveBazierPath(t);
                if (t >= 1f)
                {
                    trailRenderer.enabled = false;
                    trailRenderer.emitting = false;
                    t = 0;
                    yield return new WaitForSeconds(Interval);
                    trailRenderer.enabled = true;
                    trailRenderer.emitting = true;
                }
                yield return null;
            }
        }
    }

    private Vector3 GiveBazierPath(float t)
    {
        float tSqrd, tCbud, inverset;
        tSqrd = t * t;
        tCbud = tSqrd * t;
        inverset = 1 - t;
        if (IsChained)
            return Mathf.Pow(inverset, 3) * trailOriginalPos + 3 * Mathf.Pow(inverset, 2) * t * FirstTangent + 3 * inverset * tSqrd * SecondTangent + tCbud * Next.transform.position;

        return Mathf.Pow(inverset, 3) * trailOriginalPos + 3 * Mathf.Pow(inverset, 2) * t * FirstTangent + 3 * inverset * tSqrd * SecondTangent + tCbud * ThirdTangent;
    }
}