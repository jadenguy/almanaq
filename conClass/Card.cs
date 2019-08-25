namespace conClass
{
    public class Card : IConnectable
    {
        public Line[] SideA { get; set; } = new Line[3];
        public Line[] SideB { get; set; } = new Line[3];
        public bool Flipped = false;
        public override string ToString() => SideA[0].Letter.ToString();
        public Line GetLine(int height, bool top)
        {
            if (top ^ Flipped) { return SideA[height]; } else { return SideB[height]; }
        }
    }
}