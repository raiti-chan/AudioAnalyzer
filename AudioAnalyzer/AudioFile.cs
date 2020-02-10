using NAudio.Dsp;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
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
		/// 音声ファイルパス
		/// </summary>
		public string FilePath { get; }

		/// <summary>
		/// 音声ファイルのサンプリングレート
		/// </summary>
		public int SamplingRate => this.audioStream.WaveFormat.SampleRate;

		/// <summary>
		/// 音声ファイルのサンプル数
		/// </summary>
		public long SampleLength { get; }

		/// <summary>
		/// 音声ファイルの長さ
		/// </summary>
		public TimeSpan MusicTime { get; }

		#endregion

		/// <summary>
		/// ファイル名を指定し、音声ファイルオブジェクトを初期化します。
		/// </summary>
		/// <param name="filePath">音声ファイル名</param>
		public AudioFile(string filePath) {
			this.FilePath = filePath;
			this.FileName = Path.GetFileName(filePath);
			this.audioStream = new AudioFileReader(filePath) {
				Position = 0
			};
			this.SampleLength = this.audioStream.Length / this.audioStream.BlockAlign * this.audioStream.WaveFormat.Channels;
			double time = (double)this.audioStream.Length / this.audioStream.WaveFormat.AverageBytesPerSecond;
			this.MusicTime = new TimeSpan((long)(time * 10e6));
		}

		/// <summary>
		/// FFTを実行し、AudioTextureに書き出します。
		/// </summary>
		/// <param name="progress">進捗更新用のオブジェクト</param>
		/// <param name="texture">書き込むAudioTextureインスタンス</param>
		/// <returns></returns>
		public Task ExportTextureAsync(IProgress<int> progress, AudioTexture texture) => Task.Run(() => {
			progress.Report(0);

			int blockLength = 256; // サンプルブロックのデータ長
			float[] audioData = new float[this.audioStream.Length / this.audioStream.BlockAlign * this.audioStream.WaveFormat.Channels];
			Complex[] buffer = new Complex[blockLength]; // サンプル用バッファー

			this.audioStream.Read(audioData, 0, audioData.Length); // データを全部読み込んでるから重い
			#region old
			/*
			int fftPos = 0;
			int putPix = 0;
			for (int i = 0; i < audioData.Length; i++) {
				buffer[fftPos].X = (float)(audioData[i] * FastFourierTransform.HammingWindow(fftPos, blockLength));
				buffer[fftPos].Y = 0.0f;
				fftPos++;
				if (blockLength <= fftPos) {
					fftPos = 0;
					int m = (int)Math.Log(blockLength, 2.0);
					FastFourierTransform.FFT(true, m, buffer);

					for (int j = 0; j < blockLength; j++) {
						double diagonal = Math.Sqrt(buffer[j].X * buffer[j].X + buffer[j].Y * buffer[j].Y);
						texture.Put(putPix, j, diagonal * 100);
					}
					putPix++;
					progress.Report((int)((double)i / audioData.Length * 100));
				}
				
			}
			*/
			#endregion

			int pixcel = audioData.Length / 8192;
			int pixcelIndex = 0;
			for (int i = 0; i < audioData.Length; i += pixcel) {
				int audioDataStartIndex = i - (blockLength / 2); // サンプルブロックの先頭インデックス
				for (int j = 0; j < buffer.Length; j++) {
					int audioDataIndex = audioDataStartIndex + j; // サンプルデータのインデックス
					buffer[j].X = (float)(audioDataIndex < 0 ? 0 : audioDataIndex >= audioData.Length ? 0 : (audioData[audioDataIndex] * FastFourierTransform.HammingWindow(j, blockLength)));
					buffer[j].Y = 0;
				}

				int m = (int)Math.Log(blockLength, 2.0);
				FastFourierTransform.FFT(true, m, buffer);

				double[] levels = new double[buffer.Length / 2];
				for (int j = 0; j < levels.Length; j++) {
					double diagonal = Math.Sqrt(buffer[j].X * buffer[j].X + buffer[j].Y * buffer[j].Y);
					double intensityDB = 10.0 * Math.Log(diagonal);
					double percent = 1.0 - ((intensityDB < -60.0) ? 1.0 : intensityDB / -60.0);
					levels[j] = percent;
				}
				texture.PutDataX(pixcelIndex, levels);
				pixcelIndex++;
				progress.Report((int)((double)i / audioData.Length * 100));
			}

			progress.Report(100);

		});
	}
}
