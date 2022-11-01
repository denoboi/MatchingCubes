using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class StackableBase : MonoBehaviour, IStackable
{
    [field: SerializeField] public StackTypeSO StackType { get; set; }

    public IStacker Stacker { get; set; }

    public bool IsStacked { get; set; }
    public bool CanStack { get; set; }

    private MeshRenderer meshRenderer;
    public MeshRenderer MeshRenderer { get { return meshRenderer == null ? meshRenderer = GetComponentInChildren<MeshRenderer>() : meshRenderer; } }

    public bool IsImmune { get; set; }

    public virtual void OnEnable()
    {
        Events.OnSpeedBoostChanged.AddListener(OnSpeedBoostChanged);
    }

    public virtual void OnDisable()
    {
        Events.OnSpeedBoostChanged.RemoveListener(OnSpeedBoostChanged);
    }

    public virtual void Start()
    {
        CanStack = true;
        Initialise();
    }

    public virtual void OnStacked(IStacker stacker)
    {
        IsStacked = true;
        Stacker = stacker;
    }

    public virtual void OnUnstacked()
    {
        IsStacked = false;
        CanStack = false;
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

        ParticleSystem particle = Instantiate(StackType.ColorChangeParticle, transform.position, StackType.ColorChangeParticle.transform.rotation, transform);

        foreach (var p in particle.GetComponentsInChildren<ParticleSystem>())
        {
            var main = p.main;
            main.startColor = StackType.Color;
        }

        particle.Play();

        Initialise();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out IObstacle obstacle))
        {
            if (IsImmune)
            {
                if (IsStacked)
                    obstacle.Dispose();
                return;
            }

            else if (Stacker.Stacks.Count > 0)
            {
                obstacle.OnInteracted(this);
                IStackable lastStack = null;
                if (Stacker.Stacks.Count > 0)
                    lastStack = Stacker.Stacks.Last();

                Events.OnLastStackableChanged.Invoke(lastStack);
            }
        }
    }

    private void OnSpeedBoostChanged(bool isActive)
    {
        IsImmune = isActive;
    }
}
