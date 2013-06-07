using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using GoogleSpeechWebApiPoc;
using System.CodeDom.Compiler;
using GlobalEnglish.Step.Web.FileRepository.ffmpeg;
using MvcApplication1.Helpers;
using GlobalEnglish.Recognition.DataContracts;

namespace MvcApplication1.Controllers
{
    public class RecognizeController : ApiController
    {
        // GET api/regognize
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/regognize/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/regognize
		public RecognitionResult Post()
        {
			//configure proxy so that I can see server requests in fiddler
			//GlobalProxySelection.Select = new WebProxy("127.0.0.1", 8888);

			//fast and dirty - copy audio file from one stream to another
			Stream audioData = new MemoryStream();
			Request.Content.CopyToAsync(audioData);

			string recognizedText = recognizeFile(audioData);
			if (recognizedText == "")
			{
				return null;
			}

			RecognitionResult result = analyzePronunciation(audioData);
			if (result == null || result.Type != "RecognitionSucceeded")
			{
				return null;
			}

			result.RecognizedText = recognizedText;

			return result;
        }

		private RecognitionResult analyzePronunciation(Stream audioData)
		{
			HttpClient client = new HttpClient();

			//copy Speech specific headers
			client.DefaultRequestHeaders.Add("ExpectedResults", Request.Headers.GetValues("ExpectedResults").First());
			client.DefaultRequestHeaders.Add("RecognitionType", Request.Headers.GetValues("RecognitionType").First());
			client.DefaultRequestHeaders.Add("Grammar", Request.Headers.GetValues("Grammar").First());
			client.DefaultRequestHeaders.Add("UserId", Request.Headers.GetValues("UserId").First());

			byte[] data;
			audioData.Position = 0;
			using (var br = new BinaryReader(audioData)) data = br.ReadBytes((int)audioData.Length);
			ByteArrayContent content = new ByteArrayContent(data);
			content.Headers.ContentType = new MediaTypeHeaderValue("audio/ogg");

			//call to speech
			HttpResponseMessage response = client.PostAsync("http://stagespeech.globalenglish.com/recognitions.svc/requests", content).Result;
			response.EnsureSuccessStatusCode();
			string responseBody = response.Content.ReadAsStringAsync().Result;

			return JsonConvert.DeserializeObject<RecognitionResult>(responseBody);
		}

		private string recognizeFile(Stream audioData)
		{
			using (TempFileCollection tempFiles = new TempFileCollection())
			{
				//create temporary file name to save input stream to
				string oggFilePath = Path.GetTempPath() + Path.GetRandomFileName() + ".ogg";
				tempFiles.AddFile(oggFilePath, false);
				//save input stream to temporary file
				using (Stream inputFileStream = File.OpenWrite(oggFilePath))
				{
					CopyStream(audioData, inputFileStream);
				}

				//create temporary file name to save output file to
				string flacFilePath = Path.GetTempPath() + Path.GetRandomFileName() + ".flac";
				tempFiles.AddFile(flacFilePath, false);

				//convert input file
				Converter converter = new Converter();
				converter.ConvertFile(oggFilePath, flacFilePath);

				GoogleSpeechWebApiPoc.GoogleSpeechApi.SpeechInputResult result = GoogleSpeechApi.ProcessFlacFile(flacFilePath, 16000);

				return (result.getBestHypothesis() == null) ? "" : result.getBestHypothesis().utterance;
			}
		}

		private void CopyStream(Stream input, Stream output)
		{
			input.Position = 0;
			byte[] buffer = new byte[8 * 1024];
			int len;
			while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
			{
				output.Write(buffer, 0, len);
			}
		}

        // PUT api/regognize/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/regognize/5
        public void Delete(int id)
        {
        }
    }
}
