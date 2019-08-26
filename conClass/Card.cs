using System.Text;

namespace conClass
{
    public class Card
    {
        public Line[] SideA { get; set; } = new Line[3];
        public Line[] SideB { get; set; } = new Line[3];
        public bool Flipped = false;
        public override string ToString() => SideA[0].Letter.ToString();
        public string Print(bool back = false)
        {

            var top = new StringBuilder();
            var middle = new StringBuilder();
            var bottom = new StringBuilder();
            Line[] side = SideA;
            if (back) { side = SideB; }
            top.Append("-");
            middle.Append("--");
            bottom.Append("---");
            top.Append(side[0].Letter);
            middle.Append(side[1].Letter);
            bottom.Append(side[2].Letter);
            top.Append("---");
            middle.Append("--");
            bottom.Append("-");
            if (side[0].NextHeight == 0) { top.Append("---"); }
            else { top.Append("\\"); }
            if (side[2].NextHeight == 2) { bottom.Append("---"); }
            else { bottom.Append("/"); }
            if (side[1].NextHeight == 0)
            {
                top.Append("/-");
                middle.Append("/");
            }
            else if (side[1].NextHeight == 2)
            {
                middle.Append("\\");
                bottom.Append("\\-");
            }
            else  { 
                top.Append(" /-");
                middle.Append(" X -");
                bottom.Append(" \\-"); 
                }


            System.Diagnostics.Debug.WriteLine(top);
            System.Diagnostics.Debug.WriteLine(middle);
            System.Diagnostics.Debug.WriteLine(bottom);
            top.AppendLine(middle.ToString());
            top.AppendLine(bottom.ToString());
            return top.ToString();
        }
        public Line GetLine(int height, bool top)
        {
            if (top ^ Flipped) { return SideA[height]; } else { return SideB[height]; }
        }
    }
}