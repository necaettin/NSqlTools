using NSqlTools.Lib.Helpers;
using NSqlTools.Types.FormDataContracts;
using System;
using System.IO;

namespace NSqlTools.BusinessLayer
{
    public class FormDataBusiness
    {
		public Type[] formTypes 
		{ 
			get
			{
				return new Type[] {
					typeof(SqlViewerScreenDataContract),
					typeof(TextViewerScreenDataContract),
					typeof(RunQueryScreenDataContract),
					typeof(SearchDBScreenDataContract),
					typeof(InsertScriptGeneratorScreenDataContract),
					typeof(FreeTextCompareScreenDataContract),
					typeof(FavoriteQueryScreenDataContract),
					typeof(DBObjectCompareScreenDataContract),
					typeof(DBBatchCompareScreenDataContract),
					typeof(ConnectionStringsScreenDataContract),
					typeof(ProjectsScreenDataContract),
					typeof(SnippetScreenDataContract),
					typeof(DataCompareScreenDataContract),
					typeof(TfsChangesetSearchScreenDataContract)
				};
			} 
		}

		public ScreenDataListContract GetAll(String fileName)
        {
            if (!File.Exists(fileName))
                return new ScreenDataListContract();

			ScreenDataListContract formDataContract = SerializeHelper.DeserializeFromXml<ScreenDataListContract>(fileName, formTypes);

			return formDataContract ?? new ScreenDataListContract();
        }

		public void SaveAll(ScreenDataListContract formDataContract, String fileName)
        {
			SerializeHelper.SerializeToXml(formDataContract, fileName, formTypes);
        }
    }
}
