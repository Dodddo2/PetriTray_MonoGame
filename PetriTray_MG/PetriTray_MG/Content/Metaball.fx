float4 positions[10];
float4 colors[10];
int ballCount;
texture2D Blob;
sampler BlobSampler = sampler_state
{
	Texture = <Blob>;
};

float4 MetaballFalloff(float2 coord : TEXCOORD1, int counter) : COLOR1
{
	float dist = distance(coord, positions[counter].xy);
	float falloff = exp(-(pow(dist, 2) / (2 * positions[counter].w * positions[counter].w)));
	return (colors[counter] * falloff);
}

float4 PS(float4 pos : SV_POSITION, float4 color1 : COLOR0, float2 coords : TEXCOORD0) : SV_TARGET
{
	coords = float2(coords.x, -coords.y);
	float treshold = 0.2f;
	float4 ret = float4(0,0,0,0);

	int counter;
	for (counter = 0; counter < ballCount; counter++) {
		ret += MetaballFalloff(coords, counter);
	}

	
	ret = ret - treshold;

	clip(ret.a);

	ret = lerp(0, 1 / (1 - treshold), ret);
	float blobStencil = ret.a;
	float alphaStencil = -ret.a + 1;
	ret.a = (ret.a < 0.4f ? -1 : ret.a);

	clip(ret.a);



	return ret;
}


float4 Blur(float4 pos : SV_POSITION, float4 color : COLOR0, float2 coords : TEXCOORD0) : SV_TARGET
{	
	float4 blobDiffuse = tex2D(BlobSampler, coords);
	float4 ret = float4(blobDiffuse.gbr , 1);
	return ret;
}


technique Ball
{
	pass P0
	{
		PixelShader = compile ps_4_0_level_9_3 PS();
	}

	pass P1
	{
		PixelShader = compile ps_4_0_level_9_3 Blur();
	}
}
