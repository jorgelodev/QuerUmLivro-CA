

using System.ComponentModel.DataAnnotations;

namespace QuerUmLivro.Domain.Entities
{
    public abstract class EntityBase
    {
        public int Id { get; set; }
        protected EntityBase()
        {
            
        }            
    }
}
