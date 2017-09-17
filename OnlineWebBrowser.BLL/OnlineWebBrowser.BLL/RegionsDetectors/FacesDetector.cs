using System;
using System.Collections.Generic;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Cuda;
using Emgu.CV.Structure;
using OnlineWebBrowser.BLL.Interfaces;

namespace OnlineWebBrowser.BLL.RegionsDetectors
{
    public class FacesDetector : IRegionsDetector
    {
        public CascadeClassifier Face { get; set; }

        public FacesDetector(string haarcascade)
        {
            Face = new CascadeClassifier(haarcascade);
        }

        public void Detect(Mat image, List<Rectangle> faces)
        {
            var img = image.ToImage<Gray, Byte>();
            faces.AddRange(Face.DetectMultiScale(img, 1.1, 3));
        }
    }
}