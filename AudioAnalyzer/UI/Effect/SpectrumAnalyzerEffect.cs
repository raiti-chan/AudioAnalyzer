using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace AudioAnalyzer.UI.Effect {
	/// <summary>
	/// 生成したテクスチャのプレビュー用シェーダー
	/// </summary>
	internal class SpectrumAnalyzerEffect : ShaderEffect{

		#region Property

		/// <summary>
		/// シェーダーファイル指定のプロパティ
		/// </summary>
		public static readonly DependencyProperty ShaderFileProperty = DependencyProperty.Register("ShaderFile", typeof(string), typeof(SpectrumAnalyzerEffect), new FrameworkPropertyMetadata("シェーダーファイル", OnChangedShaderFileProperty));

		/// <summary>
		/// シェーダーファイル名
		/// </summary>
		public string ShaderFile {
			get => (string)this.GetValue(ShaderFileProperty);
			set => this.SetValue(ShaderFileProperty, value);
		}

		/// <summary>
		/// シェーダー内の時間用プロパティ
		/// </summary>
		public static readonly DependencyProperty SeqProperty = DependencyProperty.Register("Seq", typeof(double), typeof(SpectrumAnalyzerEffect), new PropertyMetadata(0.0, PixelShaderConstantCallback(0)));

		/// <summary>
		/// 経過時間
		/// </summary>
		public double Seq {
			get => (double)this.GetValue(SeqProperty);
			set => this.SetValue(SeqProperty, value);
		}

		/// <summary>
		/// シェーダーの音声テクスチャプロパティ
		/// </summary>
		public static readonly DependencyProperty AudioTexProperty = RegisterPixelShaderSamplerProperty("AudioTex", typeof(SpectrumAnalyzerEffect), 0);

		/// <summary>
		/// 音声テクスチャ
		/// </summary>
		public Brush AudioTex {
			get => (Brush)this.GetValue(AudioTexProperty);
			set => this.SetValue(AudioTexProperty, value);
		}

		#endregion

		/// <summary>
		/// スペクトラムアナライザを表示するコントロール
		/// </summary>
		public SpectrumAnalyzerEffect() => this.PixelShader = new PixelShader();

		/// <summary>
		/// シェーダーファイルが変更された場合
		/// </summary>
		/// <param name="obj">インスタンス</param>
		/// <param name="e">イベントパラメーター</param>
		private static void OnChangedShaderFileProperty(DependencyObject obj, DependencyPropertyChangedEventArgs e) {
			if (!(obj is SpectrumAnalyzerEffect effect)) {
				return;
			}

			try {
				effect.PixelShader.UriSource = new Uri(Path.GetFullPath(effect.ShaderFile));
			} catch (Exception) {

			}
		}

	}
}
