using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Tank.Interfaces {
    public interface IInputControler {
        void Update();
        void Execute(float speed, float turnSpeed);
        //void Move(float speed);
        //void Turn(float turnSpeed);
    }
}
