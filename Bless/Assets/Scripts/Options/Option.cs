using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    [Header("KeyBinds")]
    public KeyCode Use = KeyCode.Mouse0;
    public KeyCode Throw = KeyCode.Mouse1;
    public KeyCode Pick = KeyCode.E;
    public KeyCode Intract = KeyCode.E;
    public KeyCode Drop = KeyCode.R;
    public KeyCode Exchange = KeyCode.X;
    public KeyCode Run = KeyCode.Mouse3;
    public KeyCode Jump = KeyCode.Space;
    [Header("Menu Keys")]
    public KeyCode QuestLog = KeyCode.L;

    [Header("Options")]
    public bool HoldToRun;

    [Header("LOL")]
    public bool Penguin;
}
