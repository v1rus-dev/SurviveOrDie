using System;

namespace Enemy
{
    [Serializable]
    public enum ZombieState
    {
        Idle,
        WalkToPoint,
        WalkToTarget,
        RunToTarget,
        Attack,
        Dead
    }
}