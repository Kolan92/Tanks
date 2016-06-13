using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Tank.Interfaces {
    public interface IInputManager {
        float Vertical { get; }
        float Horizontal { get; }
        bool FirePressed { get; }
    }
}
