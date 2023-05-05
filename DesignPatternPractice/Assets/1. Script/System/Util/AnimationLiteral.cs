using UnityEngine;

namespace  Util
{
    public class PlayerAnimationStringLiteral
    {
        public static readonly string Horizontal = "Horizontal";
        public static readonly string Vertical = "Vertical";
        public static readonly string IsMoveState = "isMoveState";
        public static readonly string IsRun = "isRun";
    }

    public class PlayerAnimationHashLiteral
    {
        public static readonly int Horizontal = Animator.StringToHash(PlayerAnimationStringLiteral.Horizontal);
        public static readonly int Vertical = Animator.StringToHash(PlayerAnimationStringLiteral.Vertical);
        public static readonly int IsMoveState = Animator.StringToHash(PlayerAnimationStringLiteral.IsMoveState);
        public static readonly int IsRun = Animator.StringToHash(PlayerAnimationStringLiteral.IsRun);
    }
}
