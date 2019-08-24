namespace conClass
{
    public class Line
    {
        public Line(char letter, int nextHeight, LineType type = LineType.End)
        {
            Letter = letter;
            NextHeight = nextHeight;
            Type = type;
        }

        public char Letter { get; set; }
        public int NextHeight { get; set; }
        public LineType Type { get; set; }
    }

    public enum LineType
    {
        Continue, Start, End
    }
}