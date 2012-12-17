// Save.cs
// 
//

namespace ImBatch.Actions
{
    using System.IO;
    using System.Xml;

    internal class Save : Action
    {
        public Save(XmlElement act)
        {
        }

        public override void Apply(ImageData img)
        {
            img.Image.Save(Path.Combine(img.Target, img.Filename));
        }
    }
}
