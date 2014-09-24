using System;
using System.Collections.Generic;
//using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PhotoCaptioner
{
	public static class PhotoProcessor
	{
		public delegate void Progress(decimal percentComplete, PhotoProcessResult result);

		public static Progress OnProgress { get; set; }

		private static decimal percentComplete = 0;
		public static void ProcessPhotos(string inputPath, string outputPath, FontFamily captionFont, Color fontColor, Color backgroundColor)
		{
			percentComplete = 0;
			Program.DebugMessage("Starting");
			List<string> fileNames = GetFileList(inputPath);
			DoProgress(new PhotoProcessResult() { Success = true, FileName = "Found", Message = string.Format("{0} files in {1}", fileNames.Count, inputPath) });
			Program.DebugMessage("Got {0} files", fileNames.Count.ToString());

			int idx = 0;
			foreach (string fileName in fileNames)
			{
				PhotoProcessResult result = new PhotoProcessResult();
				percentComplete = ++idx / (decimal)fileNames.Count * 100m;
				try
				{
					result = CheckTarget(outputPath, fileName);
					if (result.Success)
					{
						result = CaptionImage(fileName, captionFont, fontColor, backgroundColor);
						if (result.Success)
						{
							result = SavePhoto(outputPath, fileName, result.Message, result.ImageData);
						}
					}
				}
				catch (Exception ex)
				{
					result.Success = false;
					result.FileName = fileName;
					result.Message = string.Format("Type: {0}, Message: {1}, Stack: {0}", ex.GetType().Name, ex.Message, ex.StackTrace);
					Program.DebugMessage(result.Message);
				}

				DoProgress(result);
			}

			Program.DebugMessage("Done");
			percentComplete = 100;
			DoProgress();
		}

		private static PhotoProcessResult CheckTarget(string outputPath, string fileName)
		{
			PhotoProcessResult result = new PhotoProcessResult() { Success = false, Message = "", ImageData = null };
			fileName = Path.GetFileName(fileName);
			string outFileName = Path.Combine(outputPath, fileName);

			// Check if the target file already exists
			if (File.Exists(outFileName))
			{
				result.FileName = fileName;
				result.Message = "File Already Exists";
				return result;
			}

			result.Success = true;
			return result;
		}

		private static PhotoProcessResult SavePhoto(string outputPath, string fileName, string processingMessage, byte[] imageData)
		{
			PhotoProcessResult result = new PhotoProcessResult() { Success = false, Message = processingMessage, ImageData = null };
			fileName = Path.GetFileName(fileName);
			result.FileName = fileName;
			
			string outFileName = Path.Combine(outputPath, fileName);
						
			// Write the file
			try
			{
				File.WriteAllBytes(outFileName, imageData);
			}
			catch (Exception ex)
			{
				result.FileName = "Error";
				result.Message = ex.Message;
				return result;
			}

			result.Success = true;
			return result;
		}

		private static void DoProgress(PhotoProcessResult result = null)
		{
			NonBlockingConsole.WriteLine(result == null ? "" : result.Message);
			if (OnProgress != null)
			{
				OnProgress(percentComplete, result);
			}
		}

		public static List<string> GetFileList(string path)
		{

			List<FileInfo> fis = new List<FileInfo>();

			try
			{
				if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
				{
					fis.AddRange(new DirectoryInfo(path).GetFiles("*.jpg", SearchOption.AllDirectories));
					fis.AddRange(new DirectoryInfo(path).GetFiles("*.bmp", SearchOption.AllDirectories));
					fis.AddRange(new DirectoryInfo(path).GetFiles("*.png", SearchOption.AllDirectories));
					fis.AddRange(new DirectoryInfo(path).GetFiles("*.gif", SearchOption.AllDirectories));
					fis.AddRange(new DirectoryInfo(path).GetFiles("*.jpeg", SearchOption.AllDirectories));
				}
			}
			catch (Exception ex) { Program.DebugMessage("Exception in GetFileList: {0}", ex.Message); }

			return fis.Select(f => f.FullName).ToList();
		}

		private static double wpfDPI = 96.0;
		public static PhotoProcessResult CaptionImage(string filePath, FontFamily captionFont, Color fontColor, Color backgroundColor)
		{
			PhotoProcessResult result = new PhotoProcessResult() { Success = false, Message = string.Empty, FileName = Path.GetFileName(filePath) };
			
			List<string> actionsTaken = new List<string>();

			BitmapFrame img = BitmapFrame.Create(new Uri(filePath));

			// If there is no caption we aren't going to do anything
			string caption = WPFMetaData.GetPicasaCaption(img);


			if (string.IsNullOrWhiteSpace(caption))
			{
				result.Message = "No Caption Found";
				return result;
			}

			#region Orientation
			int rotation = 0;
			bool flipX = false;
			int pixelWidth = img.PixelWidth, pixelHeight = img.PixelHeight;
			double width = img.Width, height = img.Height, dpiX = img.DpiX, dpiY = img.DpiY;
			switch (WPFMetaData.GetOrientation(img))
			{
				case System.Drawing.RotateFlipType.Rotate90FlipNone:
					rotation = 90;
					width = img.Height; height = img.Width;
					pixelWidth = img.PixelHeight; pixelHeight = img.PixelWidth; dpiX = img.DpiY; dpiY = img.DpiX;
					break;
				case System.Drawing.RotateFlipType.Rotate180FlipNone:
					rotation = 180;
					break;
				case System.Drawing.RotateFlipType.Rotate270FlipNone:
					rotation = 270;
					width = img.Height; height = img.Width;
					pixelWidth = img.PixelHeight; pixelHeight = img.PixelWidth; dpiX = img.DpiY; dpiY = img.DpiX;
					break;
				case System.Drawing.RotateFlipType.RotateNoneFlipX:
					flipX = true;
					break;
				case System.Drawing.RotateFlipType.Rotate90FlipX:
					rotation = 90;
					width = img.Height; height = img.Width;
					pixelWidth = img.PixelHeight; pixelHeight = img.PixelWidth; dpiX = img.DpiY; dpiY = img.DpiX;
					flipX = true;
					break;
				case System.Drawing.RotateFlipType.Rotate180FlipX:
					rotation = 180;
					flipX = true;
					break;
				case System.Drawing.RotateFlipType.Rotate270FlipX:
					rotation = 270;
					width = img.Height; height = img.Width;
					pixelWidth = img.PixelHeight; pixelHeight = img.PixelWidth; dpiX = img.DpiY; dpiY = img.DpiX;
					flipX = true;
					break;
				default:
					break;
			}

			TransformGroup tg = new TransformGroup();
			if (rotation != 0)
			{
				tg.Children.Add(new RotateTransform(rotation));
				actionsTaken.Add("Rotated");
			}
			if (flipX)
			{
				tg.Children.Add(new ScaleTransform(-1, 1));
				actionsTaken.Add("Flipped");
			}

			TransformedBitmap tb = new TransformedBitmap(img, tg);
			#endregion

			#region Captioning
			actionsTaken.Add("Captioned");


			// Determine the caption height
			double shortSide = Math.Min(width, height);
			double dpi = shortSide / 4.0d;		// Assume our images are 4" on the short side
			double captionLineHeight = 0.25d * dpi;	// 1/4" caption
			double captionHeight = captionLineHeight;


			// Determine the text size
			FormattedText text = PhotoProcessor.FitCaption(caption, captionFont, fontColor, width, captionLineHeight, out captionHeight);
			
			// The font got too small
			if (text == null)
			{
				result.Message = "Caption is too long.";
				return result;
			}


			DrawingVisual visual = new DrawingVisual();
			using (DrawingContext dc = visual.RenderOpen())
			{
				dc.DrawImage(tb, new Rect(0, 0, width, height));
				dc.DrawRectangle(new SolidColorBrush(backgroundColor), new Pen(new SolidColorBrush(backgroundColor), 0), new Rect(0, height, width, captionHeight));
				dc.DrawText(text, new Point((width / 2d - (text.TextAlignment == TextAlignment.Center ? width : text.Width) / 2d), height + (captionHeight - text.FullHeight()) / 2d));
			}

			RenderTargetBitmap rtb = new RenderTargetBitmap(pixelWidth, pixelHeight + UnitsToPixels(captionHeight, dpiY), dpiX, dpiY, PixelFormats.Default);
			rtb.Render(visual);

			#endregion


			#region Save
			// Save the new rotated, flipped, captioned image	
			var encoder = new JpegBitmapEncoder();
			BitmapMetadata clonedMD = img.Metadata.Clone() as BitmapMetadata;
			encoder.Frames.Add(BitmapFrame.Create(rtb, null, clonedMD, img.ColorContexts));

			byte[] imageData;
			using (MemoryStream ms = new MemoryStream())
			{
				encoder.Save(ms);
				imageData = ms.ToArray();
			}
			#endregion

			result.Success = true;
			result.Message = string.Join(", ", actionsTaken);
			result.ImageData = imageData;
			return result;
		}

		private static FormattedText FitCaption(string caption, FontFamily captionFont, Color fontColor, double maxWidth, double lineHeight, out double captionHeight)
		{
			FormattedText text = new FormattedText(caption, CultureInfo.InvariantCulture, FlowDirection.LeftToRight, new Typeface(captionFont, FontStyles.Normal, FontWeights.Normal, FontStretches.Normal), 1d, new SolidColorBrush(fontColor));

			if (PhotoProcessor.GetMaxTextSize(text, maxWidth, lineHeight, out captionHeight))
			{
				return text;
			}

			return null;
		}
		private static bool GetMaxTextSize(FormattedText text, double maxWidth, double lineHeight, out double captionHeight)
		{
			double minFontSize = 1;
			double upperFontSize = 2000;	// Start with something big. This will fail we start too small
			double lowerFontSize = 0;
			double halfFontSize = 0;

			text.MaxTextWidth = 0;
			text.MaxLineCount = 1;
	
			// Determine the font size for the height we have
			do
			{
				halfFontSize = Math.Round((upperFontSize - lowerFontSize) / 2.0 + lowerFontSize);
				text.SetFontSize(halfFontSize);

				if (text.FullHeight() > lineHeight)
				{
					upperFontSize = halfFontSize;
				}
				else if (text.FullHeight() < lineHeight)
				{
					lowerFontSize = halfFontSize;
				}
				else
				{
					break; // exact size
				}
			} while (upperFontSize - lowerFontSize > 1);	// We have a close enough range of 1 so we don't continue halving to very small decimal places

			// Set the width and allow the text to wrap to multiple lines			
			text.MaxTextWidth = maxWidth;
			text.MaxLineCount = int.MaxValue;
			text.TextAlignment = TextAlignment.Center;

			captionHeight = text.FullHeight();

			return halfFontSize >= minFontSize;
		}

		private static int UnitsToPixels(double units, double dpi)
		{
			return Convert.ToInt32(units * dpi / wpfDPI);
		}

		public static double FullHeight(this FormattedText text)
		{
			return Math.Max(text.Height, text.Extent);
		}
	}

}
