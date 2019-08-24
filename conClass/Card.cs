namespace conClass
{
    public class Card : IConnectable
    {
        public Line[] SideA { get; set; } = new Line[3];
        public Line[] SideB { get; set; } = new Line[3];
    }
}