using System;

namespace NSqlTools.Types
{
    [Serializable]
    public class FavoriteQueryContract
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string QueryText { get; set; }

        public DateTime CreatedDate { get; set; }

		public String UniqueId { get; set; }

		public override string ToString()
		{
			return Name;
		}
	}
}
