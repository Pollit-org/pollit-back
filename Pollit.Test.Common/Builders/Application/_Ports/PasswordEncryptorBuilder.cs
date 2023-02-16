using Pollit.Infra.PasswordEncryptor;

namespace Pollit.Test.Common.Builders.Application._Ports;

public class PasswordEncryptorBuilder : IFluentBuilder<PasswordEncryptor>
{
    public PasswordEncryptor Build()
    {
        return new PasswordEncryptor();
    }
}