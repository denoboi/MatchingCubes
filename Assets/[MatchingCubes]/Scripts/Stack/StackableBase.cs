using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StackableBase : MonoBehaviour, IStackable
{
    [field: SerializeField] public StackTypeSO StackType { get; set; }

    public IStacker Stacker { get; set; }

    public bool IsStacked { get { return Stacker == null ? false : true; } }

    private MeshRenderer meshRenderer;
    public MeshRenderer MeshRenderer { get { return meshRenderer == null ? meshRenderer = GetComponentInChildren<MeshRenderer>() : meshRenderer; } }

    public virtual void Start()
    {
        Initialise();
    }

    public virtual void OnStacked(IStacker stacker)
    {
        Stacker = stacker;
    }

    public virtual void OnUnstacked()
    {
        transform.SetParent(null);
    }

    public virtual void Dispose()
    {
        ParticleSystem particle = Instantiate(StackType.DisposeParticle, transform.position, Quaternion.identity);

        foreach (var p in particle.GetComponentsInChildren<ParticleSystem>())
        {
            var main = p.main;
            main.startColor = StackType.Color;
        }

        particle.Play();
        Stacker.RemoveStack(this);

        if (DOTween.IsTweening(gameObject.GetInstanceID()))
            DOTween.Kill(gameObject.GetInstanceID(), true);

        Destroy(gameObject);
    }

    public void Initialise()
    {
        if (StackType == null) return;

        MeshRenderer.material.color = StackType.Color;
    }

    public virtual void UpdateStackType(StackTypeSO stackType)
    {
        StackType = stackType;
        Initialise();
    }
}
