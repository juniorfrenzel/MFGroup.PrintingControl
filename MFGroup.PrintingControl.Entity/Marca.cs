using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MFGroup.PrintingControl.Entity
{
    public class Marca
    {
        [Key]
        public int MarcaID { get; set; }

        [Required]
        [StringLength(50)]
        public string Descricao { get; set; }
    }
}
