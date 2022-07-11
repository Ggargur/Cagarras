using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Way : MonoBehaviour
{
    
    [SerializeField] private Vector3 TurningSpeed;
    [SerializeField] private Hoop[] hoop;

    [Header("Colors")]
    [SerializeField] private Color InicialColor;
    [SerializeField] private Color AchivedColor;
    [SerializeField] private Color NextColor;

    private ParticleSystem[] _childps;
    private Transform[] _childt;

    public bool HasBeenFinished;

    [System.NonSerialized] public int HowManyHoops;
    private int HoopsIndex = -1;
    private void Awake()
    {
        _childps = GetComponentsInChildren<ParticleSystem>();
        foreach(ParticleSystem ps in _childps)
        {
            var trails = ps.trails;
            trails.colorOverLifetime = InicialColor;
        }

        _childt = GetComponentsInChildren<Transform>();

        hoop = GetComponentsInChildren<Hoop>();
        HowManyHoops = hoop.Length;
        ChangeNext();
        StartCoroutine(TurnHoops());
    }

    public void ChangeNext()
    {
        if(HoopsIndex < HowManyHoops)
        {
            HoopsIndex++;
            if (HoopsIndex > 0)
            {
                hoop[HoopsIndex - 1].isNext = false;
                _childps[HoopsIndex - 1].Stop();
                var mainold = _childps[HoopsIndex - 1].main;
                mainold.loop = false;
                var oldTrails = _childps[HoopsIndex - 1].trails;
                oldTrails.colorOverLifetime = AchivedColor;
                StartCoroutine(DisableOld(hoop[HoopsIndex - 1]));
                AudioManager.PlaySound(AudioManager.Sound.CompleteTask, 0);
            }
            hoop[HoopsIndex].isNext = true;
            _childps[HoopsIndex].Play();
            var trail = _childps[HoopsIndex].trails;
            trail.colorOverLifetime = NextColor;
        }
        if(HoopsIndex == HowManyHoops - 1)
            HasBeenFinished = true;
    }
    IEnumerator TurnHoops()
    {
        while (true)
        {
            foreach (Transform _t in _childt)
                _t.Rotate(TurningSpeed * Time.deltaTime);
            transform.Rotate(-TurningSpeed * Time.deltaTime);
            yield return null;
        }
    }
    IEnumerator DisableOld(Hoop hoop)
    {
        yield return new WaitForSeconds(1f);
        hoop.gameObject.SetActive(false);
    }
}
