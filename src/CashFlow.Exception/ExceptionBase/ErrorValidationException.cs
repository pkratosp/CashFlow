﻿namespace CashFlow.Exception.ExceptionBase
{
    public class ErrorValidationException : CashFlowException
    {
        public List<string> Errors { get; set; }

        public ErrorValidationException(List<string> errorsMessage) { 
            Errors = errorsMessage;
        }

    }
}
