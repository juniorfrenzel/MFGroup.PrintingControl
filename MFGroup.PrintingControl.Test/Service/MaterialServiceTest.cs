using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MFGroup.PrintingControl.Repository.Abstract;
using MFGroup.PrintingControl.Service.Abstract;
using MFGroup.PrintingControl.Entity;
using MFGroup.PrintingControl.Service;
using MFGroup.PrintingControl.Test.AssertExtensions;

namespace MFGroup.PrintingControl.Test.Service
{
    [TestClass]
    public class MaterialServiceTest : BaseTest
    {
        private Marca marcaA = new Marca { MarcaID = 1, Descricao = "Marca A" };
        private Marca marcaB = new Marca { MarcaID = 2, Descricao = "Marca B" };

        private List<Material> materiaisCadastrados = null;

        public MaterialServiceTest()
        {
            materiaisCadastrados = new List<Material> {
                new Material{ MaterialID = 1, Descricao = "Material 1", Marca = marcaA },
                new Material{ MaterialID = 2, Descricao = "Material 2", Marca = marcaA },
                new Material{ MaterialID = 3, Descricao = "Material 3", Marca = marcaB }
            };
        }

        private Mock<IMaterialRepository> CriarRepositorioMocado()
        {
            Mock<IMaterialRepository> materialRepository = new Mock<IMaterialRepository>();

            materialRepository.Setup(s => s.ObterMateriais()).Returns(materiaisCadastrados.AsQueryable());

            return materialRepository;
        }

        [TestMethod]
        public void NaoPodeCadastrarOuModificarMaterialComDataDefault()
        {
            Mock<IMaterialRepository> materialRepository = CriarRepositorioMocado();

            Material novoMaterial = new Material();

            MaterialService materialService = new MaterialService(materialRepository.Object);

            Assert.Throws<ArgumentException>(() => materialService.Save(novoMaterial), "Data não informada!", ExceptionMessageCompareOptions.Exact);
        }

        [TestMethod]
        public void NaoPodeCadastrarOuModificarMaterialSemDescricao()
        {
            Mock<IMaterialRepository> materialRepository = CriarRepositorioMocado();

            Material novoMaterial = new Material { Data = DateTime.UtcNow };

            MaterialService materialService = new MaterialService(materialRepository.Object);

            Assert.Throws<ArgumentException>(() => materialService.Save(novoMaterial), "Descrição não informada!", ExceptionMessageCompareOptions.Exact);
        }

        [TestMethod]
        public void NaoPodeCadastrarOuModificarMaterialSemMarca()
        {
            Mock<IMaterialRepository> materialRepository = CriarRepositorioMocado();

            Material novoMaterial = new Material { Data = DateTime.UtcNow, Descricao = "Novo Material" };

            MaterialService materialService = new MaterialService(materialRepository.Object);

            Assert.Throws<ArgumentException>(() => materialService.Save(novoMaterial), "Marca não informada!", ExceptionMessageCompareOptions.Exact);
        }

        [TestMethod]
        public void NaoPodeCadastarMaterialComQuantidadeNegativa()
        {
            Mock<IMaterialRepository> materialRepository = CriarRepositorioMocado();

            Material novoMaterial = new Material
            {
                Data = DateTime.UtcNow,
                Descricao = "Novo Material",
                Marca = marcaA,
                Quantidade = -1
            };

            MaterialService materialService = new MaterialService(materialRepository.Object);

            Assert.Throws<ArgumentException>(() => materialService.Save(novoMaterial), "Não é permitido cadastrar material com quantidade negativa!", ExceptionMessageCompareOptions.Exact);

            materialRepository.Verify(v => v.ObterMateriais(), Times.Once());
        }

        [TestMethod]
        public void NaoPodeCadastarMaterialComValorNegativo()
        {
            Mock<IMaterialRepository> materialRepository = CriarRepositorioMocado();

            Material novoMaterial = new Material
            {
                Data = DateTime.UtcNow,
                Descricao = "Novo Material",
                Marca = marcaA,
                Quantidade = 10,
                Valor = -10
            };

            MaterialService materialService = new MaterialService(materialRepository.Object);

            Assert.Throws<ArgumentException>(() => materialService.Save(novoMaterial), "Não é permitido cadastrar material com valor negativo!", ExceptionMessageCompareOptions.Exact);

            materialRepository.Verify(v => v.ObterMateriais(), Times.Once());
        }

        [TestMethod]
        public void NaoPodeCadastarMaterialComDescricaoDuplicadaParaMesmaMarca()
        {
            Mock<IMaterialRepository> materialRepository = CriarRepositorioMocado();

            Material novoMaterial = new Material
            {
                Data = DateTime.UtcNow,
                Descricao = "Material 1",
                Marca = marcaA 
            };

            MaterialService materialService = new MaterialService(materialRepository.Object);

            Assert.Throws<ArgumentException>(() => materialService.Save(novoMaterial), "Material já cadastrado para esta marca!", ExceptionMessageCompareOptions.Exact);

            materialRepository.Verify(v => v.ObterMateriais(), Times.Once());
        }

        [TestMethod]
        public void NaoPodeModificarMaterialComDescricaoDuplicaParaMesmaMarca()
        {
            Mock<IMaterialRepository> materialRepository = CriarRepositorioMocado();

            MaterialService materialService = new MaterialService(materialRepository.Object);

            Material materialModificado = materiaisCadastrados.SingleOrDefault(s => s.MaterialID == 1);

            materialModificado.Descricao = "Material 2";

            Assert.Throws<ArgumentException>(() => materialService.Save(materialModificado), "Não é possível alterar a descrição ou a marca do material, descrição já cadastrada para esta marca!", ExceptionMessageCompareOptions.Exact);

            materialRepository.Verify(v => v.ObterMateriais(), Times.Once());
        }

        [TestMethod]
        public void PodeCadastrarMaterialSemValorEQuantidade()
        {
            Mock<IMaterialRepository> materialRepository = CriarRepositorioMocado();

            Material novoMaterial = new Material { Data = DateTime.UtcNow, Descricao = "Novo Material", Marca = marcaA  };

            MaterialService materialService = new MaterialService(materialRepository.Object);

            materialRepository.Verify(v => v.Save(It.IsAny<Material>()), Times.Once());
        }

        [TestMethod]
        public void PodeCadastrarMaterialSemValor()
        {
            Mock<IMaterialRepository> materialRepository = CriarRepositorioMocado();

            Material novoMaterial = new Material { Data = DateTime.UtcNow, Descricao = "Novo Material", Marca = marcaA, Quantidade = 10 };

            MaterialService materialService = new MaterialService(materialRepository.Object);

            materialRepository.Verify(v => v.Save(It.IsAny<Material>()), Times.Once());
        }

        public void PodeCadastrarMaterialSemQuantidade()
        {
            Mock<IMaterialRepository> materialRepository = CriarRepositorioMocado();

            Material novoMaterial = new Material { Data = DateTime.UtcNow, Descricao = "Novo Material", Marca = marcaA, Valor = 10 };

            MaterialService materialService = new MaterialService(materialRepository.Object);

            materialRepository.Verify(v => v.Save(It.IsAny<Material>()), Times.Once());
        }

        public void PodeCadastrarMaterial()
        {
            Mock<IMaterialRepository> materialRepository = CriarRepositorioMocado();

            Material novoMaterial = new Material { Data = DateTime.UtcNow, Descricao = "Novo Material", Marca = marcaA, Quantidade = 10, Valor = 10 };

            MaterialService materialService = new MaterialService(materialRepository.Object);

            materialRepository.Verify(v => v.Save(It.IsAny<Material>()), Times.Once());
        }

        [TestMethod]
        public void PodeModificarMaterial()
        {
            Mock<IMaterialRepository> materialRepository = CriarRepositorioMocado();

            Material materialModificado = materiaisCadastrados.SingleOrDefault(s => s.MaterialID == 1);

            materialModificado.Descricao = "Material 4";
            materialModificado.Marca = marcaB;
            materialModificado.Data = DateTime.UtcNow;

            materialRepository.Verify(v => v.Save(It.IsAny<Material>()), Times.Once());
        }
    }
}
