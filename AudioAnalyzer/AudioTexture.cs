using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
		public static readonly int Height = 256;

		#region Private Field

		/// <summary>
		/// 画像データ
		/// </summary>
		private readonly WriteableBitmap bitmap;
		//private readonly Bitmap bitmap;

		#endregion

		#region Property
		
		public ImageBrush ImageBrush { get; set; }

		#endregion

		/// <summary>
		/// 画像テクスチャの作成
		/// </summary>
		public AudioTexture() => this.bitmap = new WriteableBitmap(Width, Height, 600, 600, PixelFormats.Pbgra32, null);
			//this.bitmap = new Bitmap(Width, Height);
		/*
		public void Put(int x, double[] levels) {
			if (x >= Width) {
				return;
			}
			byte[] pixels = new byte[Height * 4];
			for (int i = 0, index = 0; i < Height; i += 4, index++) {
				pixels[i + 2] = (byte)(Math.Min((int)(levels[index] * 255), 255));
				pixels[i + 3] = 255;
			}

			this.bitmap.WritePixels(new Int32Rect(0, 0, 1, Height), pixels, 4, x, 0);
		}
		*/

		public void Put(int x, int y, double level) {
			if (x >= Width || y >= Height) {
				return;
			}
			byte[] pixels = { 0, 0, 0, 255 };
			pixels[2] = (byte)Math.Min((int)(level * 255), 255);
			this.bitmap.WritePixels(new Int32Rect(0, 0, 1, 1), pixels, 4, x, y);
		}

		/*
		public void Put(int x, int y, double level) {
			if (x >= Width || y >= Height) {
				return;
			}

			this.bitmap.SetPixel(x, y, System.Drawing.Color.FromArgb(Math.Min((int)(level * 255), 255), 0, 0));
		}
		*/
		

		public void SaveTexture(string fileName) {
			//this.bitmap.Save(fileName);
			///*
			using (var stream = new FileStream(fileName, FileMode.Create)) {
				var encoder = new PngBitmapEncoder {
					Interlace = PngInterlaceOption.On
				};
				encoder.Frames.Add(BitmapFrame.Create(this.bitmap));
				encoder.Save(stream);
			}
			//*/
		}
	}
}
