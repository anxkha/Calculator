// ----------------------------------------------------------------------------
//  Calculator.cs
//  Copyright (c) 2012, Lucas M. Suggs
//
//  This is the Model for storing and manipulating the calculator memory state.
// ----------------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;

namespace Calculator.Models
{
    public class Calculator
    {
        public enum Operations
        {
            None = 0,
            Divide,
            Multiply,
            Subtract,
            Add,
        }

        [DataType(DataType.Currency)]
        public double CurrentValue { get; set; }

        [DataType(DataType.Text)]
        public string CurrentHistory { get; set; }

        public Operations CurrentOperation { get; set; }

        public double LastValue { get; set; }
        public double RunningTotal { get; set; }
        public double MemoryStore { get; set; }

        public bool FirstOperation { get; set; }
        public bool DoEqualsRepeat { get; set; }

        public bool DivideByZero { get; set; }

        public void PerformCalculation()
        {
            DivideByZero = false;

            switch (CurrentOperation)
            {
                case Models.Calculator.Operations.Add:
                    RunningTotal += LastValue;
                    break;

                case Models.Calculator.Operations.Subtract:
                    RunningTotal -= LastValue;
                    break;

                case Models.Calculator.Operations.Multiply:
                    RunningTotal *= LastValue;
                    break;

                case Models.Calculator.Operations.Divide:
                    if (0 == LastValue)
                    {
                        DivideByZero = true;
                        break;
                    }

                    RunningTotal /= LastValue;
                    break;

                case Models.Calculator.Operations.None:
                    RunningTotal = LastValue;
                    break;
            }
        }
    }
}