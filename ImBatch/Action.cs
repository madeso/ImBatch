// File: Action.cs
// License: 
// Please see readme.md

namespace ImBatch
{
    using System;
    using System.Collections.Generic;
    using System.Xml;

    internal abstract class Action
    {
        private static readonly Dictionary<string, Func<XmlElement, Action>> BuildActions = new Dictionary<string, Func<XmlElement, Action>>
            {
                {"save", act => new Actions.Save(act) },
                { "changeextension", act => new Actions.ChangeExtension(act) },
                { "resize", act => new Actions.Resize(act) },
                { "bw", act => new Actions.MakeBw(act) },
                { "rotateflip", act => new Actions.RotateFlip(act) },
                { "invert", act => new Actions.Invert(act) },
                { "colorfilter", act => new Actions.ColorFilter(act) },
                { "gamma", act => new Actions.Gamma(act) },
                { "brightness", act => new Actions.Brightness(act) },
                { "contrast", act => new Actions.Contrast(act) },
                { "crop", element => new Actions.Crop(element) },
                { "text", element => new Actions.Text(element) },
                { "image", element => new Actions.Image(element) },
                { "shape", element => new Actions.Shape(element) },
                { "rename", element => new Actions.Rename(element) }
            };

        // insert text, shape, image
        // enumerate
        public static Action Create(XmlElement act)
        {
            var type = act.Name;
            Func<XmlElement, Action> func;

            if (false == BuildActions.TryGetValue(type, out func))
            {
                throw new Exception("Not a known type: " + type + ", valid types are: " + Parsing.Expand(BuildActions.Keys));
            }
            return func(act);
        }

        public abstract void Apply(ImageData img);
    }
}

namespace ImBatch.Actions
{
}
