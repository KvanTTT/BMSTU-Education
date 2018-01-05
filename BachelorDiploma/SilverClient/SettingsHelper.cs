using System;
using KvantSound;
using System.IO;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;

namespace SilverClient
{
	public static class SettingsHelper
	{
		public static void Save(Settings Settings)
		{
			using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
			{
				using (IsolatedStorageFileStream isfs = new IsolatedStorageFileStream("Settings.xml", FileMode.Create, isf))
				{
					using (StreamWriter Writer = new StreamWriter(isfs))
					{
						XmlSerializer serializer = new XmlSerializer(typeof(Settings));
						serializer.Serialize(Writer, Settings);
					}
				}
			}
		}

		public static Settings Load()
		{
			Settings result = null;
			using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
			{			
				using (IsolatedStorageFileStream isfs = new IsolatedStorageFileStream("Settings.xml", FileMode.OpenOrCreate, isf))
				{
					using (StreamReader Reader = new StreamReader(isfs))
					{
						XmlSerializer serializer = new XmlSerializer(typeof(Settings));
						try
						{
							result = (Settings)serializer.Deserialize(Reader);
						}
						catch
						{
							result = new Settings();
						}
					}
				}
			}
			return result;
		}
	}
}
