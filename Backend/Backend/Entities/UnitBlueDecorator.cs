using Backend.Enums;

namespace Backend.Entities
{
    public class UnitBlueDecorator : UnitDecorator
    {
        public UnitBlueDecorator(Unit unit) : base(unit) { this.Label = "Blue " + this.Label; }
        //public override string GetLabel()
        //{
        //    return $"Blue " + base.GetLabel();
        //}
    }
}
