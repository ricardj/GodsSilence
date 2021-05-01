using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command
{
    [HideInInspector]
    public bool isCommandCompleted;
    public CommandType commandType;
    public abstract IEnumerator ActivateCommand();

    public abstract IEnumerator UpdateCommandCallback();
}

public enum CommandType
{
    GOTO,
    ATTACK,
    ATTACK_WAIT,
    GOTO_WAIT,
    FOLLOW_TARGET,
}
