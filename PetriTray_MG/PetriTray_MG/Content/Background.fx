float2 worldXY;
float gameTime;

texture2D backgroundImage;
SamplerState Sampler {
	AddressU = Wrap;
	AddressV = Wrap;
	Texture = <backgroundImage>;
};


float4 PS(float4 pos : SV_POSITION, float4 color1 : COLOR1, float2 coords : TEXCOORD1) : SV_TARGET
{	
	worldXY = worldXY * (1.0f / 2560.0f);
	float2 distortedCoords = float2(coords.x + sin(gameTime * 2.4f + coords.x * 6.28) / 300 + worldXY.x, coords.y + cos(gameTime * 3.8f + coords.y * 6.28) / 300 + worldXY.y);
	float4 ret = backgroundImage.Sample(Sampler, distortedCoords);
	return float4 (ret.rg, ret.b*0.8f, ret.b);
}


technique PerlinNoise
{
	pass P0
	{
		PixelShader = compile ps_4_0_level_9_3 PS();
	}
}