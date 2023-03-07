using System;
using System.Collections.Generic;
using UnityEngine;

internal interface OnDropFunctions
{
    void SetOnDropFunc(Func<GameObject, bool> func);
    void ResetOnDropFunc();
}