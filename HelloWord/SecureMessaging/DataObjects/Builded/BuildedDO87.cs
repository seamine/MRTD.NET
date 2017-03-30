﻿using HelloWord.Cryptography;
using HelloWord.Infrastructure;
using HelloWord.ISO7816.CommandAPDU.Body;
using HelloWord.SecureMessaging.DataObjects.DO;

namespace HelloWord.SecureMessaging.DataObjects.Builded
{
    public class BuildedDO87 : IBinary
    {
        private readonly IBinary _kSenc;
        private readonly IBinary _rawCommandApdu;
        public BuildedDO87(
                IBinary rawCommandApdu,
                IBinary kSenc
            )
        {
            _kSenc = kSenc;
            _rawCommandApdu = rawCommandApdu;
        }

        public byte[] Bytes()
        {
            //If no Data is available, leave building DO ‘87’ out
            var data = new CommandApduData(
                            new CommandApduBody(_rawCommandApdu)
                        );

            if (new Length(data).Is(0))
            {
                return new Binary().Bytes();
            }

            var encryptedData = new EncryptedCommandApduData(
                                    _kSenc,
                                    new Padded(data)
                                );

            return new DO87(encryptedData).Bytes();
        }
    }
}
