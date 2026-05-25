using System;
using static NSqlTools.Types.Enums;

namespace NSqlTools.Types
{
	[Serializable]
	public class TableContract
	{
		#region Constructors
		public TableContract(String name, Int32 tableId) : this()
		{
			Name = name;
			TableId = tableId;
		}

		public TableContract() { }
		#endregion

		#region Properties
		public String Name { get; set; }

		public Int32 TableId { get; set; }

        public Int32 RowCount { get; set; }

        public RunningStatusEnum Status { get; set; } = RunningStatusEnum.NotCompleted;

		public String StatusName
		{
			get
			{
				switch (Status)
				{
					case RunningStatusEnum.Completed:
						return "Completed";
					case RunningStatusEnum.NotCompleted:
						return "Not Completed";	
					case RunningStatusEnum.Running:
						return "Running";
				}

				return null;
			}
		}

		#endregion
	}
}
