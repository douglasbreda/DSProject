using System.ComponentModel.DataAnnotations;
using DSProject.Interface;

namespace DSProject.Model
{
    //Classe base para implementação de classes simples
    public class BaseEntity : IEntity
    {
        #region [Interface]

        //Id da tabela
        [Key]
        public int Id { get; set; }

        //Descrição padrão
        public string Description { get; set; }

        //Chave estrangeira para o integrante
        public int IntegrantId { get; set; }

        #endregion
    }
}