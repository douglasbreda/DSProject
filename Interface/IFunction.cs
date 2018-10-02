namespace DSProject.Interface
{
    public interface IFunction
    {
        #region [Properties]

        string Description { get; set; }

        string Pattern { get; set; }

        int Size { get; set; }

        object Result { get; set; }

        bool IsMatch { get; set; }

        #endregion
    }
}
