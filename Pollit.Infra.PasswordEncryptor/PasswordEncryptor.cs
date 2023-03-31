using System;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Pollit.Domain._Ports;
using Pollit.Domain.Users.ClearPasswords;
using Pollit.Domain.Users.EncryptedPasswords;

namespace Pollit.Infra.PasswordEncryptor;

public class PasswordEncryptor : IPasswordEncryptor
{
    public EncryptedPassword Encrypt(ClearPassword clearPassword)
    {
        var salt = B64Salt.NewSalt();
        var hash = GenerateHash(clearPassword, salt);

        return new EncryptedPassword(salt, hash);
    }
    
    public bool ValidateClearPasswordAgainstEncrypted(ClearPassword clearPassword, EncryptedPassword encryptedPassword)
    {
        var hash = GenerateHash(clearPassword, encryptedPassword.Salt);
        return hash == encryptedPassword.Hash;
    }

    private static string GenerateHash(ClearPassword password, B64Salt salt)
        => Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password.Value,
            salt.AsByteArray(),
            KeyDerivationPrf.HMACSHA1,
            100000,
            256 / 8)
        );
}