using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Way : MonoBehaviour
{
    
    [SerializeField] private Vector3 TurningSpeed;
    [SerializeField] private CollisionHandler handler;
    public Hoop[] hoop;

    [Header("Colors")]
    [SerializeField] private Color InicialColor;
    [SerializeField] private Color AchivedColor;
    [SerializeField] private Color NextColor;

    private MeshRenderer[] _childps;
    private Transform[] _childt;


    [System.NonSerialized] public int HowManyHoops;
    public int HoopsIndex = -1;

    public bool HasBeenFinished;

    private void Awake()
    {
        _childps = GetComponentsInChildren<MeshRenderer>();
        foreach(var mr in _childps)
        {
            mr.material.color = InicialColor;
        }

        _childt = GetComponentsInChildren<Transform>();

        hoop = GetComponentsInChildren<Hoop>();
        HowManyHoops = hoop.Length;
        ChangeNext();
    }

    public void ChangeNext()
    {
        if(HoopsIndex < HowManyHoops)
        {
            HoopsIndex++;
            if (HoopsIndex > 0)
            {
                hoop[HoopsIndex - 1].isNext = false;
                var oldTrails = _childps[HoopsIndex - 1].material;
                oldTrails.color = AchivedColor;
                StartCoroutine(DisableOld(hoop[HoopsIndex - 1]));
                AudioManager.PlaySound(AudioManager.Sound.CompleteTask, 0);
            }
            if(HoopsIndex < HowManyHoops)
            {
                hoop[HoopsIndex].isNext = true;
                var trail = _childps[HoopsIndex].material;
                trail.color = NextColor;
            }
        }
        if(HoopsIndex == HowManyHoops - 1)
            HasBeenFinished = true;
        handler.UpdateRestartPosition();
    }
    IEnumerator DisableOld(Hoop hoop)
    {
        yield return new WaitForSeconds(1f);
        hoop.gameObject.SetActive(false);
    }
}
