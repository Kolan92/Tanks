using UnityEngine;
using System.Collections;
using Assets.Scripts.Tank.Interfaces;

public class TankModel : ITankModel {
    public float HitPoints { get; set; }
    public float Armor { get; set; }
    public int Ammunition { get; set; }
    public float Speed { get; set; }
    public float TurnSpeed { get; set; }
    public float Velocity { get; set; }
    public Rigidbody Rigidbody { get; set; }
    public int PlayerNumber { get; set; }
    public bool IsComputerControled { get; set; }

    public TankModel() {
        HitPoints = 100;
        Armor = 100;
        Ammunition = 100;
        Speed = 20;
        TurnSpeed = 180;
        Velocity = 1;
    }
}
