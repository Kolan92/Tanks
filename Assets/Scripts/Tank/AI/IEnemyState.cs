namespace Assets.Scripts.Tank.AI {
    public interface IEnemyState {
        void UpdateState();
        void GoToNextState();
    }
}