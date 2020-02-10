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

		#region Private Field
		
		/// <summary>
		/// 画像データ
		/// </summary>
		private readonly byte[] bitmap;

		#endregion

		#region Property

		/// <summary>
		/// 画像の幅
		/// </summary>
		public int Width { get; }

		/// <summary>
		/// 画像の高さ
		/// </summary>
		public int Height { get; }

		#endregion

		/// <summary>
		/// 画像テクスチャの作成
		/// </summary>
		public AudioTexture(int width, int height) {
			this.Width = width;
			this.Height = height;
			this.bitmap = new byte[this.Width * this.Height * 4];
		}


		/// <summary>
		/// データを縦1列に書き込みます
		/// </summary>
		/// <param name="x">書き込む列(時間)</param>
		/// <param name="levels">スペクトラム</param>
		public void PutDataX(int x, double[] levels) {
			if (x >= this.Width) {
				return;
			}
			for (int i = 0; i < levels.Length; i++) {
				byte data = (byte)(Math.Min((int)(levels[i] * 255), 255));
				this.bitmap[(x + i * this.Width) * 4 + 2] = data;
				this.bitmap[(x + i * this.Width) * 4 + 3] = 255;
			}
		}

		/// <summary>
		/// 現在のテクスチャデータで、新しいBitmapSourceを生成します。
		/// </summary>
		/// <returns></returns>
		public BitmapSource CreateBitmapSource() => BitmapSource.Create(this.Width, this.Height, 96, 96, PixelFormats.Pbgra32, null, this.bitmap, this.Width * 4);



		/// <summary>
		/// テクスチャをファイルに書き出しします。
		/// </summary>
		/// <param name="fileName">書き出すファイル名</param>
		public void SaveTexture(string fileName) {
			BitmapSource bitmapSource = this.CreateBitmapSource();
			using (var stream = new FileStream(fileName, FileMode.Create)) {
				var encoder = new PngBitmapEncoder {
					Interlace = PngInterlaceOption.On
				};
				encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
				encoder.Save(stream);
			}
		}
		
	}
}
