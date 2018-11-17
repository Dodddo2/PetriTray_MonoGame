using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriTray_MG
{
	public class BasicGeometry
	{
		public VertexBuffer VertexBuffer;
		public IndexBuffer IndexBuffer;
		public BasicEffect Effect;
		public BasicGeometry( ) { }

		public int Draw( Matrix world, Matrix view, Matrix projection )
		{
			Effect.World = world;
			Effect.View = view;
			Effect.Projection = projection;
			Effect.CurrentTechnique.Passes[ 0 ].Apply( );
			return DrawWithoutEffect( );
		}
		public int DrawWithoutEffect( )
		{
			var device = VertexBuffer.GraphicsDevice;
			device.SetVertexBuffer( VertexBuffer );
			device.Indices = IndexBuffer;
			device.DrawIndexedPrimitives( PrimitiveType.TriangleList, 0, 0, IndexBuffer.IndexCount / 3 );
			return VertexBuffer.VertexCount;
		}


		public static BasicGeometry CreateCube( GraphicsDevice device )
		{
			return CreateCube( device, v => v, false );
		}
		public static BasicGeometry CreateCube<T>( GraphicsDevice device,
			Func<VertexPositionNormalTexture, T> createVertexCallback, bool insideVisible ) where T : struct
		{
			var vertices = new VertexPositionNormalTexture[ ] {
				// X face
				new VertexPositionNormalTexture( new Vector3( 1, -1, 1 ), new Vector3( 1, 0, 0 ), new Vector2( 0, 0 ) ),
				new VertexPositionNormalTexture( new Vector3( 1, 1, 1 ), new Vector3( 1, 0, 0 ), new Vector2( 0, 1 ) ),
				new VertexPositionNormalTexture( new Vector3( 1, -1, -1 ), new Vector3( 1, 0, 0 ), new Vector2( 1, 0 ) ),
				new VertexPositionNormalTexture( new Vector3( 1, 1, -1 ), new Vector3( 1, 0, 0 ), new Vector2( 1, 1 ) ),
				// -X face
				new VertexPositionNormalTexture( new Vector3( -1, -1, -1 ), new Vector3( -1, 0, 0 ), new Vector2( 0, 0 ) ),
				new VertexPositionNormalTexture( new Vector3( -1, 1, -1 ), new Vector3( -1, 0, 0 ), new Vector2( 0, 1 ) ),
				new VertexPositionNormalTexture( new Vector3( -1, -1, 1 ), new Vector3( -1, 0, 0 ), new Vector2( 1, 0 ) ),
				new VertexPositionNormalTexture( new Vector3( -1, 1, 1 ), new Vector3( -1, 0, 0 ), new Vector2( 1, 1 ) ),
				// Y face
				new VertexPositionNormalTexture( new Vector3( -1, 1, -1 ), new Vector3( 0, 1, 0 ), new Vector2( 0, 0 ) ),
				new VertexPositionNormalTexture( new Vector3( 1, 1, -1 ), new Vector3( 0, 1, 0 ), new Vector2( 0, 1 ) ),
				new VertexPositionNormalTexture( new Vector3( -1, 1, 1 ), new Vector3( 0, 1, 0 ), new Vector2( 1, 0 ) ),
				new VertexPositionNormalTexture( new Vector3( 1, 1, 1 ), new Vector3( 0, 1, 0 ), new Vector2( 1, 1 ) ),
				// -Y face
				new VertexPositionNormalTexture( new Vector3( -1, -1, -1 ), new Vector3( 0, -1, 0 ), new Vector2( 0, 0 ) ),
				new VertexPositionNormalTexture( new Vector3( -1, -1, 1 ), new Vector3( 0, -1, 0 ), new Vector2( 0, 1 ) ),
				new VertexPositionNormalTexture( new Vector3( 1, -1, -1 ), new Vector3( 0, -1, 0 ), new Vector2( 1, 0 ) ),
				new VertexPositionNormalTexture( new Vector3( 1, -1, 1 ), new Vector3( 0, -1, 0 ), new Vector2( 1, 1 ) ),
				// Z face
				new VertexPositionNormalTexture( new Vector3( -1, -1, 1 ), new Vector3( 0, 0, 1 ), new Vector2( 0, 0 ) ),
				new VertexPositionNormalTexture( new Vector3( -1, 1, 1 ), new Vector3( 0, 0, 1 ), new Vector2( 0, 1 ) ),
				new VertexPositionNormalTexture( new Vector3( 1, -1, 1 ), new Vector3( 0, 0, 1 ), new Vector2( 1, 0 ) ),
				new VertexPositionNormalTexture( new Vector3( 1, 1, 1 ), new Vector3( 0, 0, 1 ), new Vector2( 1, 1 ) ),
				// -Z face
				new VertexPositionNormalTexture( new Vector3( 1, -1, -1 ), new Vector3( 0, 0, -1 ), new Vector2( 0, 0 ) ),
				new VertexPositionNormalTexture( new Vector3( 1, 1, -1 ), new Vector3( 0, 0, -1 ), new Vector2( 0, 1 ) ),
				new VertexPositionNormalTexture( new Vector3( -1, -1, -1 ), new Vector3( 0, 0, -1 ), new Vector2( 1, 0 ) ),
				new VertexPositionNormalTexture( new Vector3( -1, 1, -1 ), new Vector3( 0, 0, -1 ), new Vector2( 1, 1 ) ),
			};
			var convertedVertices = Array.ConvertAll( vertices, x => createVertexCallback( x ) );
			var indices = insideVisible ?
				 new ushort[ ]
				{
					0,2,1,1,2,3,
					4,6,5,5,6,7,
					8,10,9,9,10,11,
					12,14,13,13,14,15,
					16,18,17,17,18,19,
					20,22,21,21,22,23
				} : new ushort[ ]
				{
					0,1,2,2,1,3,
					4,5,6,6,5,7,
					8,9,10,10,9,11,
					12,13,14,14,13,15,
					16,17,18,18,17,19,
					20,21,22,22,21,23
				};

			var ret = new BasicGeometry( );
			ret.VertexBuffer = new VertexBuffer( device, typeof( T ), 24, BufferUsage.WriteOnly );
			ret.VertexBuffer.SetData( convertedVertices );
			ret.IndexBuffer = new IndexBuffer( device, IndexElementSize.SixteenBits, indices.Length, BufferUsage.WriteOnly );
			ret.IndexBuffer.SetData( indices );
			ret.Effect = new BasicEffect( device );
			ret.Effect.EnableDefaultLighting( );
			ret.Effect.PreferPerPixelLighting = true;
			ret.Effect.SpecularPower = 500;
			ret.Effect.SpecularColor = new Vector3( 0.9f, 0.9f, 1 );
			return ret;
		}
		public static BasicGeometry CreateRoundedCube( GraphicsDevice device, float sideLength )
		{
			return CreateRoundedCube( device, v => v, sideLength );
		}
		public static BasicGeometry CreateRoundedCube<T>( GraphicsDevice device,
			Func<VertexPositionNormalTexture, T> createVertexCallback, float roundedPortion ) where T : struct
		{
			float a = roundedPortion;
			var vertices = new VertexPositionNormalTexture[ ] {
				// X face: 0-3
				new VertexPositionNormalTexture( new Vector3( 1, a-1, 1-a ), new Vector3( 1, 0, 0 ), new Vector2( 0, 0 ) ),
				new VertexPositionNormalTexture( new Vector3( 1, 1-a, 1-a ), new Vector3( 1, 0, 0 ), new Vector2( 0, 1 ) ),
				new VertexPositionNormalTexture( new Vector3( 1, a-1, a-1 ), new Vector3( 1, 0, 0 ), new Vector2( 1, 0 ) ),
				new VertexPositionNormalTexture( new Vector3( 1, 1-a, a-1 ), new Vector3( 1, 0, 0 ), new Vector2( 1, 1 ) ),
				// -X face: 4-7
				new VertexPositionNormalTexture( new Vector3( -1, a-1, a-1 ), new Vector3( -1, 0, 0 ), new Vector2( 0, 0 ) ),
				new VertexPositionNormalTexture( new Vector3( -1, 1-a, a-1 ), new Vector3( -1, 0, 0 ), new Vector2( 0, 1 ) ),
				new VertexPositionNormalTexture( new Vector3( -1, a-1, 1-a ), new Vector3( -1, 0, 0 ), new Vector2( 1, 0 ) ),
				new VertexPositionNormalTexture( new Vector3( -1, 1-a, 1-a ), new Vector3( -1, 0, 0 ), new Vector2( 1, 1 ) ),
				// Y face: 8-11
				new VertexPositionNormalTexture( new Vector3( a-1, 1, a-1 ), new Vector3( 0, 1, 0 ), new Vector2( 0, 0 ) ),
				new VertexPositionNormalTexture( new Vector3( 1-a, 1, a-1 ), new Vector3( 0, 1, 0 ), new Vector2( 0, 1 ) ),
				new VertexPositionNormalTexture( new Vector3( a-1, 1, 1-a ), new Vector3( 0, 1, 0 ), new Vector2( 1, 0 ) ),
				new VertexPositionNormalTexture( new Vector3( 1-a, 1, 1-a ), new Vector3( 0, 1, 0 ), new Vector2( 1, 1 ) ),
				// -Y face: 12-15
				new VertexPositionNormalTexture( new Vector3( a-1, -1, a-1 ), new Vector3( 0, -1, 0 ), new Vector2( 0, 0 ) ),
				new VertexPositionNormalTexture( new Vector3( a-1, -1, 1-a ), new Vector3( 0, -1, 0 ), new Vector2( 0, 1 ) ),
				new VertexPositionNormalTexture( new Vector3( 1-a, -1, a-1 ), new Vector3( 0, -1, 0 ), new Vector2( 1, 0 ) ),
				new VertexPositionNormalTexture( new Vector3( 1-a, -1, 1-a ), new Vector3( 0, -1, 0 ), new Vector2( 1, 1 ) ),
				// Z face: 16-19
				new VertexPositionNormalTexture( new Vector3( a-1, a-1, 1 ), new Vector3( 0, 0, 1 ), new Vector2( 0, 0 ) ),
				new VertexPositionNormalTexture( new Vector3( a-1, 1-a, 1 ), new Vector3( 0, 0, 1 ), new Vector2( 0, 1 ) ),
				new VertexPositionNormalTexture( new Vector3( 1-a, a-1, 1 ), new Vector3( 0, 0, 1 ), new Vector2( 1, 0 ) ),
				new VertexPositionNormalTexture( new Vector3( 1-a, 1-a, 1 ), new Vector3( 0, 0, 1 ), new Vector2( 1, 1 ) ),
				// -Z face: 20-23
				new VertexPositionNormalTexture( new Vector3( 1-a, a-1, -1 ), new Vector3( 0, 0, -1 ), new Vector2( 0, 0 ) ),
				new VertexPositionNormalTexture( new Vector3( 1-a, 1-a, -1 ), new Vector3( 0, 0, -1 ), new Vector2( 0, 1 ) ),
				new VertexPositionNormalTexture( new Vector3( a-1, a-1, -1 ), new Vector3( 0, 0, -1 ), new Vector2( 1, 0 ) ),
				new VertexPositionNormalTexture( new Vector3( a-1, 1-a, -1 ), new Vector3( 0, 0, -1 ), new Vector2( 1, 1 ) ),
			};
			var convertedVertices = Array.ConvertAll( vertices, x => createVertexCallback( x ) );
			var indices = new ushort[ ]
				{
					0,1,2,2,1,3, // X
					4,5,6,6,5,7, // -X
					8,9,10,10,9,11, // Y
					12,13,14,14,13,15, // -Y
					16,17,18,18,17,19, // Z
					20,21,22,22,21,23, // -Z
					1,0,18,18,19,1, // X,Z
					2,3,20,21,20,3, // X,-Z
					6,7,16,17,16,7, // -X,Z
					5,4,22,22,23,5, // -X,-Z
					3,1,9,11,9,1, // X,Y
					7,5,8,8,10,7, // -X,Y
					10,11,17,19,17,11, // Y,Z
					9,8,21,23,21,8, // Y,-Z
					0,2,14,14,15,0, // X,-Y
					4,6,12,13,12,6, // -X,-Y
					15,13,16,16,18,15, // Z,-Y
					12,14,20,20,22,12, // -Z,-Y
					11,1,19, // X,Y,Z
					3,9,21, // X,Y,-Z
					7,10,17, // -X,Y,Z
					8,5,23, // -X,Y,-Z
					0,15,18, // X,-Y,Z
					14,2,20, // X,-Y,-Z
					13,6,16, // -X,-Y,Z
					4,12,22, // -X,-Y,-Z
				};

			var ret = new BasicGeometry( );
			ret.VertexBuffer = new VertexBuffer( device, typeof( T ), 24, BufferUsage.WriteOnly );
			ret.VertexBuffer.SetData( convertedVertices );
			ret.IndexBuffer = new IndexBuffer( device, IndexElementSize.SixteenBits, indices.Length, BufferUsage.WriteOnly );
			ret.IndexBuffer.SetData( indices );
			ret.Effect = new BasicEffect( device );
			ret.Effect.EnableDefaultLighting( );
			ret.Effect.DiffuseColor = new Vector3( 0.9f, 0.5f, 0.2f );
			ret.Effect.PreferPerPixelLighting = true;
			ret.Effect.SpecularPower = 1000;
			ret.Effect.SpecularColor = new Vector3( 1, 1, 1 );
			return ret;
		}

		public static BasicGeometry CreateSphere( GraphicsDevice device )
		{
			return CreateSphere( device, 15, 7, v => v );
		}

		public static BasicGeometry CreateSphere<T>( GraphicsDevice device, int slices, int stacks, Func<VertexPositionNormalTexture, T> createVertexCallback ) where T : struct
		{
			return CreateSphere( device, slices, stacks, 0, 0, -MathHelper.PiOver2, MathHelper.PiOver2, createVertexCallback, true );
		}

		public static BasicGeometry CreateHalfSphere<T>( GraphicsDevice device, int slices, int stacks, Func<VertexPositionNormalTexture, T> createVertexCallback ) where T : struct
		{
			return CreateSphere( device, slices, stacks, 0, 0, 0, MathHelper.PiOver2, createVertexCallback, true );
		}

		public static BasicGeometry CreateSphere<T>( GraphicsDevice device, int slices, int stacks,
			float sliceAngle1, float sliceAngle2, float stackAngle1, float stackAngle2,
			Func<VertexPositionNormalTexture, T> createVertexCallback, bool insideVisible ) where T : struct
		{
			if ( slices < 3 || stacks < 3 || slices > ushort.MaxValue || stacks > ushort.MaxValue || ( slices + 1 ) * ( stacks + 1 ) > ushort.MaxValue )
				throw new ArgumentOutOfRangeException( "Sphere does not support 64K+ vertices" );
			if ( sliceAngle1 > sliceAngle2 || stackAngle1 >= stackAngle2 )
				throw new ArgumentOutOfRangeException( "Angles are wrong" );
			var vpnt = new T[ ( slices + 1 ) * ( stacks + 1 ) ];
			float phi, theta;
			float dphi = ( stackAngle2 - stackAngle1 ) / stacks;
			float dtheta = sliceAngle2 == sliceAngle1 ? MathHelper.TwoPi / slices : ( sliceAngle2 - sliceAngle1 ) / slices;
			float x, y, z, sc;
			int index = 0;
			for ( int stack = 0; stack <= stacks; stack++ )
			{
				phi = stack == stacks ? stackAngle2 : stackAngle1 + stack * dphi;
				y = (float)Math.Sin( phi );
				sc = -(float)Math.Cos( phi );
				for ( int slice = 0; slice <= slices; slice++ )
				{
					theta = slice == slices ? sliceAngle2 : sliceAngle1 + slice * dtheta;
					x = sc * (float)Math.Sin( theta );
					z = sc * (float)Math.Cos( theta );
					vpnt[ index++ ] = createVertexCallback( new VertexPositionNormalTexture( new Vector3( x, y, z ), new Vector3( x, y, z ),
						new Vector2( (float)slice / (float)slices, (float)stack / (float)stacks ) ) );
				}
			}
			var indices = new ushort[ slices * stacks * 6 ];
			index = 0;
			int k = slices + 1;
			for ( int stack = 0; stack < stacks; stack++ )
			{
				for ( int slice = 0; slice < slices; slice++ )
				{
					indices[ index++ ] = (ushort)( ( stack + 0 ) * k + slice );
					indices[ index++ ] = insideVisible ? (ushort)( ( stack + 0 ) * k + slice + 1 ) : (ushort)( ( stack + 1 ) * k + slice + 0 );
					indices[ index++ ] = insideVisible ? (ushort)( ( stack + 1 ) * k + slice + 0 ) : (ushort)( ( stack + 0 ) * k + slice + 1 );
					indices[ index++ ] = (ushort)( ( stack + 0 ) * k + slice + 1 );
					indices[ index++ ] = insideVisible ? (ushort)( ( stack + 1 ) * k + slice + 1 ) : (ushort)( ( stack + 1 ) * k + slice );
					indices[ index++ ] = insideVisible ? (ushort)( ( stack + 1 ) * k + slice ) : (ushort)( ( stack + 1 ) * k + slice + 1 );
				}
			}

			var ret = new BasicGeometry( );
			ret.VertexBuffer = new VertexBuffer( device, typeof( T ), vpnt.Length, BufferUsage.WriteOnly );
			ret.VertexBuffer.SetData( vpnt );
			ret.IndexBuffer = new IndexBuffer( device, IndexElementSize.SixteenBits, indices.Length, BufferUsage.WriteOnly );
			ret.IndexBuffer.SetData( indices );
			ret.Effect = new BasicEffect( device );
			return ret;
		}

	}
}
