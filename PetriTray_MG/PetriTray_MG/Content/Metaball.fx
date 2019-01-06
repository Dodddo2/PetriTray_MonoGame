float3 positions[20];
float4 colors[20];
float heats[20];

float4 MetaballFalloff(float2 coord: TEXCOORD1, float counter) : COLOR1
{
	float dist = distance(coord, positions[counter]);
	float falloff = exp(-((dist*dist) / (2 * heats[counter] * heats[counter])));
	return (colors[counter] * falloff);
}

float4 PS(float2 coords: TEXCOORD0) : COLOR0
{
	float4 ret= MetaballFalloff(coords, 0) + MetaballFalloff(coords, 1);
	return ret;
}

technique Ball
{
	pass P0
	{
		PixelShader = compile ps_4_0_level_9_3 PS();
	}
}
