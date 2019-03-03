using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOrder
{
    bool Finished { get; }
    bool Interruptible { get; }
    void Perform();
}
