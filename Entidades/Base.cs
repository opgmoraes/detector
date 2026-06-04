using System.ComponentModel.DataAnnotations;

namespace Detector.Entidades
{
    public class Base
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public DateTime DataAtualizacao { get; set; } = DateTime.Now;
        public bool Ativo { get; internal set; } = true;
        public virtual void Desativar()
        {
            Ativo = false;
            DataAtualizacao = DateTime.Now;
        }

    }
}
