using UnityEngine;

public static class GameConstant
{
    public static class ShaderProperties
    {
        public static readonly int DissolveID = Shader.PropertyToID("_DissolveThreshold");
        public static readonly int ColorID = Shader.PropertyToID("_BaseColor");
    }

    public static class ShaderValue
    {
        public static readonly float MinValue = 0f;
        public static readonly float MaxValue = 1f;
    }

    public static class PlayerAnimation
    {
        private const string IS_MOVING = "IsMoving";
        private const string DEAD = "Dead";

        public static readonly int MOVE_HASH = Animator.StringToHash(IS_MOVING);
        public static readonly int DEAD_HASH = Animator.StringToHash(DEAD);
    }

    public static class EnvironmentAnimation
    {
        private const string OPEN = "Open";
        private const string CLOSE = "Close";

        public static readonly int OPEN_HASH = Animator.StringToHash(OPEN);
        public static readonly int CLOSE_HASH = Animator.StringToHash(CLOSE);
    }
}
