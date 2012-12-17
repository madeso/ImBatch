// File: ImageBatcher.cs
// License: 
// Please see readme.md

namespace ImBatch
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    internal class ImageBatcher
    {
        private readonly List<Action> actions = new List<Action>();

        public void Apply(long index, string source, string target, string filename)
        {
            var img = new ImageData(Path.Combine(source, filename), target, filename);
            img.Params["num"] = index.ToString();
            foreach (var a in this.actions)
            {
                a.Apply(img);
            }
        }

        public void LoadFromFile(string file)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(file);
            var root = doc["batch"];
            if (root == null)
            {
                throw new Exception("Missing root batch element");
            }
            var actions = root["actions"];
            if (actions == null)
            {
                throw new Exception("missing actions element");
            }

            foreach (var act in actions.ChildNodes)
            {
                var a = act as XmlElement;
                if (a != null)
                {
                    this.actions.Add(Action.Create(a));
                }
            }
        }
    }
}
