using System;
using System.Collections.Generic;

namespace NSqlTools.Types
{
	[Serializable]
	public class ConnectionStringContract
	{
		#region Constructors
		public ConnectionStringContract()
		{
			Id = Guid.NewGuid().ToString();
		}

		public ConnectionStringContract(String name, String connectionString) : this()
		{
			Name = name;
			ConnectionString = connectionString;
		}
		#endregion

		#region Methods
		public String Id { get; set; }

		public String Name { get; set; }

		public String ConnectionString { get; set; }

		public String DataSource { get; set; }

		public String UserName { get; set; }

		public String Password { get; set; }

		public String InitialCatalog { get; set; }

		public Boolean IntegratedSecurity { get; set; }

		// Eklenen veritabanı sıralama listesi
		public List<String> DatabaseOrderList { get; set; } = new List<String>();
		#endregion
	}
}
