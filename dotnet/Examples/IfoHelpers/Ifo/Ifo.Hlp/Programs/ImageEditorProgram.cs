using Ifo.Hlp.Helpers;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.Formats.Png;

namespace Ifo.Hlp.Programs
{
    public abstract class ImageEditorProgram
    {
        static readonly string Filename = Path.Combine(DirectoryHelper.GetProjectDirectory(), @"Files\colors.jpg");
        public static async Task TestAsync()
        {
            using Image<Rgba32> image = await Image.LoadAsync<Rgba32>(Filename);

            await image.Clone(x => x.Rotate(7))
                .SaveAsync(TempFilename(".r7"));

            await image
                .Clone(x => x.Sepia())
                .SaveAsync(TempFilename(".sepia"));

            await image
                .Clone(x => x.Grayscale())
                .SaveAsync(TempFilename(".grayscale"));

            await image
                .Clone(x => x.Grayscale().Sepia())
                .SaveAsync(TempFilename(".grayscale.sepia"));

            //var imageSepia = image.Clone(x => x.Sepia());
            ////image.Mutate(x => x.Sepia());
            //await imageSepia.SaveAsync(Path.Combine(DirectoryHelper.GetProjectDirectory(), @"Files\Temp\zurich-l.sepia.jpg"));
        }



        private static string TempFilename(string extraTitle)
        {
            return Filename.Replace("Files\\", "Files\\Temp\\").Replace(".jpg", $"{extraTitle}.png");
        }
    }
}
