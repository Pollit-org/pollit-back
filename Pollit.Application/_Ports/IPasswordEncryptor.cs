﻿using Pollit.Domain.Users.ClearPasswords;
using Pollit.Domain.Users.EncryptedPasswords;

namespace Pollit.Application._Ports;

public interface IPasswordEncryptor
{
    EncryptedPassword Encrypt(ClearPassword clearPassword);

    bool ValidateClearPasswordAgainstEncrypted(ClearPassword clearPassword, EncryptedPassword encryptedPassword);
}