using Backend.Enums;

namespace Backend.Entities
{
    public class UnitHealDecorator : UnitDecorator
    {
        public UnitHealDecorator(Unit unit) : base(unit) { this.Label = "H " + this.Label; }
        //public override string GetLabel()
        //{
        //    return $"Blue " + base.GetLabel();
        //}
    }
}
