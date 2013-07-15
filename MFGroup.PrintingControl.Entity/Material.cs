using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MFGroup.PrintingControl.Entity
{
    public class Material
    {
        public int MaterialID { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [Required]
        [StringLength(500)]
        public string Descricao { get; set; }

        [Required]
        public virtual Marca Marca { get; set; }
        
        public int? Quantidade { get; set; }

        [StringLength(6)]
        public string Gramatura { get; set; }

        [StringLength(50)]
        public string Tamanho { get; set; }

        public decimal? Valor { get; set; }
    }
}
