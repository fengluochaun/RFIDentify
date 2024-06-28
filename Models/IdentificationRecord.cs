using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFIDentify.Models
{
	public class IdentificationRecord
	{
		public int RecordId { get; set; }
		public int UserId { get; set; }
		public string? Name { get; set; }
		public DateTime Time { get; set; }
		
		public string? RecognitionStatus { get; set; }
	}
}
