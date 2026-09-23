using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolidworksAPITest
{
    public class SolidWorkerHelper
    {
        public Feature SimpleExtrudeBoss(ModelDoc2 swModel, double lengthMm)
        {
            Feature feature = swModel.FeatureManager.FeatureExtrusion3(
                true,
                false,
                true,
                (int)swEndConditions_e.swEndCondBlind,
                (int)swEndConditions_e.swEndCondBlind,
                lengthMm,
                0.0,
                false,
                false,
                false,
                false,
                0.0,
                0.0,
                false,
                false,
                false,
                false,
                true,
                true,
                true,
                (int)swStartConditions_e.swStartSketchPlane,
                0.0,
                false);

            return feature;
        }
    }
}
