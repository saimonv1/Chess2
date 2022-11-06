using Backend.Enums;

namespace Backend.Entities
{
    public class UnitDamageDecorator : UnitDecorator
    {
        public UnitDamageDecorator(Unit unit) : base(unit) { this.Label = "D " + this.Label; }
        //public override string GetLabel()
        //{
        //    return $"Blue " + base.GetLabel();
        //}
    }
}
