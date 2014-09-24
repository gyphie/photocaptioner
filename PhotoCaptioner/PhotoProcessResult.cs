
namespace PhotoCaptioner
{
	public class PhotoProcessResult
	{
		public bool Success { get; set; }
		public string FileName { get; set; }
		public string Message { get; set; }
		public byte[] ImageData { get; set; }

		public override string ToString()
		{
			return string.Format("{0}: {1}", this.FileName, this.Message);
		}
	}
}
