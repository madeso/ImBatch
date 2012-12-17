// ChangeExtension.cs
// 
//

namespace ImBatch.Actions
{
    using System;
    using System.IO;
    using System.Xml;

    using Action = ImBatch.Action;

    internal class ChangeExtension : Action
    {
        private readonly string ext;

        public ChangeExtension(XmlElement act)
        {
            this.ext = act.GetAttribute("ext");
            if (string.IsNullOrWhiteSpace(this.ext))
            {
                throw new Exception("missing extension");
            }
        }

        public override void Apply(ImageData img)
        {
            img.Filename = Path.ChangeExtension(img.Filename, this.ext);
        }
    }
}
