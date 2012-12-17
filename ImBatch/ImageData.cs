// File: ImageData.cs
// License: 
// Please see readme.md

namespace ImBatch
{
    using System.Collections.Generic;
    using System;

    internal class ImageData : IDisposable
    {
        public readonly string Source;
        public string Filename;
        public SuperImage Image;

        public Dictionary<string, string> Params = new Dictionary<string, string>();
        public string Target;

        public ImageData(string source, string target, string filename)
        {
            this.Source = source;
            this.Target = target;
            this.Filename = filename;
            this.Load();
        }

        public void Load()
        {
            CleanImage();
            this.Image = new SuperImage(this.Source);
        }

        private void CleanImage()
        {
            if (this.Image != null) this.Image.Dispose();
        }

        public void Dispose()
        {
            CleanImage();
        }
    }
}
