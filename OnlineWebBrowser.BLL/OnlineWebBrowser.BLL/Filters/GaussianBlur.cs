using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Cuda;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using OnlineWebBrowser.BLL.Interfaces;

namespace OnlineWebBrowser.BLL.Filters
{
    public class GaussianBlur : IFilter
    {
        public byte Radius { get; set; }

        public GaussianBlur(byte radius)
        {
            Radius = radius;
        }

        public void Proccess(Mat image, List<Rectangle> regions)
        {
            foreach (var region in regions) {
                var bufferImage = image.ToImage<Bgr, Byte>();
                bufferImage.ROI = region;
                var croppedImage = bufferImage.Copy();

                var blurredImg = new Mat();
                CvInvoke.GaussianBlur(croppedImage, blurredImg, Size.Empty, Radius);

                var xx = 0;
                var yy = 0;
                for (int y = region.Y; y < region.Y + region.Height - 1; y++) {
                    for (int x = region.X; x < region.X + region.Width - 1; x++) {
                        var p = blurredImg.Bitmap.GetPixel(xx, yy);
                        image.Bitmap.SetPixel(x, y, p);
                        xx++;
                    }
                    yy++;
                }
            }
        }
    }
}
