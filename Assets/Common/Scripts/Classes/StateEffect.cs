using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StateEffect
{
    public string label;
    public MonoBehaviour affectedScript;
    public GameObject affectedGameObject;
    public bool inverseInfluence;
}
