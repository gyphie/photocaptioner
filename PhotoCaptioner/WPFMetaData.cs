using System.Drawing;
using System.Windows.Media.Imaging;

namespace PhotoCaptioner
{
	class WPFMetaData
	{
		public static RotateFlipType GetOrientation(BitmapFrame bitmapFrame)
		{
			var metadata = bitmapFrame.Metadata as BitmapMetadata;
			if (metadata != null)
			{
				var value = metadata.GetQuery("System.Photo.Orientation");
				if (value != null)
				{
					return OrientationToFlipType(value);
				}
			}

			return RotateFlipType.RotateNoneFlipNone;
		}

		public static string GetPicasaCaption(BitmapFrame bitmapFrame)
		{
			var caption = "";
			var metadata = bitmapFrame.Metadata as BitmapMetadata;

			if (metadata != null)
			{
				var c = metadata.GetQuery("/app13/{ushort=0}/{ulonglong=61857348781060}/iptc/{str=Caption}");
				if (c != null && c is string)
				{
					caption = c as string;
				}
			}

			if (string.IsNullOrWhiteSpace(caption))
			{
				caption = metadata.Title;
			}

			return caption;
		}


		private static RotateFlipType OrientationToFlipType(object orientation)
		{
			int o = 0;
			int.TryParse(orientation.ToString(), out o);

			switch (o)
			{
				case 1:
					return RotateFlipType.RotateNoneFlipNone;
				case 2:
					return RotateFlipType.RotateNoneFlipX;
				case 3:
					return RotateFlipType.Rotate180FlipNone;
				case 4:
					return RotateFlipType.Rotate180FlipX;
				case 5:
					return RotateFlipType.Rotate90FlipX;
				case 6:
					return RotateFlipType.Rotate90FlipNone;
				case 7:
					return RotateFlipType.Rotate270FlipX;
				case 8:
					return RotateFlipType.Rotate270FlipNone;
				default:
					return RotateFlipType.RotateNoneFlipNone;
			}
		}

	}
}
