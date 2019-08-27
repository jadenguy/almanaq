using System.Text;

namespace Loop
{
    public class Card
    {
        public Line[] SideA { get; set; } = new Line[3];
        public Line[] SideB { get; set; } = new Line[3];
        public bool Flipped = false;
        public override string ToString() => SideA[0].Letter.ToString() + SideB[0].Letter.ToString();
        public string[] Print(bool back = false)
        {
            var topRow = new StringBuilder();
            var middle = new StringBuilder();
            var bottom = new StringBuilder();
            Line[] side = SideA;
            if (back) { side = SideB; }
            topRow.Append("-");
            middle.Append("--");
            bottom.Append("---");
            topRow.Append(side[0].Letter);
            middle.Append(side[1].Letter);
            bottom.Append(side[2].Letter);
            topRow.Append("---");
            middle.Append("--");
            bottom.Append("-");
            if (side[0].NextHeight == 0)
            {
                topRow.Append("---");
                middle.Append("\\/-");
                bottom.Append("/\\-");
            }
            else if (side[1].NextHeight == 1)
            {
                topRow.Append("\\/----\\/-");
                middle.Append("/\\-\\/-/\\-");
                bottom.Append("---/\\----");
            }
            else if (side[2].NextHeight == 2)
            {
                topRow.Append("\\/-");
                middle.Append("/\\-");
                bottom.Append("---");
            }
            else if (side[1].NextHeight == 0)
            {
                topRow.Append("\\/----");
                middle.Append("/\\-\\/-");
                bottom.Append("---/\\-");
            }
            else if (side[1].NextHeight == 2)
            {
                topRow.Append("---\\/-");
                middle.Append("\\/-/\\-");
                bottom.Append("/\\----");
            }
            else { throw new System.Exception(); }
            return new string[] { topRow.ToString(), middle.ToString(), bottom.ToString() };
        }
        public Line GetLine(int height, bool top)
        {
            if (!top) { return SideB[height]; } else { return SideA[height]; }
        }
    }
}