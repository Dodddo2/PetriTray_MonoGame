#define MAXBALLS 20

float3 positions[MAXBALLS];
float4 colors[MAXBALLS];
float4 parameters[MAXBALLS];

float4 MetaballFalloff(float2 coords : TEXCOORD0, float counter) : COLOR1
{
	float distance = sqrt(pow(coords.x - positions[counter].x, 2) + pow(coords.y - positions[counter].y, 2));
	if (distance < parameters[counter].x) {
		return colors[counter];
	}
	return float4(0,0,0,0);
}

float4 PS(float2 coords: TEXCOORD0) : COLOR0
{
	return MetaballFalloff(coords, 0);
}

technique Ball
{
	pass P0
	{
		PixelShader = compile ps_4_0_level_9_3 PS();
	}
}
