using System;
using System.Collections;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using SharperCV;
//using openCVSpriteFactory;


namespace IPpakge
{
	/// <summary>
	/// Summary description for CImage.
	/// </summary>
	public class CImage
	{
		private Bitmap bitmap;
		private BitmapData data;
		private bool Locked = false;
		[DllImport("msvcrt.dll")]
		public static extern unsafe void * memcpy(void * dest,void * src,int size);
		public CImage(string FileName)
		{
			bitmap = (Bitmap)Image.FromFile(FileName);
		}

		public CImage(Bitmap bitmap)
		{
			this.bitmap = bitmap;
		}

		public CImage(int Width, int Height, Color color)
		{
			this.bitmap = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
			this.Clear(color);
		}

		public CImage(CvImage img)
		{
			this.bitmap = new Bitmap(img.Size.width,img.Size.height,PixelFormat.Format24bppRgb);
			this.LockImage();
			unsafe
			{
				memcpy(this.data.Scan0.ToPointer(),img.getPixelAddr(0,0,0).ToPointer(),
					this.Width * this.Height * 3);
			}
			this.UnLockImage();
		}

		public CImage(int Width, int Height)
		{
			this.bitmap = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
		}

		public Bitmap GetBitmap
		{
			get { return this.bitmap; }
		}

		unsafe private Color GetCvPixelFromPointer(IntPtr color)
		{
			byte Red;
			byte Green;
			byte Blue;
			uint val = *((uint*)color.ToPointer());
			Red =   (byte)((val & 0x00FF0000) >> 16);
			Green = (byte)((val & 0x0000FF00) >> 8);
			Blue =  (byte)(val & 0x000000FF);
			return Color.FromArgb(Red,Red,Green,Blue);
		}

		public CvImage GetCvImage
		{

			get
			{
				IntPtr lp = this.bitmap.GetHbitmap();
				int kl = lp.ToInt32();
				CvImage img  = new CvImage(new CvSize(this.bitmap.Width,this.bitmap.Height),
					BitDepths.IPL_DEPTH_8U,3);
				this.LockImage();
				unsafe
				{
					memcpy(img.getPixelAddr(0,0,0).ToPointer(),
						this.data.Scan0.ToPointer(),this.Width * this.Height * 3);
				}
				this.UnLockImage();
				return img;
			}
		}

		public int Width
		{
			get { return this.bitmap.Width; }
		}

		public int Height
		{
			get { return this.bitmap.Height; }
		}

		public bool LockImage()
		{
			if(Locked)
				return false;
			try
			{
				data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width,
					bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
				Locked = true;
				return true;
			}
			catch
			{
				return false;
			}
		}
		public bool UnLockImage()
		{
			if (!Locked)
				return false;
			Locked = false;
			bitmap.UnlockBits(data);
			return true;
		}
		public Color GetPixel(int X, int Y)
		{
			if (!Locked)
				throw new Exception("Image hadn't been locked");
			int stride, Offset;
			System.IntPtr Scan0;
			stride = data.Stride;
			Scan0 = data.Scan0;
			Offset = stride - data.Width * 3;
			Color pixel;
			unsafe
			{
				byte* p = (byte*)(void*)Scan0;
				Y *= 3;
				pixel = Color.FromArgb(p[X * (data.Width * 3 + Offset) + Y+2],
					p[X * (data.Width * 3 + Offset) + Y+2], 
					p[X * (data.Width * 3 + Offset) + Y + 1],
					p[X * (data.Width * 3 + Offset) + Y]);
			}
			return pixel;
		}
		public void SetPixel(int X, int Y, Color pixel)
		{
			int stride, Offset;
			System.IntPtr Scan0;
			stride = data.Stride;
			Scan0 = data.Scan0;
			Offset = stride - data.Width * 3;
			unsafe
			{
				byte* p = (byte*)(void*)Scan0;
				Y *= 3;
				p[X * (data.Width * 3 + Offset) + Y] = pixel.B;
				p[X * (data.Width * 3 + Offset) + Y + 1] = pixel.G;
				p[X * (data.Width * 3 + Offset) + Y + 2] = pixel.R;
			}
		}

		public void Clear()
		{
			Clear(Color.Black);
		}

		public void Clear(Color color)
		{
			bool Unlock = false;
			if (!Locked)
			{
				Unlock = true;
				LockImage();
			}
			for (int i = 0; i < this.bitmap.Height; i++)
			{
				for (int j = 0; j < this.bitmap.Width; j++)
					SetPixel(i, j, color);
			}
			if (Unlock)
				UnLockImage();
		}

		private void Swap(ref int X, ref int Y)
		{
			int temp = X;
			X = Y;
			Y = temp;
		}

		private void CheckBoundries(int x1, int y1, int x2, int y2)
		{
			if (x1 >= this.Width || x2 >= this.Width || x1 < 0
				|| x2 < 0 || y1 >= this.Height || y2 >= this.Height || y1 < 0
				|| y2 < 0)
			{
				throw new Exception("Out of range");
			}
		}

		public void DrawCircle(Point center, int radius, Color color)
		{
			DrawCircle(center.X, center.Y, radius, color);
		}

		public void DrawCircle(int x, int y, int radius, Color color)
		{
			CheckBoundries(x, y, x, y);
			Swap(ref x, ref y);
			bool Unlock = false;
			if (!Locked)
			{
				Unlock = true;
				LockImage();
			}


			int currentDX = 0;
			int currentDY = radius;
			int tempCal = 1 - radius;


			SetPixel(x, y + radius, color);
			SetPixel(x, y - radius, color);
			SetPixel(x + radius, y, color);
			SetPixel(x - radius, y, color);

			while (currentDX < currentDY)
			{
				currentDX++;
				if (tempCal < 0)
				{
					tempCal += 2 * currentDX + 1;
				}
				else
				{
					currentDY--;
					tempCal += 2 * (currentDX - currentDY) + 1;
				}

				SetPixel(x + currentDX, y + currentDY, color);
				SetPixel(x + currentDX, y - currentDY, color);
				SetPixel(x - currentDX, y + currentDY, color);
				SetPixel(x - currentDX, y - currentDY, color);
				SetPixel(x + currentDY, y + currentDX, color);
				SetPixel(x + currentDY, y - currentDX, color);
				SetPixel(x - currentDY, y + currentDX, color);
				SetPixel(x - currentDY, y - currentDX, color);
			}

			if (Unlock)
				UnLockImage();

		}

		public void DrawLine(Point p1, Point p2, Color color)
		{
			DrawLine(p1.X, p1.Y, p2.X, p2.Y, color);
		}

		public void DrawLine(int x1, int y1, int x2, int y2, Color color)
		{
			CheckBoundries(x1, y1, x2, y2);
			Swap(ref x1, ref y1);
			Swap(ref x2, ref y2);

			bool Unlock = false;
			if (!Locked)
			{
				Unlock = true;
				LockImage();
			}

			int dx = x2 - x1, dy = y2 - y1;
			if (dx != 0 || dy != 0)
			{
				if (Math.Abs(dx) >= Math.Abs(dy))
				{
					float y = y1 + 0.5f;
					float dly = (float)dy / (float)dx;
					if (dx > 0)
						for (int x = x1; x <= x2; x++)
						{
							SetPixel(x, (int)(Math.Floor(y)), color);
							y += dly;
						}
					else
						for (int x = x1; x >= (int)x2; x--)
						{
							SetPixel(x, (int)(Math.Floor(y)), color);
							y -= dly;
						}
				}
				else
				{
					float x = x1 + 0.5f;
					float dlx = (float)dx / (float)dy;
					if (dy > 0)
						for (int y = y1; y <= y2; y++)
						{
							SetPixel((int)(Math.Floor(x)), y, color);
							x += dlx;
						}
					else
						for (int y = y1; y >= (int)y2; y--)
						{
							SetPixel((int)(Math.Floor(x)), y, color);
							x -= dlx;
						}
				}
			}


			if (Unlock)
				UnLockImage();
		}

		public void DrawRectangle(Point p1, Point p2, Color color)
		{
			DrawRectangle(p1.X, p1.Y, p2.X, p2.Y, color);
		}

		public void DrawRectangle(Point TopLeft, int Width, int Height, Color color)
		{
			DrawRectangle(TopLeft.X, TopLeft.Y, TopLeft.X + Width, TopLeft.Y + Height, color);
		}

		public void DrawRectangle(int x1, int y1, int x2, int y2, Color color)
		{
			CheckBoundries(x1, y1, x2, y2);
			Swap(ref x1, ref y1);
			Swap(ref x2, ref y2);
			bool Unlock = false;
			if (!Locked)
			{
				Unlock = true;
				LockImage();
			}

			int i;
			// Draw top and bottom border
			for (i = x1; i <= x2; i++)
			{
				SetPixel(i, y1, color);
				SetPixel(i, y2, color);
			}

			// Draw left and right border
			for (i = y1; i <= y2; i++)
			{
				SetPixel(x1, i, color);
				SetPixel(x2, i, color);
			}
			if (Unlock)
				UnLockImage();
		}
	}

}
