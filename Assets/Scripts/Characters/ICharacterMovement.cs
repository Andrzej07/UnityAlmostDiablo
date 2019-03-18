using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Characters
{
    public interface ICharacterMovement
    {
        void MoveTo(Vector3 point);
        void Stop();
        void Teleport(Vector3 point);
    }
}
