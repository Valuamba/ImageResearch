using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ImageResearchNew.Converters
{
	/// <summary>
	/// Returns aggregate value of input values.
	/// </summary>
	public abstract class ValueGenericConverter<T> : BaseGenericConverter<T>, IValueConverter where T : new()
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			Debug.Assert(targetType.IsAssignableFrom(typeof(T)), $"targetType should be {typeof(T).FullName}");

			var tResult = new T();
			if (!Convert(value, culture, ref tResult))
				return DependencyProperty.UnsetValue;

			ApplyParameter(parameter, culture, ref tResult);
			return System.Convert.ChangeType(tResult, targetType);
		}

		public T ConvertTo<T>(object value)
        {
			var type = typeof(T);
			return type == typeof(object) || type.IsPrimitive
			? (T) System.Convert.ChangeType(value, type)
			: (T) TypeDescriptor.GetConverter(type).ConvertFrom(value);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}
