using NSqlTools.Types.BaseTypes;
using ScintillaNET;
using System;

namespace NSqlTools.Types.FormDataContracts
{
    [Serializable]
    public class TextViewerScreenDataContract : BaseScreenDataContract
    {
		public TextViewerScreenDataContract() { }
		
		public TextViewerScreenDataContract(String name = null) : base(name) { }
        public string ViewerText { get; set; }

		public Lexer? Lexer { get; set; }
	}
}