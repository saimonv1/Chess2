using Backend.Enums;

namespace Backend.Entities
{
    public class UnitRedDecorator : UnitDecorator
    {
        public UnitRedDecorator(Unit unit) : base(unit) { this.Label = "Red " + this.Label; }
        //public override string GetLabel()
        //{
        //    return $"Red " + base.GetLabel();
        //}
    }
}
