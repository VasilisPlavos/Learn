using System.Reflection;
using SixLabors.ImageSharp.Formats.Webp;

namespace Examples._23.ImageSharp.Services;

public interface IImageSharpService
{
    Task<bool> RunAsync();
}

public class ImageSharpService : IImageSharpService
{
    private readonly string _buildPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!);

    public async Task<bool> RunAsync()
    {
        // loading image from file
        using var image = await Image.LoadAsync($"{_buildPath}/image.jpg");
        
        // get image file extension (ex. jpg)
        var imageTypeExtension = GetImageTypeExt(image);
        
        // get base64string of webp image from jpg image
        var base64String = image.ToBase64String(WebpFormat.Instance);
        
        // get image file extension from base64String (ex. webp)
        imageTypeExtension = GetBase64StringImageTypeExt(base64String);
        
        // load image from base64String
        using var image2 = ConvertBase64StringToImage(base64String);
        
        // load image to MemoryStream in WebP format and return it
        var imageStream = ImageToStream(image);
        
        // get base64string of webp image from ImageStream
        base64String = StreamToBase64String(imageStream);
        
        // saving image as webp
        await image.SaveAsync($"{_buildPath}/image.webp");
        
        // load image to MemoryStream in webp image
        // save image to webp
        // return webpimage from stream
        var webPImage = ImageToWebP(image);
        image.Dispose();
        image2.Dispose();
        return true;
    }

    private Image ConvertBase64StringToImage(string base64String)
    {
        var finalB64 = base64String.Split(",")[1];
        var imageContent = Convert.FromBase64String(finalB64);
        return Image.Load(imageContent);
    }

    private string GetBase64StringImageTypeExt(string base64String)
    {
        var imgExt = base64String.Split(",")[0].Replace("data:image/", "").Replace(";base64","");
        return imgExt;
    }

    private string GetImageTypeExt(Image image)
    {
        return image.Metadata.DecodedImageFormat!.FileExtensions.First();
    }
    
    private MemoryStream ImageToStream(Image image)
    {
        var webPStream = new MemoryStream();
        image.Save(webPStream, new WebpEncoder{ Quality = 80 }); // similar image.SaveAsWebp(webPStream);
        webPStream.Position = 0;
        return webPStream;
    }
    
    private Image ImageToWebP(Image image)
    {
        var stream = ImageToStream(image);
        return Image.Load(stream);
    }
    
    private string StreamToBase64String(MemoryStream imageStream)
    {
        using var image = Image.Load<Rgba32>(imageStream);
        var webPBase64String = image.ToBase64String(WebpFormat.Instance);
        return webPBase64String;
    }
}