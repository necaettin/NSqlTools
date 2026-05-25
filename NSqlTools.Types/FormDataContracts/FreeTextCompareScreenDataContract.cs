using NSqlTools.Types.BaseTypes;
using ScintillaNET;
using System;

namespace NSqlTools.Types.FormDataContracts
{
    [Serializable]
    public class FreeTextCompareScreenDataContract : BaseScreenDataContract
    {
		public FreeTextCompareScreenDataContract() { }
		public FreeTextCompareScreenDataContract(String name = null) : base(name) { }

		public Lexer? Lexer { get; set; }

		public string LeftText { get; set; }

        public string RightText { get; set; }
    }
}