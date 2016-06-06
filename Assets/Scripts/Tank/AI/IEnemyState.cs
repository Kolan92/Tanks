using Assets.Scripts.Tank.Enums;

namespace Assets.Scripts.Tank.AI {
    public enum StateStatus {
        Sucess,
        Failed,
        Running
    }

    public interface IEnemyState {
        StateStatus Status { get; set; }
        EnemyState NextState { get; set; }
        void UpdateState();
        void Execute();
    }
}