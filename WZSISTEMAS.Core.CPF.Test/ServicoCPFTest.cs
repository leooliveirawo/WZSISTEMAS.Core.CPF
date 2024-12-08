using WZSISTEMAS.Core.CPF.Interfaces;

namespace WZSISTEMAS.Core.CPF.Test;

#nullable disable

[TestClass]
public class ServicoCPFTest
{
    private readonly IServicoCPF servicoCPF;

    private readonly string cPFNulo = default;
    private readonly string cPFVazio = string.Empty;
    private readonly string cPFComprimentoErrado = "312496070";
    private readonly string cPFInvalido = "31249607001";
    private readonly string cPFValido = "31249607000";

    private readonly string digitosNulo = default;
    private readonly string digitosVazio = string.Empty;
    private readonly string digitosComprimentoErrado = "31249607";
    private readonly string digitos = "312496070";

    public ServicoCPFTest() : this(new ServicoCPF())
	{
		servicoCPF = new ServicoCPF();
	}

    public ServicoCPFTest(IServicoCPF servicoCPF)
    {
        this.servicoCPF = servicoCPF;
    }

    [TestMethod, ExpectedException(typeof(ArgumentNullException))]
	public virtual void Validar_CPFNulo()
	{
        servicoCPF.Validar(cPFNulo);
	}

    [TestMethod, ExpectedException(typeof(ArgumentException))]
    public virtual void Validar_CPFVazio()
	{
        servicoCPF.Validar(cPFVazio);
	}

    [TestMethod]
    public virtual void Validar_CPFComprimentoErrado()
    {
        if (servicoCPF.Validar(cPFComprimentoErrado))
            Assert.Fail();
    }

    [TestMethod]
    public virtual void Validar_CPFInvalido_OK()
    {
        if (servicoCPF.Validar(cPFInvalido))
            Assert.Fail();
    }

    [TestMethod]
    public virtual void Validar_CPFValido_OK()
    {
        if (!servicoCPF.Validar(cPFValido))
            Assert.Fail();
    }

    [TestMethod]
    public virtual void Gerar_OK()
    {
        if (!servicoCPF.Validar(servicoCPF.Gerar()))
            Assert.Fail();
    }

    [TestMethod, ExpectedException(typeof(ArgumentNullException))]
    public virtual void GerarDV_DigitosNulo()
    {
        servicoCPF.GerarDV(digitosNulo);
    }

    [TestMethod, ExpectedException(typeof(ArgumentException))]
    public virtual void GerarDV_DigitosVazio()
    {
        servicoCPF.GerarDV(digitosVazio);
    }

    [TestMethod, ExpectedException(typeof(ArgumentException))]
    public virtual void GerarDV_DigitosComprimentoErrado()
    {
        servicoCPF.GerarDV(digitosComprimentoErrado);
    }

    [TestMethod]
    public virtual void GerarDV_OK()
    {
        var cPF = servicoCPF.GerarDV(digitos);

        Assert.AreEqual(cPF, cPFValido);
    }
}
