using Ifo.Hlp.Helpers;

namespace Ifo.Hlp.Programs
{
    public abstract class ImageEditorProgram
    {        
        public static async Task TestAsync()
        {
            var filename = Path.Combine(DirectoryHelper.GetProjectDirectory(), @"Files\zurich-l.jpg");
            using var image = await Image.LoadAsync(filename);
            image.Mutate(x => x.Sepia());
            await image.SaveAsync(filename.Replace(".jpg", ".sepia.jpg"));

        }
    }
}
