namespace Core
{
    public class RangeUnit : BaseUnit
    {
        public override float AttackRange => 7.5f;
        public override float AttackDamage => 15.0f;
        public override float AttackCooldown => 1.5f;
        public override float VisionRange => 10.0f;
    }
}