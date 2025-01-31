﻿
/*===================================================================================
* 
*   Copyright (c) Userware/OpenSilver.net
*      
*   This file is part of the OpenSilver Runtime (https://opensilver.net), which is
*   licensed under the MIT license: https://opensource.org/licenses/MIT
*   
*   As stated in the MIT license, "the above copyright notice and this permission
*   notice shall be included in all copies or substantial portions of the Software."
*  
\*====================================================================================*/

using System;
using System.Windows.Markup;
using OpenSilver.Internal;

#if MIGRATION
namespace System.Windows.Media
#else
namespace Windows.UI.Xaml.Media
#endif
{
    /// <summary>
    /// An abstract class that describes a gradient, composed of gradient stops.
    /// Classes that derive from GradientBrush describe different ways of interpreting
    /// gradient stops.
    /// </summary>
    [ContentProperty(nameof(GradientStops))]
    public partial class GradientBrush : Brush
    {
        protected GradientBrush() { }

        private protected GradientBrush(GradientBrush original)
            : base(original)
        {
            MappingMode = original.MappingMode;
            SpreadMethod = original.SpreadMethod;
            foreach (GradientStop stop in original.GradientStops)
            {
                GradientStops.Add(new GradientStop { Offset = stop.Offset, Color = stop.Color });
            }
        }

        /// <summary>
        /// Gets or sets the brush's gradient stops, which is a collection of the GradientStop
        /// objects associated with the brush, each of which specifies a color and an offset
        /// along the brush's gradient axis. The default is an empty GradientStopCollection.
        /// </summary>
        public GradientStopCollection GradientStops
        {
            get { return (GradientStopCollection)GetValue(GradientStopsProperty); }
            set { SetValue(GradientStopsProperty, value); }
        }

        /// <summary>
        /// Identifies the GradientStops dependency property.
        /// </summary>
        public static readonly DependencyProperty GradientStopsProperty =
            DependencyProperty.Register(
                nameof(GradientStops),
                typeof(GradientStopCollection),
                typeof(GradientBrush),
                new PropertyMetadata(
                    new PFCDefaultValueFactory<GradientStop>(
                        static () => new GradientStopCollection(),
                        static (d, dp) =>
                        {
                            GradientBrush gb = (GradientBrush)d;
                            var collection = new GradientStopCollection();
                            collection.SetParentBrush(gb);
                            return collection;
                        }),
                    OnGradientStopsChanged,
                    CoerceGradientStops));

        private static void OnGradientStopsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GradientBrush gradientBrush = (GradientBrush)d;
            if (null != e.OldValue)
            {
                ((GradientStopCollection)e.OldValue).SetParentBrush(null);
            }
            if (null != e.NewValue)
            {
                ((GradientStopCollection)e.NewValue).SetParentBrush(gradientBrush);
            }
        }

        private static object CoerceGradientStops(DependencyObject d, object baseValue)
        {
            return baseValue ?? new GradientStopCollection();
        }

        /// <summary>
        /// Gets or sets a BrushMappingMode enumeration value that specifies whether
        /// the positioning coordinates of the gradient brush are absolute or relative
        /// to the output area.
        /// </summary>
        public BrushMappingMode MappingMode
        {
            get { return (BrushMappingMode)GetValue(MappingModeProperty); }
            set { SetValue(MappingModeProperty, value); }
        }
        /// <summary>
        /// Identifies the MappingMode dependency property.
        /// </summary>
        public static readonly DependencyProperty MappingModeProperty =
            DependencyProperty.Register("MappingMode", typeof(BrushMappingMode), typeof(GradientBrush), new PropertyMetadata(BrushMappingMode.RelativeToBoundingBox));



        /// <summary>
        /// Gets or sets the type of spread method that specifies how to draw a gradient
        /// that starts or ends inside the bounds of the object to be painted.
        /// The default is Pad.
        /// </summary>
        public GradientSpreadMethod SpreadMethod
        {
            get { return (GradientSpreadMethod)GetValue(SpreadMethodProperty); }
            set { SetValue(SpreadMethodProperty, value); }
        }

        /// <summary>
        /// Identifies the SpreadMethod dependency property.
        /// </summary>
        public static readonly DependencyProperty SpreadMethodProperty =
            DependencyProperty.Register("SpreadMethod", typeof(GradientSpreadMethod), typeof(GradientBrush), new PropertyMetadata(GradientSpreadMethod.Pad));


    }
}
