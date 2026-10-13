using UnityEngine;

public static class GameConstant
{
    public static class GameTag
    {
        public const string WALL = "Wall";
    }
    public static class CameraPriority
    {
        public static readonly int ACTIVE_PRIORITY = 10;
        public static readonly int DEACTIVE_PRIORITY = 1;
    }
    public static class ShaderProperties
    {
        public static readonly int DissolveID = Shader.PropertyToID("_DissolveThreshold");
        public static readonly int BaseColorID = Shader.PropertyToID("_BaseColor");
        public static readonly int EmissionID = Shader.PropertyToID("_EmissionColor");
    }
    public static class MathConstant
    {
        public static readonly float NORMALIZED_MIN = 0f;
        public static readonly float NORMALIZED_MAX = 1f;
    }
    public static class PlayerAnimation
    {
        private const string IS_MOVING = "IsMoving";
        private const string IS_SPRINT = "IsSprint";
        private const string DEAD = "Dead";
        private const string STAND_UP = "StandUp";

        public static readonly int MOVE_HASH = Animator.StringToHash(IS_MOVING);
        public static readonly int SPRINT_HASH = Animator.StringToHash(IS_SPRINT);
        public static readonly int DEAD_HASH = Animator.StringToHash(DEAD);
        public static readonly int STAND_UP_HASH = Animator.StringToHash(STAND_UP);
    }

    public static class EnvironmentAnimation
    {
        private const string OPEN = "Open";
        private const string CLOSE = "Close";

        public static readonly int OPEN_HASH = Animator.StringToHash(OPEN);
        public static readonly int CLOSE_HASH = Animator.StringToHash(CLOSE);
    }
}
