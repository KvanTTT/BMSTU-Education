using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace MusicSyncLib.Cons
{
	class Program
	{
		static void Main(string[] args)
		{
			GetAudioData();
		}

		static void SerializeDeserialize()
		{
			var serializer = new XmlSerializer(typeof(scorepartwise));

			scorepartwise score;
			using (var reader = new StreamReader(@"..\..\..\Data\On high mountain.xml"))
			{
				score = (scorepartwise)serializer.Deserialize(reader);
			}

			using (var writer = new StreamWriter(@"..\..\..\Data\On high mountain (deserialized).xml"))
			{
				serializer.Serialize(writer, score);
			}
		}

		static void GetAudioData()
		{
			var decoder = new Decoder();
			decoder.LoadAudioFile(@"..\..\..\Data\test.mp3");
		}
	}
}
