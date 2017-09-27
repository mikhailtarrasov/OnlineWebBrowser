using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.CvEnum;
using OnlineWebBrowser.BLL.Filters;
using OnlineWebBrowser.BLL.RegionsDetectors;

namespace OnlineWebBrowser.BLL {
    public class FaceBlurrer
    {
        public void Blur(List<string> images, byte radius = 6)
        {
            var haarcascadeFrontalface = "C:\\Users\\Mikhail\\Source\\Repos\\OnlineWebBrowser\\OnlineWebBrowser.BLL\\OnlineWebBrowser.BLL\\RegionsDetectors\\haarcascade_frontalface_default.xml";
            var facesDetector = new FacesDetector(haarcascadeFrontalface);

            byte blurringRadius = radius;
            var blurring = new GaussianBlur(blurringRadius);

            var editor = new ImageRegionsEditor(facesDetector, blurring);

            foreach (var imagePath in images)
            {
                using (Mat image = CvInvoke.Imread(imagePath, ImreadModes.AnyColor)) 
                {
                    editor.EditImage(image);
                    CvInvoke.Imwrite(imagePath, image);
                }    
            }
            
        }
    }
}
