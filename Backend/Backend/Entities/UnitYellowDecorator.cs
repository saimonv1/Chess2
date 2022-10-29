using Backend.Enums;

namespace Backend.Entities
{
    public class UnitYellowDecorator : UnitDecorator
    {
        public UnitYellowDecorator(Unit unit) : base(unit) { this.Label = "Yellow " + this.Label; }
        //public override string GetLabel()
        //{
        //    return $"Yellow " + base.GetLabel();
        //}
    }
}
