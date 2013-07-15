using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MFGroup.PrintingControl.Entity;

namespace MFGroup.PrintingControl.Service.Abstract
{
    public interface IMaterialService
    {
        void Save(Material material);
    }
}
