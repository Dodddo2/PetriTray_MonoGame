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
	//Gauss-görbe implementációja
	float dist = distance(coord, positions[counter].xy);
	float falloff = exp(-(pow(dist, 2) / (2 * positions[counter].w * positions[counter].w)));
	return (colors[counter] * falloff);
}

float4 PS(float4 pos : SV_POSITION, float4 color1 : COLOR0, float2 coords : TEXCOORD0) : SV_TARGET
{
	coords = float2(coords.x, -coords.y);
	float treshold = 0.2f;
	float4 ret = float4(0,0,0,0);

	//Adott pixelre mekkora erővel vetül az összes metaball
	int counter;
	for (counter = 0; counter < ballCount; counter++) {
		ret += MetaballFalloff(coords, counter);
	}

	//kívánt méret kívüli részek levágása
	ret = ret - treshold;
	clip(ret.a);

	//Érdekes Stencilek
	float blobStencil = ret.a;
	float alphaStencil = -ret.a + 1;

	//Vágás után a komponensek nyújtása 
	ret = lerp(0, 1 / (1 - treshold), ret);

	//Metaball peremén a színek telítése
	ret.rgb = normalize(ret.rgb);
	float maxComponent;
	maxComponent = max(ret.r, ret.g);
	maxComponent = max(maxComponent, ret.b);
	ret.rgb = lerp(0, maxComponent, ret.rgb);

	//Metaball széleinek trimmelése
	float trimmFactor = 0.4f;
	ret.a = (ret.a < trimmFactor ? -1 : ret.a);

	clip(ret.a);

	ret.a = -ret.a + 1 + trimmFactor;
	//ret.a = lerp(0, 1-trimmFactor, ret.a);



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
