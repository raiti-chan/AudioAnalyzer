using AudioAnalyzer.UI.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		/// ファイル名
		/// </summary>
		public string FileName => this.Model.AudioFile == null ? "音声ファイルが選択されていません" : this.Model.AudioFile.FileName;

		/// <summary>
		/// サンプリングレート
		/// </summary>
		public string SamplingRate => this.Model.AudioFile == null ? "Null" : this.Model.AudioFile.SamplingRate.ToString();

		/// <summary>
		/// 音声ファイルの長さ(秒)
		/// </summary>
		public string MusicLength => this.Model.AudioFile == null ? "Null" : this.Model.AudioFile.MusicLength.ToString() + " 秒";

		/// <summary>
		/// 書き出しの進行度
		/// </summary>
		public int Progress => this.Model.AudioFile == null ? 0 : this.Model.Progress;

		#endregion

		#region Command

		/// <summary>
		/// ファイル選択コマンド
		/// </summary>
		public SelectAudioFile SelectAudioFile => this.Model.SelectAudioFile;

		/// <summary>
		/// テクスチャ書き出しコマンド
		/// </summary>
		public ExportTexture ExportTexture => this.Model.ExportTexture;

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
