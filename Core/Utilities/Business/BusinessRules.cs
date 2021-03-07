using Core.Utilities.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        public static IResult Run(params IResult[] logics) {
            foreach (var logic in logics)
            {
                if (!logic.Success)
                {
                    return logic;
                }
            }/*
            for (int i = 0; i < logics.Length; i++)// eger istersek bununla methodun son halini error list donduruyo yapip tum hatalari dondurebilir.
            {
                if (!logics[i].Success)
                {
                    return logics[i];
                }
            }*/

            return null;
        
        }

    }
}
