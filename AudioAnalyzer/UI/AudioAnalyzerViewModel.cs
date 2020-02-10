using AudioAnalyzer.UI.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace AudioAnalyzer.UI {
	/// <summary>
	/// ViewModel
	/// </summary>
	internal class AudioAnalyzerViewModel : INotifyPropertyChanged {

		/// <summary>
		/// プロパティが変更されたときにViewに通知するイベント
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		#region Property
		/// <summary>
		/// Model
		/// </summary>
		public AudioAnalyzerModel Model { get; private set; }

		/// <summary>
		/// UIが操作可能か
		/// </summary>
		public bool IsEnableUI => this.Model.IsEnableUI;

		/// <summary>
		/// プレビューボタンの状態
		/// </summary>
		public bool IsEnablePreview => this.Model.IsEnablePreview;

		/// <summary>
		/// ファイル名
		/// </summary>
		public string FileName => this.Model.AudioFile == null ? "音声ファイルが選択されていません" : this.Model.AudioFile.FileName;

		/// <summary>
		/// 音声ファイルへのUri
		/// </summary>
		public Uri AudioFileUri => this.Model.AudioFile == null ? null : new Uri(this.Model.AudioFile.FilePath);

		/// <summary>
		/// 音声テクスチャのオーディオソース
		/// </summary>
		public ImageSource AudioTexSource => this.Model.AudioTexSource;

		/// <summary>
		/// サンプリングレート
		/// </summary>
		public string SamplingRate => this.Model.AudioFile == null ? "Null" : this.Model.AudioFile.SamplingRate.ToString();

		/// <summary>
		/// 音声ファイルの長さ(秒)
		/// </summary>
		public string MusicLength => this.Model.AudioFile == null ? "Null" : this.Model.AudioFile.MusicTime.TotalSeconds.ToString("F1") + " 秒";

		/// <summary>
		/// 音声ファイルのサンプル数
		/// </summary>
		public string MusicSampleLengh => this.Model.AudioFile == null ? "Null" : this.Model.AudioFile.SampleLength.ToString();

		/// <summary>
		/// 出力するX方向へのピクセル数
		/// </summary>
		public string OutputPixcelCount => this.Model.OutputPixcelCount.ToString();

		/// <summary>
		/// 音声のサンプル数 / X方向への出力ピクセル数
		/// </summary>
		public string DataPerPixcel => this.Model.DataPerPixcel == -1 ? "N/a" : this.Model.DataPerPixcel.ToString("F1");

		/// <summary>
		/// 出力するテクスチャの枚数
		/// </summary>
		public string OutputTextureCount => this.Model.OutputTextureCount.ToString();

		/// <summary>
		/// FFTの際に使用する窓関数の種類リスト
		/// </summary>
		public IEnumerable<WindowFunction> WindowFunctions { get; } = Util.Enumeration.GetAll<WindowFunction>();

		/// <summary>
		/// 解像度リスト
		/// </summary>
		public IEnumerable<Resolution> Resolutions { get; } = Util.Enumeration.GetAll<Resolution>();

		/// <summary>
		/// 選択中の窓関数
		/// </summary>
		public WindowFunction SelectedWindowFunction {
			get => this.Model.SelectedWindowFunction;
			set => this.Model.SelectedWindowFunction = value;
		}

		/// <summary>
		/// 周波数分解能
		/// </summary>
		public Resolution SelectedSampleResolution {
			get => this.Model.SelectedSampleResolution;
			set => this.Model.SelectedSampleResolution = value;
		}

		/// <summary>
		/// テクスチャのX解像度
		/// </summary>
		public Resolution SelectedTextureResolutionX {
			get => this.Model.SelectedTextureResolutionX;
			set => this.Model.SelectedTextureResolutionX = value;
		}

		/// <summary>
		/// テクスチャのY解像度
		/// </summary>
		public Resolution SelectedTextureResolutionY {
			get => this.Model.SelectedTextureResolutionY;
			set => this.Model.SelectedTextureResolutionY = value;
		}
		
		/// <summary>
		/// 出力するテクスチャライン数
		/// </summary>
		public int LineCount {
			get => this.Model.LineCount;
			set => this.Model.LineCount = value;
		}

		/// <summary>
		/// FFTの解析にに使用するデータを連続的にするか否か
		/// </summary>
		public bool IsContinuousData {
			get => this.Model.IsContinuousData;
			set => this.Model.IsContinuousData = value;
		}

		/// <summary>
		/// 書き出しの進行度
		/// </summary>
		public int Progress => this.Model.AudioFile == null ? 0 : this.Model.Progress;

		/// <summary>
		/// プレビューアニメーションの時間
		/// </summary>
		public Duration PreviewDuration => this.Model.AudioFile == null ? new TimeSpan(0) : this.Model.AudioFile.MusicTime;

		#endregion

		#region Command

		/// <summary>
		/// ファイル選択コマンド
		/// </summary>
		public SelectAudioFileCommand SelectAudioFileCommand => this.Model.SelectAudioFileCommand;

		/// <summary>
		/// テクスチャ書き出しコマンド
		/// </summary>
		public ExportTextureCommand ExportTextureCommand => this.Model.ExportTextureCommand;

		#endregion

		#region Function

		/// <summary>
		/// View Modelを初期化します。
		/// </summary>
		public AudioAnalyzerViewModel() {
			this.Model = new AudioAnalyzerModel();
			this.Model.PropertyChanged += (sender, e) => this.PropertyChanged.Invoke(this, e);
		}

		#endregion
	}
}
