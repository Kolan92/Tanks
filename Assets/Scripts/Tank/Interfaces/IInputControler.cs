using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Tank.Interfaces {
    public interface IInputControler {
        void Update();
        Vector3 GetMovment(float speed, Vector3 transform);
        Quaternion GetTurn(float turnSpeed, Quaternion rotation);
    }
}
