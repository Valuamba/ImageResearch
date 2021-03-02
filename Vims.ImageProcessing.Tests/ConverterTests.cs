using ImageResearchNew.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using LiveCharts;
namespace Vims.ImageProcessing.Tests
{
    [TestClass]
    public class ConverterTests
    {
        [TestMethod]
        public void Should_GetMaxValue_FromArray()
        {
            IEnumerable<double> array = new double[] { 2, 3, 4, 5, };
            ChartValues<int> arr = new ChartValues<int>() { 3, 5, 6, 7, 8 };
            var converter = new MaxValueOfArrayConverter();
            var obj = converter.Convert(arr, typeof(double), null, new System.Globalization.CultureInfo("en-Us"));
            //converter.Convert()
        }
    }
}
