using System.Collections.Generic;
using System.Drawing;
using Emgu.CV;

namespace OnlineWebBrowser.BLL.Interfaces
{
    public interface IFilter
    {
        void Proccess(Mat image, List<Rectangle> regions);
    }
}