using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static Vector3 WorldToUISpace(this Vector3 worldPos, Canvas parentCanvas)
    {
        //Convert the world for screen point so that it can be used with ScreenPointToLocalPointInRectangle function
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        Vector2 movePos;

        //Convert the screenpoint to ui rectangle local point
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, screenPos, parentCanvas.worldCamera, out movePos);
        //Convert the local point to world point
        return parentCanvas.transform.TransformPoint(movePos);
    }

    public static T GetRandom<T>(this IList<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }

    public static Transform GetClosestTransform(this IList<Transform> list, Transform transformToCompare)
    {
        Transform closest = null;
        float lastDistance = Mathf.Infinity;
        float currentDistance = 0;

        foreach (Transform t in list)
        {
            currentDistance = Vector3.Distance(transformToCompare.position, t.position);
            if (currentDistance < lastDistance)
            {
                closest = t;
                lastDistance = currentDistance;
            }
        }

        return closest;
    }

    public static GameObject GetClosestGameObject(this IList<GameObject> list, GameObject gameObjectToCompare)
    {
        GameObject closest = null;
        float lastDistance = Mathf.Infinity;
        float currentDistance = 0;

        foreach (GameObject go in list)
        {
            currentDistance = Vector3.Distance(gameObjectToCompare.transform.position, go.transform.position);
            if (currentDistance < lastDistance)
            {
                closest = go;
                lastDistance = currentDistance;
            }
        }

        return closest;
    }
}
