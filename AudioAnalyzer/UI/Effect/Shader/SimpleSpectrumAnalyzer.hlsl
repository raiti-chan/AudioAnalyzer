uniform float Seq : register(c0);
uniform sampler2D AudioTex : register(s0);

float get_audio_level(int split_count, int index) {
	float4 waveTexColor = tex2Dlod(AudioTex, float4(Seq, float(index) / float(split_count), 0.0, 0.0));
	return waveTexColor.r;
}

float4 main(float2 uv : TEXCOORD) : COLOR{

	if (Seq == -1) {
		return tex2D(AudioTex, uv);
	}

	float4 col = float4(0.0, 0.0, 0.0, 1.0);
	float2 pos = 1.0 - uv;

	int vert_pos = int(ceil(pos.x * 1980));
	float volume = get_audio_level(1980, vert_pos);

	if (pos.y <= volume) {
		col = lerp(float4(0.5, 1.0, 1.0, 1.0), float4(0.5, 1.0, 0.5, 1.0), pos.y);

	}

	return col;

}