using Xunit;
using Polo.Dashboard.WebApi.Domain.Model;

namespace Polo.Dashboard.WebApi.Tests.Empregados
{
    public class EmpregadosTests
    {
        [Fact(DisplayName = "Given an employee atributes, then should create an employee")]
        public void Empregado_Constructor_ShouldSetProperties()
        {
            // Arrange
            int n_pessoal = 12345;
            string sg_emp = "ABC";
            string texto_rh = "Texto RH";
            int centro_cst = 6789;
            string centro_custo = "Centro Custo";
            string cargo = "Cargo";
            string data_nascimento = "01/01/1980";

            // Act
            var empregado = new Domain.Model.Empregados(n_pessoal, sg_emp, texto_rh, centro_cst, centro_custo, cargo, data_nascimento);

            // Assert
            Assert.Equal(n_pessoal, empregado.n_pessoal);
            Assert.Equal(sg_emp, empregado.sg_emp);
            Assert.Equal(texto_rh, empregado.texto_rh);
            Assert.Equal(centro_cst, empregado.centro_cst);
            Assert.Equal(centro_custo, empregado.centro_custo);
            Assert.Equal(cargo, empregado.cargo);
            Assert.Equal(data_nascimento, empregado.data_nascimento);
        }
    }
}