using SharpDX.D3DCompiler;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace SpectrumViewer {
	internal class CustomShaderEffect : ShaderEffect {

		public static readonly DependencyProperty SeqProperty = DependencyProperty.Register("Seq", typeof(double), typeof(CustomShaderEffect), new PropertyMetadata(0.0, PixelShaderConstantCallback(0)));

		public double Seq {
			get => (double)this.GetValue(SeqProperty);
			set => this.SetValue(SeqProperty, value);
		}

		public static readonly DependencyProperty AudioTexProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("AudioTex", typeof(CustomShaderEffect), 0);

		public Brush AudioTex {
			get => (Brush)this.GetValue(AudioTexProperty);
			set => this.SetValue(AudioTexProperty, value);
		}



		public CustomShaderEffect() {
			this.PixelShader = new PixelShader();
			using (StreamReader reader = new StreamReader("./shader.hlsl", Encoding.UTF8)) {
				string hlsl = reader.ReadToEnd();
				reader.Close();
				CompilationResult result = ShaderBytecode.Compile(hlsl, "main", "ps_3_0");

				using (MemoryStream stream = new MemoryStream(result.Bytecode)) {
					this.PixelShader.SetStreamSource(stream);
				}
			}
		}

	}
}
