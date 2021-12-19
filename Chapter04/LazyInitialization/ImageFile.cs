﻿namespace LazyInitialization
{
    public class ImageFile
    {
        string fileName;
        public object LoadImage { get; set; }

        public ImageFile(string fileName)
        {
            this.fileName = fileName;
        }

        public object LoadImageFromDisk()
        {
            this.LoadImage = $"File {this.fileName} loaded from disk";
            return LoadImage;
        }
    }
}
