using System;
using System.Collections.Generic;

namespace SolidworksAPITest
{
    public class DeksloofCalculator
    {
        public record Point3D(double X, double Y, double Z);

        private readonly List<Point3D> coordinates;

        public DeksloofCalculator(List<Point3D> coordinates)
        {
            this.coordinates = coordinates;
        }

        public record DrawingValues(
            int DrawingNumber,
            double A,
            double B,
            double J,
            double C,
            double D,
            double E,
            double F,
            double G,
            double K
        );

        public List<DrawingValues> CalculateAllDrawingValues()
        {
            List<double> ankerSizes = CalculateAnkerSizes();

            List<DrawingValues> drawingValues = [];

            int numberOfDrawings = ankerSizes.Count / 2;

            for (int i = 0; i < numberOfDrawings; i++)
            {
                int drawingNumber = i + 1;

                int ankerSizeNumber = drawingNumber * 2;

                double selectedAnkerSize = ankerSizes[ankerSizeNumber - 1];

                DrawingValues values = CalculateValues(drawingNumber, selectedAnkerSize);

                drawingValues.Add(values);
            }

            return drawingValues;
        }

        private DrawingValues CalculateValues(int drawingNumber, double selectedAnkerSize)
        {
            double A = 5950;

            double C = 350;
            double D = 350;

            double B = (A - selectedAnkerSize) / 2;

            double J = A - B;

            double E = 600;
            double F = 2000;
            double G = 3200;
            double K = 4400;

            return new DrawingValues(
                drawingNumber + 35,
                A,
                B,
                J,
                C,
                D,
                E,
                F,
                G,
                K
            );
        }

        public List<double> CalculateAnkerSizes()
        {
            List<double> sizes = [];

            for (int i = 0; i < coordinates.Count - 1; i++)
            {
                double size = CalculateAnkerSize(
                    coordinates[i],
                    coordinates[i + 1]);

                sizes.Add(size);
            }

            return sizes;
        }

        private double CalculateAnkerSize(Point3D point1, Point3D point2)
        {
            double deltaX = CalculateDifference(point1.X, point2.X);

            double deltaY = CalculateDifference(point1.Y, point2.Y);

            return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }

        private double CalculateDifference(double a1, double a2)
        {
            return Math.Abs(a2 - a1);
        }
    }
}