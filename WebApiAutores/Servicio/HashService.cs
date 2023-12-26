﻿using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Cryptography;
using WebApiAutores.DTOs;

namespace WebApiAutores.Servicio
{
    public class HashService
    {
        public ResultadoHash Hash(string textoPlano)
        {
            var sal = new byte[6];
            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(sal);
            }
            return Hash(textoPlano, sal);
        }

        public ResultadoHash Hash(string textoPlano, byte[] sal)
        {
            var llaveDerivada = KeyDerivation.Pbkdf2(password: textoPlano,
                salt: sal, KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 32);

            var hash = Convert.ToBase64String(llaveDerivada);

            return new ResultadoHash()
            {
                Hash = hash,
                Sal = sal
            };
        }
    }
}