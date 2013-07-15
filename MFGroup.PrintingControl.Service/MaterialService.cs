using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MFGroup.PrintingControl.Service.Abstract;
using MFGroup.PrintingControl.Repository.Abstract;

namespace MFGroup.PrintingControl.Service
{
    public class MaterialService : IMaterialService
    {
        private IMaterialRepository materialRepository = null;

        public MaterialService(IMaterialRepository materialRepo)
        {
            materialRepository = materialRepo;
        }

        public void Save(Entity.Material material)
        {
            throw new NotImplementedException();
        }
    }
}
