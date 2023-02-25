using Pollit.Infra.PasswordEncryptor;

namespace Pollit.Test.Common.Builders.Domain._Ports;

public class PasswordEncryptorBuilder : IFluentBuilder<PasswordEncryptor>
{
    public PasswordEncryptor Build()
    {
        return new PasswordEncryptor();
    }
}