namespace DSProject.Interface
{
    public interface IEntity
    {
         #region [Properties]

         int Id { get; set; }

         string Description { get; set; }

         int IntegrantId { get; set; }

         #endregion
    }
}