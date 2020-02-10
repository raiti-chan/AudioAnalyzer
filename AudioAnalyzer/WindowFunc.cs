using AudioAnalyzer.Util;
using System;

namespace AudioAnalyzer {

	/// <summary>
	/// 窓関数の関数オブジェクト用インターフェイス
	/// </summary>
	internal interface IWindowFunction {
		/// <summary>
		/// 計算をし、値を返すメソッド
		/// </summary>
		/// <param name="x">x</param>
		/// <returns>窓関数の結果</returns>
		double Invoke(double x);

	}

	/// <summary>
	/// 窓関数オブジェクト
	/// </summary>
	internal static class WindowFunctions {

		/// <summary>
		/// 常に1を返す矩形窓関数
		/// </summary>
		public class RectangularWindowFunction : IWindowFunction {
			/// <summary>
			/// 常に1
			/// </summary>
			/// <param name="x">x</param>
			/// <returns>1</returns>
			public double Invoke(double x) => 1;
		}

		/// <summary>
		/// ハン窓
		/// </summary>
		public class HannWindowFunction : IWindowFunction {
			public double Invoke(double x) => 0.5 - 0.5 * Math.Cos(2.0 * Math.PI * x);
		}

		/// <summary>
		/// ハミング窓
		/// </summary>
		public class HammingWindowFunction : IWindowFunction {
			public double Invoke(double x) => 0.54 - 0.46 * Math.Cos(2.0 * Math.PI * x);
		}

		/// <summary>
		/// ブラックマン窓
		/// </summary>
		public class BlackmanWindowFunction : IWindowFunction {
			public double Invoke(double x) => 0.42 - 0.5 * Math.Cos(2.0 * Math.PI * x) + 0.08 * Math.Cos(4.0 * Math.PI * x);
		}

		/// <summary>
		/// バートレット-ハン窓
		/// </summary>
		public class BartlettHannWindowFunction : IWindowFunction {
			public double Invoke(double x) => 0.62 - 0.48 * Math.Abs(x - 0.5) - 0.38 * Math.Cos(2.0 * Math.PI * x);
		}

		/// <summary>
		/// ナット―ル窓
		/// </summary>
		public class NuttallWindowFunction : IWindowFunction {
			public double Invoke(double x) => 0.0355768 - 0.487396 * Math.Cos(2 * Math.PI * x) + 0.144232 * Math.Cos(4 * Math.PI * x) - 0.012604 * Math.Cos(6.0 + Math.PI * x);
		}

		/// <summary>
		/// ブラックマン-ハリス窓
		/// </summary>
		public class BlackmanHarrisWindowFunction : IWindowFunction {
			public double Invoke(double x) => 0.35875 - 0.48829 * Math.Cos(2.0 * Math.PI * x) + 0.14128 * Math.Cos(4.0 * Math.PI * x) - 0.01168 * Math.Cos(6.0 * Math.PI * x);
		}

		/// <summary>
		/// ブラックマン-ナット―ル窓
		/// </summary>
		public class BlackmanNuttallWindowFunction : IWindowFunction {
			public double Invoke(double x) => 0.3635819 - 0.4891775 * Math.Cos(2.0 * Math.PI * x) + 0.1365995 * Math.Cos(4.0 * Math.PI * x) - 0.0106411 * Math.Cos(6.0 * Math.PI * x);
		}

	}

	/// <summary>
	/// 窓関数の列挙型
	/// </summary>
	internal class WindowFunction : Enumeration {

		/// <summary>
		/// ID指定用
		/// </summary>
		private static int useId = 0;

		/// <summary>
		/// 矩形窓
		/// </summary>
		public static readonly WindowFunction RectangularWindow = new WindowFunction("矩形窓", new WindowFunctions.RectangularWindowFunction());

		/// <summary>
		/// ハン窓
		/// </summary>
		public static readonly WindowFunction HannWindow = new WindowFunction("ハン窓", new WindowFunctions.HannWindowFunction());

		/// <summary>
		/// ハミング窓
		/// </summary>
		public static readonly WindowFunction HammingWindow = new WindowFunction("ハミング窓", new WindowFunctions.HammingWindowFunction());

		/// <summary>
		/// ブラックマン窓
		/// </summary>
		public static readonly WindowFunction BlackmanWindow = new WindowFunction("ブラックマン窓", new WindowFunctions.BlackmanWindowFunction());

		/// <summary>
		/// バートレット-ハン窓
		/// </summary>
		public static readonly WindowFunction BartlettHannWindow = new WindowFunction("バートレット-ハン窓", new WindowFunctions.BartlettHannWindowFunction());

		/// <summary>
		/// ナット―ル窓
		/// </summary>
		public static readonly WindowFunction NuttallWindowFunction = new WindowFunction("ナット―ル", new WindowFunctions.NuttallWindowFunction());

		/// <summary>
		/// ブラックマン-ハリス窓
		/// </summary>
		public static readonly WindowFunction BlackmanHarrisWindow = new WindowFunction("ブラックマン-ハリス窓", new WindowFunctions.BlackmanHarrisWindowFunction());

		/// <summary>
		/// ブラックマン-ナット―ル窓
		/// </summary>
		public static readonly WindowFunction BlackmanNuttallWindow = new WindowFunction("ブラックマン-ナット―ル窓", new WindowFunctions.BlackmanNuttallWindowFunction());



		/// <summary>
		/// 窓関数名
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// 窓関数オブジェクト
		/// </summary>
		public IWindowFunction WindowFunc { get; }

		/// <summary>
		/// 関数オブジェクトと名前を指定して、要素を定義します
		/// </summary>
		/// <param name="name">窓関数名</param>
		/// <param name="windowFunction">窓関数オブジェクト</param>
		private WindowFunction(string name, IWindowFunction windowFunction) : base(useId++) {
			this.Name = name;
			this.WindowFunc = windowFunction;
		}

	}
}
