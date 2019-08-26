namespace conClass
{
    public class Line
    {
        public Line(char letter, int nextHeight, LineType type = LineType.Continue)
        {
            Letter = letter;
            NextHeight = nextHeight;
            Type = type;
        }

        public char Letter { get; set; }
        public int NextHeight { get; set; }
        public LineType Type { get; set; }
        public override string ToString() => $"{Letter} to {NextHeight}";
    }

    public enum LineType
    {
        Continue, Start, End
    }
}