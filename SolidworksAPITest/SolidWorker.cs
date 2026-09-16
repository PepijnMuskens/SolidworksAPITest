using SolidWorks.Interop.sldworks;

namespace SolidworksAPITest
{
    public class SolidWorker
    {
        SldWorks swApp;
        ModelDoc2 swModel;

        public SolidWorker()
        {
            swApp = SolidWorksSingleton.GetApplication();
            swModel = (ModelDoc2)swApp.ActiveDoc;
        }
        public void UpdateModel()
        {
            swModel.ForceRebuild3(false);
        }

        public void Setsize(float x, float y,  float z)
        {
            swModel.Parameter("Width@Sketch1").Value;
        }
    }
}
