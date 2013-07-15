using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MFGroup.PrintingControl.Entity;

namespace MFGroup.PrintingControl.Repository.Abstract
{
    public interface IMaterialRepository
    {
        void Save(Material material);

        IQueryable<Material> ObterMateriais();
    }
}
