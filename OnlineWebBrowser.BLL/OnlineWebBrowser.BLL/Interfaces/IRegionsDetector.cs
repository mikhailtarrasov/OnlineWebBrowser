using System.Collections.Generic;
using System.Drawing;
using Emgu.CV;

namespace OnlineWebBrowser.BLL.Interfaces
{
    public interface IRegionsDetector
    {
        void Detect(Mat image, List<Rectangle> regions);
    }
}