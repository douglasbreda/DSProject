using DSProject.Interface;

namespace DSProject.Model
{
    public class Function : IFunction
    {
        public string Description { get; set; } = "";
        public string Pattern { get; set; } = "";
        public int Size { get; set; } = 0;
        public object Result { get; set; } = "";
        public bool IsMatch { get; set; } = false;
        public string Link { get; set; }
    }
}
