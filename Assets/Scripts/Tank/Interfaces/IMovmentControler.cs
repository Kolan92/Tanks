using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Tank.Interfaces {
    public interface IMovmentControler {
        void Update();
        void Execute(float speed, float turnSpeed);
    }
}
