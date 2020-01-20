using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioAnalyzer {
	/// <summary>
	/// 音声テクスチャオブジェクト
	/// </summary>
	internal class AudioTexture {

		/// <summary>
		/// 画像の幅(px)
		/// </summary>
		public static readonly int Width = 8192;
		/// <summary>
		/// 画像の高さ(px)
		/// </summary>
		public static readonly int Hight = 128;

		#region Private Field
		/// <summary>
		/// 画像データ
		/// </summary>
		private Bitmap bitmap;

		#endregion

		/// <summary>
		/// 画像テクスチャの作成
		/// </summary>
		public AudioTexture() => this.bitmap = new Bitmap(Width, Hight);

		public void Put(int x, int y, double level) {
			if (x >= Width || y >= Hight) {
				return;
			}

			this.bitmap.SetPixel(x, y, Color.FromArgb(Math.Min((int)(level * 255), 255), 0, 0));
		}

		public void SaveTexture(string fileName) => this.bitmap.Save(fileName, ImageFormat.Png);
	}
}
