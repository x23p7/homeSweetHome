using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StateConnection
{
    public string stateLabel;
    public float currentValue;
    public List<MonoBehaviour> affectedScripts;
}
