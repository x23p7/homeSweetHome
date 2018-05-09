﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class State
{
    public string stateLabel;
    public bool currentState;
    public StateEffect[] effects;
}