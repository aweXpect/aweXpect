using System.IO;

namespace Testably.Expectations.Tests.ThatTests.Streams;

public sealed partial class StreamShould
{
	public class MyStream(
		byte[]? buffer = null,
		bool canRead = false,
		bool canWrite = false,
		bool canSeek = false,
		long position = 0,
		long length = 0)
		: Stream
	{
		/// <inheritdoc />
		public override bool CanRead { get; } = canRead;

		/// <inheritdoc />
		public override bool CanSeek { get; } = canSeek;

		/// <inheritdoc />
		public override bool CanWrite { get; } = canWrite;

		/// <inheritdoc />
		public override long Length { get; } = length;

		/// <inheritdoc />
		public override long Position { get; set; } = position;

		private readonly byte[] _buffer = buffer ?? Array.Empty<byte>();

		/// <inheritdoc />
		public override void Flush()
			=> throw new NotSupportedException();

		/// <inheritdoc />
		public override int Read(byte[] buffer, int offset, int count)
			=> throw new NotSupportedException();

		/// <inheritdoc />
		public override long Seek(long offset, SeekOrigin origin)
			=> throw new NotSupportedException();

		/// <inheritdoc />
		public override void SetLength(long value)
			=> throw new NotSupportedException();

		/// <inheritdoc />
		public override void Write(byte[] buffer, int offset, int count)
			=> throw new NotSupportedException();
	}
}
