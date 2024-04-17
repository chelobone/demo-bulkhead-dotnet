using System;
namespace web_app_demo.Entidades
{
	public class Payment
	{
		public int Id { get; set; }
		public int Amount { get; set; }
		public string Description { get; set; }
		public int Channel { get; set; }
	}
}

