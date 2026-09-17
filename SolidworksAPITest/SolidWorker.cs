using System;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace SolidworksAPITest
{
    public class SolidWorker
    {
        private readonly SldWorks swApp;
        private ModelDoc2 swModel;

        public SolidWorker()
        {
            swApp = SolidWorksSingleton.GetApplication();
            swModel = swApp.ActiveDoc as ModelDoc2;
        }

        public void UpdateModel()
        {
            swModel = swApp.ActiveDoc as ModelDoc2;
            if (swModel == null) return;
            swModel.ForceRebuild3(false);
        }

        public void CreateCube(double sizeMm = 100.0)
        {
            // solidworks werkt blijkbaar in meters
            double size = sizeMm / 1000.0;

            string template = swApp.GetUserPreferenceStringValue(
                (int)swUserPreferenceStringValue_e.swDefaultTemplatePart);

            swModel.Extension.SelectByID2("Front Plane", "PLANE", 0, 0, 0, false, 0, null, 0);

            SketchManager skMgr = swModel.SketchManager;
            skMgr.CreateCornerRectangle(0, 0, 0, size, size, 0);

            swModel.Extension.SelectByID2("Sketch1", "SKETCH", 0, 0, 0, false, 0, null, 0);
            
            swModel.FeatureManager.FeatureExtrusion3(true, false, false, (int)swEndConditions_e.swEndCondBlind, (int)swEndConditions_e.swEndCondBlind, size, 0.0, false, false, false, false, 0.0, 0.0, false, false, false, false, true, true, true, (int)swStartConditions_e.swStartSketchPlane, 0.0, false);

        }
    }
}