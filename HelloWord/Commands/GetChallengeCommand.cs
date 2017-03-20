﻿using HelloWord.CommandAPDU;
using HelloWord.Cryptography;
using HelloWord.Infrastructure;
using PCSC;
using PCSC.Iso7816;

namespace HelloWord.Commands
{
    public class GetChallengeCommand : ICommandAPDU
    {
        private readonly IsoCase _isoCase = IsoCase.Case2Short;
        private readonly int _expectedDataLength = 8;
        private readonly SCardProtocol _activeProtocol = SCardProtocol.T1;
        private readonly IBinary _applicationIdentifier = new BinaryHex("011E"); // 0x01 0x1E
        public byte[] Bytes()
        {
            return new CommandApdu(this._isoCase, this._activeProtocol)
            {
                CLA = 0x00,
                Instruction = InstructionCode.GetChallenge,
                P1 = 0x00,
                P2 = 0x00,
                Le = 8,
            }.ToArray();
        }

        public int ExceptedDataLength()
        {
            return this._expectedDataLength;
        }

        public IsoCase Case()
        {
            return this._isoCase;
        }

        public SCardProtocol Protocol()
        {
            return this._activeProtocol;
        }
    }
}
