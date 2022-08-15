namespace UtilityTools.CustomComponentModel
{
    public class NameImageAttribute : System.ComponentModel.DescriptionAttribute
    {
        public string Image { get; set; }

        public NameImageAttribute(string name, string image) : base(name)
        {
            this.Image = image;
        }
    }
}