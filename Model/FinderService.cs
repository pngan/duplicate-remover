﻿using System.Text;

namespace Model
{
    public class FinderService : IFinderService
	{
		public async Task<Dictionary<ulong, List<string>>> FindDuplicatesAsync()
        {
			return await Task.Run(async () =>
			{
				var equalLengthImages = new Dictionary<long, List<string>>();
				var filePatterns = new List<string> { "*.jpg", "*.CR2" };
				var parentFolder = @"e:\Users\Phillip\Documents\duplicate images for testing";

				foreach (var filePattern in filePatterns)
				{
					foreach (var fileName in Directory.EnumerateFiles(parentFolder, filePattern, SearchOption.AllDirectories))
					{
						var fi = new FileInfo(fileName);
						if (equalLengthImages.TryGetValue(fi.Length, out var equivalentImages))
						{
							equivalentImages.Add(fi.FullName);
						}
						else
						{
							equalLengthImages[fi.Length] = new List<string> { fi.FullName };
						}
					}
				}

				var duplicatesByLength = equalLengthImages.Where(eli => eli.Value.Count > 1).SelectMany(eli => eli.Value);
				var equalXORImages = new Dictionary<ulong, List<string>>();

				foreach (var duplicate in duplicatesByLength)
				{

					ulong chunk = 0;
					using (var stream = File.Open(duplicate, FileMode.Open))
					{
						using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
						{
							for (int i = 0; i < 100; i++) // test if first 400 bytes are the same
							{
								try
								{
									chunk ^= reader.ReadUInt64();
								}
								catch { }
							}
						}
					}

					if (equalXORImages.TryGetValue(chunk, out var equivalentImages))
					{
						equivalentImages.Add(duplicate);
					}
					else
					{
						equalXORImages[chunk] = new List<string> { duplicate };
					}
				}

				return equalXORImages;
			});
		}

    }
}