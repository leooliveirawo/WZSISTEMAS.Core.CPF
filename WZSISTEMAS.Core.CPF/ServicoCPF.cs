using WZSISTEMAS.Core.CPF.Interfaces;

namespace WZSISTEMAS.Core.CPF;

/// <summary>
/// Representa os recurso do serviço para manipular um CPF.
/// </summary>
public class ServicoCPF : IServicoCPF
{
    public virtual string Gerar()
    {
        var digitos = string.Empty;
        var random = new Random();

        while (digitos.Length < 9)
            digitos += random.Next(0, 9).ToString();
        
        return GerarDV(digitos);
    }

    /// <summary>
    /// Gera os dígitos verificadores do digitos do CPF informado.
    /// </summary>
    /// <param name="digitos">Os dígitos do CPF sem os digitos verificadores.</param>
    /// <returns>Um valor <see cref="string"/> representando o CPF com os dígitos verificadores.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="digitos"/> é nulo.</exception>
    /// <exception cref="ArgumentException"><paramref name="digitos"/> é vázio.</exception>
    /// <exception cref="ArgumentException"><paramref name="digitos"/> com comprimento incorreto.</exception>
    public virtual string GerarDV(string digitos)
    {
        ArgumentNullException.ThrowIfNull(digitos, nameof(digitos));
        ArgumentException.ThrowIfNullOrEmpty(digitos, nameof(digitos));

        return digitos.Length != 9
            ? throw new ArgumentException("O CPF parcial deve conter 9 digitos")
            : ComputarSegundoDigito(
            ComputarPrimeiroDigito(digitos));
    }

    /// <summary>
    /// Computa o dígito verificador do CPF com base no multiplicador.
    /// </summary>
    /// <param name="digitos">Os digitos do CPF.</param>
    /// <param name="multiplicadores">Os multiplicadores para computar o digito verificador. (9 digitos para o primeiro e 10 digitos para o segundo)</param>
    /// <returns>Um valor <see cref="string"/> representando o CPF com o dígito verificador computador.</returns>
    private static string ComputarDigito(string digitos, int[] multiplicadores)
    {
        var total = 0;

        for (var i = 0; i < digitos.Length; i++)
            total += Convert.ToInt32(digitos[i].ToString()) * (multiplicadores[i]);

        var resto = total % 11;
        var digito = resto > 2
            ? 11 - resto
            : 0;

        return $"{digitos}{digito}";
    }

    /// <summary>
    /// Computa o primeiro dígito verificador do CPF.
    /// </summary>
    /// <param name="digitos">Os digitos do CPF.</param>
    /// <returns>Um valor <see cref="string"/> representando o CPF com o primeiro dígito verificador computador.</returns>
    private static string ComputarPrimeiroDigito(string digitos)
    {
        return ComputarDigito(digitos, [10, 9, 8, 7, 6, 5, 4, 3, 2]);
    }

    /// <summary>
    /// Computa o segundo dígito verificador do CPF.
    /// </summary>
    /// <param name="digitos">Os digitos do CPF. (O primeiro dígito verificador deve estar presente)</param>
    /// <returns>Um valor <see cref="string"/> representando o CPF com o segundo dígito verificador computador.</returns>
    private static string ComputarSegundoDigito(string digitos)
    {
        return ComputarDigito(digitos, [11, 10, 9, 8, 7, 6, 5, 4, 3, 2]);
    }

    /// <summary>
    /// Realiza a validação do CPF informado por parâmetro.
    /// </summary>
    /// <param name="cPF">O CPF que será validado.</param>
    /// <returns>Um valor <see cref="bool"/> representando se o CPF é válido.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="digitos"/> é nulo.</exception>
    /// <exception cref="ArgumentException"><paramref name="digitos"/> é vázio.</exception>
    public virtual bool Validar(string cPF)
    {
        ArgumentNullException.ThrowIfNull(cPF, nameof(cPF));
        ArgumentException.ThrowIfNullOrEmpty(cPF, nameof(cPF));

        if (cPF.Length != 11)
            return false;

        var digitos = string.Empty;

        foreach (var digito in cPF.Take(9).ToList())
            digitos += digito.ToString();

        return GerarDV(digitos) == cPF;
    }
}
