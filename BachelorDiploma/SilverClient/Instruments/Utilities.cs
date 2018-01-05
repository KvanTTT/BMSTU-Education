using System;
using System.Numerics;
using System.Windows;
using System.Windows.Media;
using Saluse.MediaKit.Sample;
using KvantSound;

namespace Utilities
{
	public static class Colour
	{
		public static int BuildColour(byte alpha, byte red, byte green, byte blue)
		{
			return ((alpha << 24) | (red << 16) | (green << 8) | blue);
		}
	}

	public static class Array
	{
		public static int[] Fill(int[] array, int fillValue)
		{
			for (int index = 0; index < array.Length; index++)
			{
				array[index] = fillValue;
			}

			return array;
		}

		public static int[] Instantiate(int length, int fillValue)
		{
			return Fill(new int[length], fillValue);
		}
	}

	public static class UI
	{
		public static Point GetPosition(UIElement uiElement)
		{
			GeneralTransform generalTransform = uiElement.TransformToVisual(Application.Current.RootVisual as UIElement);
			return generalTransform.Transform(new Point(0, 0));
		}

		public static Point GetPosition(UIElement uiElement, UIElement relativeToUIElement)
		{
			GeneralTransform generalTransform = uiElement.TransformToVisual(relativeToUIElement);
			return generalTransform.Transform(new Point(0, 0));
		}
	}
	
	/// <summary>
	///  FFT peak calculations based on Cristian Ricciolo Civera's Direct Show for Silverlight: http://directshow4sl.codeplex.com/
	/// </summary>
	/// <summary>
	///  FFT peak calculations based on Cristian Ricciolo Civera's Direct Show for Silverlight: http://directshow4sl.codeplex.com/
	/// </summary>
	public static class PeakMeter
	{
		private static byte[] ComputeFFT(double[] fftRealInput, int sampleFrequency)
		{

			double[] fftRealOutput = new double[fftRealInput.Length];
			double[] fftImaginaryOutput = new double[fftRealInput.Length];
			double[] fftAmplitude = new double[fftRealInput.Length];

			FourierTransform.Compute(
							1024,
							fftRealInput,
							null,
							fftRealOutput,
							fftImaginaryOutput,
							false);

			FourierTransform.Norm(1024, fftRealOutput, fftImaginaryOutput, fftAmplitude);
			return FourierTransform.GetPeaks(fftAmplitude, null, sampleFrequency);
		}

		/*
		public static byte[] CalculateFrequencies(Int16[] samples, int sampleFrequency)
		{
			byte[] peaks;
			byte[] rightPeaks;
			double[] fftLeftRealInput = new double[samples.Length >> 1];
			double[] fftRightRealInput = new double[fftLeftRealInput.Length];
			for (int index = 0; index < fftLeftRealInput.Length; index++)
			{
				// Extract only the left channel
				fftLeftRealInput[index] = samples[(index * 2)];
				fftRightRealInput[index] = samples[((index * 2) + 1)];
			}

			peaks = ComputeFFT(fftLeftRealInput, sampleFrequency);
			rightPeaks = ComputeFFT(fftRightRealInput, sampleFrequency);

			for (int index = 0; index < peaks.Length; index++)
			{
				peaks[index] = Math.Max(peaks[index], rightPeaks[index]);
			}

			return peaks;
		}
		*/

		public static byte[] CalculateFrequencies(Complex[] Spectr, int sampleFrequency)
		{
			double[] SpectrRealPart = new double[Spectr.Length];
			for (int i = 0; i < SpectrRealPart.Length; i++)
				SpectrRealPart[i] = Spectr[i].Real;

			byte[] peaks = FourierTransform.GetPeaks(SpectrRealPart, null, sampleFrequency);

			return peaks;
		}
	}
}
