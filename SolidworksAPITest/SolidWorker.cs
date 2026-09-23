using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System.Diagnostics.Metrics;

namespace SolidworksAPITest
{
    public class SolidWorker
    {
        SldWorks swApp;
        ModelDoc2 swModel;
        ModelDocExtension swModelExt;
        EquationMgr swEquationMgr;
        SketchManager sketchManager;
        public SolidWorker()
        {
            swApp = SolidWorksSingleton.GetApplication();
            swModel = (ModelDoc2)swApp.ActiveDoc;
            swModelExt = swModel.Extension;
            swEquationMgr = swModel.GetEquationMgr();
            sketchManager = swModel.SketchManager;
        }
        public void UpdateModel()
        {
            swModel.ForceRebuild3(false);
        }

        public void Setsize(float x, float y, float z)
        {
            //swModel.Parameter("Width@Sketch1").Value;
        }

        public void CreateNewPart()
        {
            sketchManager.InsertSketch(true);

            sketchManager.CreateCenterRectangle(0, 0, 0, 1, 1, 1);

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

        public void CreateTussenplaatBoven(double a, double b, double c, double d)
        {
            //Get refrence to part to 2d sketch templates
            swModel.Extension.SelectByID2("TussenplaatBoven", "SKETCH", 0, 0, 0, false, 0, null, 0);

            //Extrude piece to plate length
            Feature extrude = swModel.FeatureManager.FeatureExtrusion3(true, false, false, (int)swEndConditions_e.swEndCondBlind, (int)swEndConditions_e.swEndCondBlind, a, 0.0, false, false, false, false, 0.0, 0.0, false, false, false, false, true, true, false, (int)swStartConditions_e.swStartSketchPlane, 0.0, false);


        }

        public void UpdateEquations(Dictionary<string,double> measurements)
        {
            if (swEquationMgr == null)
            {
                Console.WriteLine("Failed to get the equation manager");
                return;
            }
             
            for(int i = 0; i < swEquationMgr.GetCount(); i++)
            {
                if (swEquationMgr.GlobalVariable[i])
                {
                    string[] equation = swEquationMgr.Equation[i].Split("\"");
                    if (measurements.Keys.Contains(equation[1]) ){
                        //replace value with new value
                        string valueString = equation[2].Split("=")[1];
                        string newString = swEquationMgr.Equation[i].Replace(valueString, measurements[equation[1]].ToString());
                        long longEquation = 0;
                        longEquation = swEquationMgr.SetEquationAndConfigurationOption(i, newString, (int)swInConfigurationOpts_e.swAllConfiguration, null);
                            
                    }
                }

            }
            UpdateModel();
        }

        public void saveFile(string fileName)
        {
            AdvancedSaveAsOptions options = (AdvancedSaveAsOptions)swModelExt.GetAdvancedSaveAsOptions(7);
            options.SaveAllAsCopy = true;
            object ids;
            object names;
            object paths;

            options.GetItemsNameAndPath(out ids,out names,out paths);
            MessageBox.Show(ids + "\n" + names + "\n" + paths);
        }
    }
}
