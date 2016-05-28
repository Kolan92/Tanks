using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Tank.Interfaces {
    public interface ITankModel {
        float HitPoints { get; set; }
        float Armor { get; set; }
        int Ammunition { get; set; }
        float Speed { get; set; }
        float Velocity { get; set; }
        string MovementAxisName { get; set; }
        string TurnAxisName { get; set; }
        Rigidbody Rigidbody { get; set; }
        float MovementInputValue { get; set; }
        float TurnInputValue { get; set; }
        int PlayerNumber { get; set; }
        bool IsComputerControled { get; set; }
    }
}
