using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AudioAnalyzer {

	/// <summary>
	/// 音声ファイルオブジェクト
	/// </summary>
	internal class AudioFile {

		#region Private Field

		private readonly AudioFileReader audioStream;

		#endregion

		#region Property

		/// <summary>
		/// 音声ファイル名
		/// </summary>
		public string FileName { get; }

		/// <summary>
		/// 音声ファイルのサンプリングレート
		/// </summary>
		public int SamplingRate => this.audioStream.WaveFormat.SampleRate;

		/// <summary>
		/// 音声ファイルの長さ(秒)
		/// </summary>
		public int MusicLength => (int)Math.Ceiling((double)this.audioStream.Length / this.audioStream.WaveFormat.AverageBytesPerSecond);



		#endregion

		/// <summary>
		/// ファイル名を指定し、音声ファイルオブジェクトを初期化します。
		/// </summary>
		/// <param name="fileName">音声ファイル名</param>
		public AudioFile(string fileName) {
			this.FileName = fileName;
			this.audioStream = new AudioFileReader(fileName);
		}

		public Task ExportTextureAsync(IProgress<int> progress) => Task.Run(() => {
			progress.Report(50);
			Thread.Sleep(5000);
			progress.Report(100);
		});
	}
}
