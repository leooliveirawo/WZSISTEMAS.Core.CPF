namespace WZSISTEMAS.Core.CPF.Interfaces;

/// <summary>
/// Expoe os recursos do serviço para manipular um CPF.
/// </summary>
public interface IServicoCPF
{
    /// <summary>
    /// Realiza a validação do CPF informado por parâmetro.
    /// </summary>
    /// <param name="cPF">O CPF que será validado.</param>
    /// <returns>Um valor <see cref="bool"/> representando se o CPF é válido.</returns>
    bool Validar(string cPF);
    
    /// <summary>
    /// Gera randômicamente um CPF.
    /// </summary>
    /// <returns>Um valor <see cref="string"/> representando o CPF gerado.</returns>
    string Gerar();

    /// <summary>
    /// Gera os dígitos verificadores do digitos do CPF informado.
    /// </summary>
    /// <param name="digitos">Os dígitos do CPF sem os digitos verificadores.</param>
    /// <returns>Um valor <see cref="string"/> representando o CPF com os dígitos verificadores.</returns>
    string GerarDV(string digitos);
}
