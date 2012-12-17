// Gamma.cs
// 
//

namespace ImBatch.Actions
{
    using System.Xml;

    internal class Gamma : Action
    {
        private readonly double blue;
        private readonly double green;
        private readonly double red;

        public Gamma(XmlElement act)
        {
            this.red = Parsing.ParseDouble(act, "red");
            this.green = Parsing.ParseDouble(act, "green");
            this.blue = Parsing.ParseDouble(act, "blue");
        }

        public override void Apply(ImageData img)
        {
            img.Image.SetGamma(this.red, this.green, this.blue);
        }
    }
}
