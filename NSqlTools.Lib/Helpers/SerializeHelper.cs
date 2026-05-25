using System;
using System.IO;
using System.Xml.Serialization;

namespace NSqlTools.Lib.Helpers
{
	public class SerializeHelper
	{
		#region Static Methods
		// Obje listesini XML formatına çeviren metod
		public static void SerializeToXml<T>(T data, String filePath, Type[] extraTypes = null)
		{
			if (!Directory.Exists(Path.GetDirectoryName(filePath)))
				Directory.CreateDirectory(Path.GetDirectoryName(filePath));

			XmlSerializer serializer = new XmlSerializer(typeof(T), extraTypes);
			using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
			{
				serializer.Serialize(fileStream, data);
			}
		}

		// XML'den obje listesi okuyan metod
		public static T DeserializeFromXml<T>(String filePath, Type[] extraTypes = null)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(T), extraTypes);
			using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
			{
				return (T)serializer.Deserialize(fileStream);
			}
		}
		#endregion
	}
}
