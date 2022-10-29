using Backend.Enums;

namespace Backend.Entities
{
    public class UnitGreenDecorator : UnitDecorator
    {
        public UnitGreenDecorator(Unit unit) : base(unit) { this.Label = "Green " + this.Label; }
        //public override string GetLabel()
        //{
        //    return $"Green " + base.GetLabel();
        //}
    }
}
