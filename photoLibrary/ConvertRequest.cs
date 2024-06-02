namespace photoLibrary
{
    public class ConvertRequest
    {
        public string FolderGuid { get; set; }

        public string[] ConvertFile { get; set; }

        public string[] ConvertType { get; set; }
    }
}