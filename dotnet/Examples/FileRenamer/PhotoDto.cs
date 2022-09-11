namespace FileRenamer;

public class PhotoDto
{
    public int ViewOrder { get; set; }
    public string FileExt { get; set; }
    public string Location { get; set; }
    public DateTime DateCreated { get; set; }
    public string NewFileName { get; set; }
}