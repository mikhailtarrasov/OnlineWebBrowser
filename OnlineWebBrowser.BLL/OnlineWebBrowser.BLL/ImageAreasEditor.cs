using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using OnlineWebBrowser.BLL.Interfaces;

namespace OnlineWebBrowser.BLL
{
    public class ImageRegionsEditor
    {
        private IRegionsDetector RegionsDetector { get; set; }
        private IFilter Filter { get; set; }

        public ImageRegionsEditor(IRegionsDetector regionsDetector, IFilter filter)
        {
            RegionsDetector = regionsDetector;
            Filter = filter;
        }

        public void EditImage(Mat image)           
        {  
            var regions = new List<Rectangle>();
            RegionsDetector.Detect(image, regions);

            Filter.Proccess(image, regions);
        }
    }
}
