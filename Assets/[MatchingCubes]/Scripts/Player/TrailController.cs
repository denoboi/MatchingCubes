using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailController : MonoBehaviour
{
    [SerializeField] private StackTypesData stackTypesDatabase;
    [SerializeField] private TrailRenderer trailPrefab;

    private Dictionary<StackTypeSO, TrailRenderer> trails;

    private TrailRenderer currentTrail;

    private void OnEnable()
    {
        Events.OnLastStackableChanged.AddListener(UpdateTrail);
    }

    private void OnDisable()
    {
        Events.OnLastStackableChanged.RemoveListener(UpdateTrail);
    }

    private void Awake()
    {
        trails = new Dictionary<StackTypeSO, TrailRenderer>();

        foreach (var type in stackTypesDatabase.StackTypes)
        {
            TrailRenderer trail = Instantiate(trailPrefab, transform.position + Vector3.up * 0.01f, trailPrefab.transform.rotation, transform);
            trail.emitting = false;
            trail.material.color = type.Color;
            trails.Add(type, trail);
        }
    }

    private void UpdateTrail(IStackable stack)
    {
        foreach (var trail in trails)
        {
            trail.Value.emitting = false;
        }

        if (stack == null)
        {
            currentTrail = null;
            return;
        }

        currentTrail = trails.GetValueOrDefault(stack.StackType);
        currentTrail.emitting = true;
    }


    public void ToggleTrail(bool value)
    {
        if (currentTrail == null) return;

        if (value)
            currentTrail.emitting = true;
        else
            currentTrail.emitting = false;
    }
}
