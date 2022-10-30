namespace Backend.Entities.TankBuilder;

public static class Director
{
    public static Unit ConstructTank(Builder builder, int posX, int posY) => 
        builder.AddWeaponry().AddMainBody().AddCoordinates(posX, posY).Build();
}