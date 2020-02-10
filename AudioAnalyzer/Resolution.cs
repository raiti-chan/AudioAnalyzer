using AudioAnalyzer.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioAnalyzer {
	internal class Resolution : Enumeration {
		/// <summary>
		/// ID指定用
		/// </summary>
		private static int useId = 0;

		/// <summary>
		/// 8
		/// </summary>
		public static Resolution X8 = new Resolution(8);

		/// <summary>
		/// 16
		/// </summary>
		public static Resolution X16 = new Resolution(16);
		
		/// <summary>
		/// 32
		/// </summary>
		public static Resolution X32 = new Resolution(32);
		
		/// <summary>
		/// 64
		/// </summary>
		public static Resolution X64 = new Resolution(64);
		
		/// <summary>
		/// 128
		/// </summary>
		public static Resolution X128 = new Resolution(128);

		/// <summary>
		/// 256
		/// </summary>
		public static Resolution X256 = new Resolution(256);
		
		/// <summary>
		/// 512
		/// </summary>
		public static Resolution X512 = new Resolution(512);

		/// <summary>
		/// 1024
		/// </summary>
		public static Resolution X1024 = new Resolution(1024);
		
		/// <summary>
		/// 2048
		/// </summary>
		public static Resolution X2048 = new Resolution(2048);
		
		/// <summary>
		/// 4196
		/// </summary>
		public static Resolution X4096 = new Resolution(4096);
		
		/// <summary>
		/// 8192
		/// </summary>
		public static Resolution X8192 = new Resolution(8192);


		/// <summary>
		/// 値
		/// </summary>
		public int Value { get; }

		/// <summary>
		/// 解像度を指定してインスタンスを初期化
		/// </summary>
		/// <param name="resolution"></param>
		private Resolution(int resolution) : base(useId++) => this.Value = resolution;
	}
}
