float3 positions[20];
float4 colors[20];
float heats[20];

float4 MetaballFalloff(float2 coord: TEXCOORD1, float counter) : COLOR1
{
	float dist = distance(coord, positions[counter]);
	float falloff = exp(-(pow(dist, 4) / (2 * heats[counter] * heats[counter])));
	return (colors[counter] * falloff);
}

float4 PS(float2 coords: TEXCOORD0) : COLOR0
{
	float treshold = 0.2f;
	
	float4 ret= MetaballFalloff(coords, 0) + MetaballFalloff(coords, 1);
	ret = ret - treshold;
	ret = lerp(0, 1/(1 - treshold), ret);
	float blobStencil = ret.a;
	float alphaStencil = -ret.a + 1;
	ret.a = (ret.a < 0.4f ? 0 : ret.a);
	
	



	return ret;
}

technique Ball
{
	pass P0
	{
		PixelShader = compile ps_4_0_level_9_3 PS();
	}
}
