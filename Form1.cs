using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
	public partial class Form1 : Form
	{

		private static readonly HttpClient client = new HttpClient();

		public Form1()
		{
			InitializeComponent();
		}

		private async void btnSendRequest_Click(object sender, EventArgs e)
		{
			await SendPostRequest();
		}

		private async Task SendPostRequest()
		{
			var apiUrl = "http://192.168.1.210:3000/send";

			var postData = new
			{
				recipient = "markkelvin524@gmail.com",
				subject = "test",
				text = "test",
				filename = "sample.pdf",
				path = "./sample.pdf"
			};

			// Serialize object to JSON
			var json = JsonSerializer.Serialize(postData);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			try
			{
				// Send the POST request
				HttpResponseMessage response = await client.PostAsync(apiUrl, content);

				if (response.IsSuccessStatusCode)
				{
					string responseData = await response.Content.ReadAsStringAsync();
					label1.Text = "Response: " + responseData;
				}
				else
				{
					label1.Text = "Error: " + response.StatusCode;
				}

			}
			catch (Exception ex)
			{
				label1.Text = "Exception: " + ex.Message;
			}
		}
	}
}
