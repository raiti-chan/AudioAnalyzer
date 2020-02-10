using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using AudioAnalyzer.UI.Command;

namespace AudioAnalyzer.UI {
	/// <summary>
	/// ウィンドウのモデル
	/// </summary>
	internal class AudioAnalyzerModel : INotifyPropertyChanged {

		/// <summary>
		/// プロパティが変更されたとき、ViewModelに通知するイベント
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		#region Property

		/// <summary>
		/// 開いてる音声ファイル。開いてない場合はnullです。
		/// </summary>
		public AudioFile AudioFile { get; set; } = null;

		/// <summary>
		/// 音声ファイルのテクスチャ。未生成の場合nullです。
		/// </summary>
		public AudioTexture AudioTexture { get; set; } = null;

		/// <summary>
		/// 音声ファイルのテクスチャのブラシ。未生成の場合nullです。
		/// </summary>
		public ImageSource AudioTexSource { get; set; } = null;

		/// <summary>
		/// 選択中の窓関数
		/// </summary>
		public WindowFunction SelectedWindowFunction { get; set; } = WindowFunction.HammingWindow;

		//-------------------------------------------------------------------
		/// <summary>
		/// 周波数分解能
		/// </summary>
		private Resolution selectedSampleResolution = Resolution.X128;
		/// <summary>
		/// 周波数分解能
		/// </summary>
		public Resolution SelectedSampleResolution {
			get => this.selectedSampleResolution;
			set {
				if (this.selectedTextureResolutionY.Value < value.Value) {
					MessageBox.Show("周波数分解能はテクスチャの高さより大きくできません。", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
					this.selectedTextureResolutionY = value;
				}
				this.selectedSampleResolution = value;
				this.OnPropertyChanded(null);
			}
		}

		//-------------------------------------------------------------------
		/// <summary>
		/// 出力テクスチャのX解像度
		/// </summary>
		private Resolution selectedTextureResolutionX = Resolution.X8192;
		/// <summary>
		/// 出力テクスチャのX解像度
		/// </summary>
		public Resolution SelectedTextureResolutionX {
			get => this.selectedTextureResolutionX;
			set {
				this.selectedTextureResolutionX = value;
				this.OnPropertyChanded(null);
			}
		}

		//-------------------------------------------------------------------
		/// <summary>
		/// 出力テクスチャのY解像度
		/// </summary>
		private Resolution selectedTextureResolutionY = Resolution.X128;
		/// <summary>
		/// 出力テクスチャのY解像度
		/// </summary>
		public Resolution SelectedTextureResolutionY {
			get => this.selectedTextureResolutionY;
			set {
				if (this.selectedSampleResolution.Value > value.Value) {
					MessageBox.Show("テクスチャの高さは周波数分解能より小さくできません。", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
					this.selectedSampleResolution = value;
				}
				this.selectedTextureResolutionY = value;
				this.OnPropertyChanded(null);
			}
		}

		//-------------------------------------------------------------------
		/// <summary>
		/// 出力するライン数
		/// </summary>
		private int lineCount = 4;
		/// <summary>
		/// 出力するライン数
		/// </summary>
		public int LineCount {
			get => this.lineCount;
			set {
				this.lineCount = value;
				this.OnPropertyChanded(null);
			}
		}

		//-------------------------------------------------------------------
		/// <summary>
		/// FFTの解析に使うデータを連続的にするか否か
		/// </summary>
		private bool isContinuousData = false;
		/// <summary>
		/// FFTの解析に使うデータを連続的にするか否か
		/// </summary>
		public bool IsContinuousData {
			get => this.isContinuousData;
			set {
				this.isContinuousData = value;
				this.OnPropertyChanded(null);
			}
		}


		//-------------------------------------------------------------------
		/// <summary>
		/// 出力するX方向へのピクセル数
		/// </summary>
		public int OutputPixcelCount => this.selectedTextureResolutionX.Value * this.lineCount;

		/// <summary>
		/// 音声のサンプル数 / X方向への出力ピクセル数
		/// </summary>
		public float DataPerPixcel => this.AudioFile == null ? -1 : ((float)this.AudioFile.SampleLength / this.OutputPixcelCount);

		/// <summary>
		/// 出力するテクスチャの枚数
		/// </summary>
		public int OutputTextureCount => (int)Math.Ceiling(this.OutputPixcelCount / (this.selectedTextureResolutionX.Value * 4 *
			((float)this.selectedTextureResolutionY.Value / this.selectedSampleResolution.Value)));

		/// <summary>
		/// UIが操作可能か否か
		/// </summary>
		public bool IsEnableUI { get; set; } = true;

		/// <summary>
		/// プレビューボタンの状態
		/// </summary>
		public bool IsEnablePreview { get; set; } = false;

		#endregion

		#region Command

		/// <summary>
		/// 開くボタンのコマンド
		/// </summary>
		public SelectAudioFileCommand SelectAudioFileCommand { get; private set; } = new SelectAudioFileCommand();

		/// <summary>
		/// テクスチャを書き出すコマンド
		/// </summary>
		public ExportTextureCommand ExportTextureCommand { get; private set; } = new ExportTextureCommand();

		/// <summary>
		/// テクスチャ書き出しの進行度
		/// </summary>
		public int Progress { get; set; } = 0;

		#endregion

		#region Function

		/// <summary>
		/// Modelを初期化します。
		/// </summary>
		public AudioAnalyzerModel() {

		}

		/// <summary>
		/// プロパティの変更をViewに通知
		/// </summary>
		/// <param name="propertyName">変更されたプロパティ名</param>
		public void OnPropertyChanded(string propertyName) {
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

			this.SelectAudioFileCommand.OnCanExecuteChanged();
			this.ExportTextureCommand.OnCanExecuteChanged();
		}

		#endregion
	}
}
