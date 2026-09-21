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

        public void CreatePlate(double widthMm = 200.0, double heightMm = 100.0, double thicknessMm = 10.0, double holeRadiusMm = 5.0, double holeEdgeDistanceMm = 20.0)
        {
            // meters to millimeters
            double width = widthMm / 1000.0;
            double height = heightMm / 1000.0;
            double thickness = thicknessMm / 1000.0;
            double holeRadius = holeRadiusMm / 1000.0;
            double holeEdgeDistance = holeEdgeDistanceMm / 1000.0;

            // 1. Create a new part

            string template = swApp.GetUserPreferenceStringValue(
                (int)swUserPreferenceStringValue_e.swDefaultTemplatePart);

            swModel = (ModelDoc2)swApp.NewDocument(template, (int)swDwgPaperSizes_e.swDwgPaperA4size, 0, 0);

            SketchManager skMgr = swModel.SketchManager;

            // 2. Create plate sketch

            swModel.Extension.SelectByID2(
                "Front Plane",
                "PLANE",
                0,
                0,
                0,
                false,
                0,
                null,
                0);

            skMgr.InsertSketch(true);

            skMgr.CreateCornerRectangle(
                0,
                0,
                0,
                width,
                height,
                0);

            skMgr.InsertSketch(true);

            // 3. Extrude plate

            swModel.Extension.SelectByID2(
                "Sketch1",
                "SKETCH",
                0,
                0,
                0,
                false,
                0,
                null,
                0);

            swModel.FeatureManager.FeatureExtrusion3(
                true,       
                false,
                false,
                (int)swEndConditions_e.swEndCondBlind,
                (int)swEndConditions_e.swEndCondBlind,
                thickness,
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

            swModel.ClearSelection2(true);

            // 4. Select the top face

            // Z = thickness because the plate was extruded from Z = 0.
            swModel.Extension.SelectByID2(
                "",
                "FACE",
                width / 2.0,
                height / 2.0,
                thickness,
                false,
                0,
                null,
                0);

            // 5. Create hole sketch on top face

            skMgr.InsertSketch(true);

            // 6. Create four holes

            // bottom left
            skMgr.CreateCircle(
                holeEdgeDistance,
                holeEdgeDistance,
                0,
                holeEdgeDistance + holeRadius,
                holeEdgeDistance,
                0);

            // bottom right
            skMgr.CreateCircle(
                width - holeEdgeDistance,
                holeEdgeDistance,
                0,
                width - holeEdgeDistance + holeRadius,
                holeEdgeDistance,
                0);

            // top left
            skMgr.CreateCircle(
                holeEdgeDistance,
                height - holeEdgeDistance,
                0,
                holeEdgeDistance + holeRadius,
                height - holeEdgeDistance,
                0);

            // top right
            skMgr.CreateCircle(
                width - holeEdgeDistance,
                height - holeEdgeDistance,
                0,
                width - holeEdgeDistance + holeRadius,
                height - holeEdgeDistance,
                0);

            skMgr.InsertSketch(true);

            // 7. Cut the holes through the plate

            swModel.Extension.SelectByID2(
                "Sketch2",
                "SKETCH",
                0,
                0,
                0,
                false,
                0,
                null,
                0);

            swModel.FeatureManager.FeatureCut3(
                true,   
                false,  
                false,  
                (int)swEndConditions_e.swEndCondThroughAll,
                (int)swEndConditions_e.swEndCondThroughAll,
                0.0,    
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
                false,
                false,
                false,
                false,
                false,
                false,
                (int)swStartConditions_e.swStartSketchPlane,
                0.0,    
                false
            );
        }
    }
}