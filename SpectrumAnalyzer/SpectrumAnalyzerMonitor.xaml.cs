using NAudio.Dsp;
using NAudio.Wave;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SpectrumAnalyzer {
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class SpectrumAnalyzerMonitor : Window {


		#region Private Field

		/// <summary>
		/// タイマーの更新スパン
		/// </summary>
		private static readonly long TimeSpan = 167000;

		/// <summary>
		/// ファイル名
		/// </summary>
		private static readonly string FileName = ".\\インドア系ならトラックメイカー.mp3";

		/// <summary>
		/// 最小音量
		/// </summary>
		//private static readonly double MinDB = -60.0;

		/// <summary>
		/// タイマー
		/// </summary>
		private DispatcherTimer _timer = null;

		/// <summary>
		/// 音楽データ
		/// </summary>
		private AudioFileReader _audioStream;

		/// <summary>
		/// 音声出力
		/// </summary>
		private WaveOutEvent _outputDevice;

		/// <summary>
		/// 音楽の長さ
		/// </summary>
		private int _musicLength;

		/// <summary>
		/// フーリエ変換後の音楽データ
		/// </summary>
		private float[,] _result;

		/// <summary>
		/// 波形表示のライン配列
		/// </summary>
		private Line[] _bar;

		/// <summary>
		/// 波形表示ラインのブラシ
		/// </summary>
		private Brush _brush;

		/// <summary>
		/// 描画する経過時間
		/// </summary>
		private int _drawTime;

		#endregion

		public SpectrumAnalyzerMonitor() {
			this.InitializeComponent();
			this.MouseLeftButtonDown += (sender, e) => this.DragMove();
			this.Loaded += new RoutedEventHandler(this.Window_Loaded);
		}

		/// <summary>
		/// Windowの初期化が終わった時呼ばれる
		/// </summary>
		/// <param name="sender">イベント送信元オブジェクト</param>
		/// <param name="e">イベント引数</param>
		private void Window_Loaded(object sender, RoutedEventArgs e) {

			// タイマーの初期化
			this._timer = new DispatcherTimer(DispatcherPriority.Normal) {
				Interval = new TimeSpan(TimeSpan)
			};
			this._timer.Tick += new EventHandler(this.TickUpdate);


			this._audioStream = new AudioFileReader(FileName);  // 音声ファイルストリームの生成
			this._result = this.Fft();                          // 高速フーリエ変換

			this._brush = new SolidColorBrush(Color.FromArgb(128, 61, 221, 200));   // 描画用ラインのブラシ

			// 描画用ラインの生成
			this._bar = new Line[this._result.GetLength(1)];
			for (int i = 0; i < this._bar.Length; i++) {
				Line line = new Line() {
					HorizontalAlignment = HorizontalAlignment.Left,
					VerticalAlignment = VerticalAlignment.Center,
					Stroke = this._brush
				};
				this.grid.Children.Add(line);
				this._bar[i] = line;  // 配列の要素の初期化
			}

			

			this._audioStream.Position = 0;  // フーリエ変換で移動したポジションを0に
			this._outputDevice = new WaveOutEvent();
			this._outputDevice.Init(this._audioStream);

			this._musicLength = (int)this._audioStream.Length / this._audioStream.WaveFormat.AverageBytesPerSecond; //音楽の長さ(秒)

			this._outputDevice.Play();

			this._timer.Start();

		}

		/// <summary>
		/// 波形データを高速フーリエ変換し、配列に結果を格納します。
		/// </summary>
		/// <returns>高速フーリエ変換されたデータ</returns>
		private float[,] Fft() {
			int fftLength = 256; // サンプルのデータ長
			int fftPos = 0;      // サンプルのイテレータ

			float[] samples = new float[this._audioStream.Length / this._audioStream.BlockAlign * this._audioStream.WaveFormat.Channels];
			float[,] result = new float[samples.Length / fftLength, fftLength / 2];
			Complex[] buffer = new Complex[fftLength];

			this._audioStream.Read(samples, 0, samples.Length); // 波形データの読み込み

			for (int i = 0; i < samples.Length; i++) {
				buffer[fftPos].X = (float)(samples[i] * FastFourierTransform.HammingWindow(fftPos, fftLength));
				buffer[fftPos].Y = 0.0f;
				fftPos++;
				if (fftLength <= fftPos) {
					fftPos = 0;

					int m = (int)Math.Log(fftLength, 2.0);
					FastFourierTransform.FFT(true, m, buffer);

					for (int j = 0; j < result.GetLength(1); j++) {
						double diagonal = Math.Sqrt(buffer[j].X * buffer[j].X + buffer[j].Y * buffer[j].Y);
						//double intensityDB = 10.0 * Math.Log(diagonal);
						//double percent = (intensityDB < MinDB) ? 1.0 : intensityDB / MinDB;

						result[i / fftLength, j] = (float)diagonal;

					}

				}
			}
			return result;
		}

		private void TickUpdate(object sender, EventArgs e) {
			this._drawTime = (int)((this._audioStream.Position / (double)this._audioStream.Length) * this._result.GetLength(0));
			this.PrintSpectrum();
		}

		private void PrintSpectrum() {
			Console.Write("Update :");
			Console.WriteLine(this._drawTime);
			if (this._drawTime >= this._result.GetLength(0)) {
				return;
			}

			for (int i = 0; i < this._result.GetLength(1); i++) {
				Line current = this._bar[i];

				current.X1 = i * 7 + 32;
				current.X2 = i * 7 + 32;

				current.Y1 = 0;
				current.Y2 = Math.Min(7700 * this._result[this._drawTime, i], 400);
			}

		}

		/// <summary>
		/// メニューのExitが押されたときアプリケーションを終了させる
		/// </summary>
		/// <param name="sender">イベント送信元オブジェクト</param>
		/// <param name="e">イベント引数</param>
		private void Exit_Clicked(object sender, RoutedEventArgs e) => this.Close();

	}

}
