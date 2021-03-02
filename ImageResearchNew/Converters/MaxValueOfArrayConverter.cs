using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageResearchNew.Converters
{
    public class MaxValueOfArrayConverter : ValueGenericConverter<double>
    {
        protected override Func<object, CultureInfo, double> ConvertMethod =>
            (value, culture) =>
            {
                if (value is IEnumerable)
                {
                    return ConvertTo<double>((value as IEnumerable).Cast<object>().Max());
                }
                return 0;
            };
    }
}
