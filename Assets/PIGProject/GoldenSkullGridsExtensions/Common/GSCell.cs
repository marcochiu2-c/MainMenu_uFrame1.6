using System.Linq;
using UnityEngine;

namespace Gamelogic.Grids.GoldenSkull
{
	/// <summary>
	/// A component for that allows Golden Skull tile art to be used with Gamelogic Grids. 
	/// </summary>
	public class GSCell : TileCell
	{
		#region Constants		
		/// <summary>
		/// The default dimensions of the "flat" area of hex cells.
		/// </summary>
		public static readonly Vector2 DefaultHexCellDimensions = new Vector2(188, 64);

		/// <summary>
		/// The default dimensions of the "flat" area of iso cells.
		/// </summary>
		public static readonly Vector2 DefaultIsoCellDimensions = new Vector2(126, 64);

		/// <summary>
		/// The amount to move a hex sprite to stack it onto another.
		/// </summary>
		public const float DefaultHexStackOffset = 94f;

		/// <summary>
		/// The amount to move a iso sprite to stack it onto another.
		/// </summary>
		public const float DefaultIsoStackOffset = 94f;
		#endregion

		#region Public Fields		
		/// <summary>
		/// The dimensions of the "flat" are of cells.
		/// </summary>
		public Vector2 cellDimensions = DefaultHexCellDimensions;

		/// <summary>
		/// The offset to used for stacking tiles.
		/// </summary>
		public float stackOffset = DefaultHexStackOffset;

		#endregion

		#region Private Fields
		private int stackHeight = 1;
		private Color color = Color.white;
		private Sprite sprite;
		private int sortingLayerOrder;
		private float heightOffset;
		#endregion

		#region Properties		
		/// <summary>
		/// Gets or sets the layer order for this cell to use.
		/// </summary>
		/// <value>
		/// The layer order.
		/// </value>
		public int SortingLayerOrder
		{
			get { return sortingLayerOrder; }

			set
			{
				sortingLayerOrder = value;
			
				__UpdatePresentation(true);
			}
		}

		/// <summary>
		/// Gets or sets the height of the stack for this cell.
		/// </summary>
		/// <value>
		/// The height of the stack.
		/// </value>
		public int StackHeight
		{
			get { return stackHeight; }

			set
			{
				if (stackHeight != value)
				{
					if (stackHeight >= 1)
					{
						stackHeight = value;
						__UpdatePresentation(true);
					}
				}
			}
		}

		/// <summary>
		/// Gets or sets the color to tint this cell.
		/// </summary>
		/// <value>
		/// The color.
		/// </value>
		public override Color Color
		{
			get { return color; }

			set
			{
				color = value;
				__UpdatePresentation(true);
			}
	
		}

		/// <summary>
		/// Gets or sets the sprite.
		/// </summary>
		/// <value>
		/// The sprite.
		/// </value>
		public Sprite Sprite
		{
			get { return sprite; }

			set
			{
				sprite = value;
				__UpdatePresentation(true);
			}
		}

		public override Vector2 Dimensions
		{
			get { return cellDimensions; }
		}

		public float HeightOffset
		{
			get { return heightOffset; }
			set
			{
				heightOffset = value;
				__UpdatePresentation(true);
			}
		}
		#endregion

		#region Methods
		public override void __UpdatePresentation(bool forceUpdate)
		{
			UpdateHeight();

			var children = transform.GetChildren();
			int i = 0;
			foreach (var child in children)
			{
				var spriteRenderer = child.GetComponent<SpriteRenderer>();

				spriteRenderer.sprite = sprite;
				spriteRenderer.color = color;
				spriteRenderer.sortingOrder = sortingLayerOrder + i;

				i++;
			}
		}

		public override void SetAngle(float angle)
		{
			//Not necessary to implement for uniform grid cells.
		}

		public override void AddAngle(float angle)
		{
			//Not necessary to implement for uniform grid cells.
		}

		private void UpdateHeight()
		{
			var children = transform.GetChildren().ToList();

			var firstChild = children.First();
			var otherChildren = children.ButFirst();

			firstChild.transform.localPosition = Vector3.up * GetHeight(0);

			foreach (var otherChild in otherChildren)
			{
				if (Application.isPlaying)
				{
					DestroyObject(otherChild.gameObject);
				}
				else
				{
					DestroyImmediate(otherChild.gameObject);
				}
			}

			for (int i = 1; i < stackHeight; i++)
			{
				var newChild = Instantiate(firstChild);
				newChild.transform.parent = transform;

				newChild.localPosition = Vector3.up * GetHeight(i);
			}
		}

		private float GetHeight(int stackLevel)
		{
			return heightOffset + stackOffset*stackLevel;
		}

		#endregion
	}
}